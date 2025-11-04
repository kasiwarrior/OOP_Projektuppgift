
using BackendLibrary;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VisualInterface
{
    public partial class MainPage : ContentPage
    {
        private readonly WorkerRegistry _registry;
        private ObservableCollection<WorkerDto> _workers = new();

        public MainPage(WorkerRegistry registry)
        {
            InitializeComponent();
            _registry = registry;

            WorkTypePicker.ItemsSource = Enum.GetNames(typeof(WorkType)).ToList();
            ShiftTypePicker.ItemsSource = Enum.GetNames(typeof(ShiftType)).ToList();

            StartDatePicker.Date = DateTime.Now;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await EnsureBackupAvailableAndLoadAsync();
            RefreshWorkers();
        }

        private async Task EnsureBackupAvailableAndLoadAsync()
        {
            
            string destDir = FileSystem.AppDataDirectory;
            string destPath = Path.Combine(destDir, "Backup.csv");

            if (!File.Exists(destPath))
            {
                try
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync("Backup.csv");
                    using var outFs = File.Create(destPath);
                    await stream.CopyToAsync(outFs);
                }
                catch
                {
                    
                }
            }

            
            try
            {
                Directory.SetCurrentDirectory(destDir);
            }
            catch
            {
              
            }

           
            _registry.LoadBackup();
        }

        private void RefreshWorkers()
        {
            var list = _registry.SearchWorker(); 
            var vms = list.Select(w => new WorkerDto
            {
                Id = w.GetId(),
                Name = w.GetName(),
                WorkType = w.GetWorkType().ToString(),
                ShiftType = w.GetShiftType().ToString(),
                HasShoes = w.GetWorkShoes(),
                StartDate = w.GetStartDate(),
                Display = w.ToString(),
                Original = w
            }).ToList();

            _workers = new ObservableCollection<WorkerDto>(vms);
            WorkersView.ItemsSource = _workers;
        }

        
        private void SearchByName_Click(object sender, EventArgs e)
        {
            var name = SearchNameEntry.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayAlert("Info", "Ange ett namn att söka.", "OK");
                return;
            }

            var results = _registry.SearchWorker(name: name);
            if (results.Count == 0)
            {
                DisplayAlert("Resultat", "Inga arbetare hittades.", "OK");
                return;
            }

            _workers = new ObservableCollection<WorkerDto>(results.Select(w => new WorkerDto
            {
                Id = w.GetId(),
                Name = w.GetName(),
                WorkType = w.GetWorkType().ToString(),
                ShiftType = w.GetShiftType().ToString(),
                HasShoes = w.GetWorkShoes(),
                StartDate = w.GetStartDate(),
                Display = w.ToString(),
                Original = w
            }));
            WorkersView.ItemsSource = _workers;
        }

        
        private void SearchById_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SearchIdEntry.Text, out int id))
            {
                DisplayAlert("Fel", "Ogiltigt id", "OK");
                return;
            }

            if (_registry.SearchWorker(id, out var worker))
            {
                _workers = new ObservableCollection<WorkerDto>(new[]
                {
                    new WorkerDto
                    {
                        Id = worker.GetId(),
                        Name = worker.GetName(),
                        WorkType = worker.GetWorkType().ToString(),
                        ShiftType = worker.GetShiftType().ToString(),
                        HasShoes = worker.GetWorkShoes(),
                        StartDate = worker.GetStartDate(),
                        Display = worker.ToString(),
                        Original = worker
                    }
                });
                WorkersView.ItemsSource = _workers;
            }
            else
            {
                DisplayAlert("Resultat", "Arbetare hittades inte.", "OK");
            }
        }

        private void ClearSearch_Click(object sender, EventArgs e) => RefreshWorkers();

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(IdEntry.Text, out int id))
            {
                DisplayAlert("Fel", "Ogiltigt id", "OK");
                return;
            }

            var name = NameEntry.Text ?? string.Empty;
            var workType = (WorkType)Enum.Parse(typeof(WorkType), WorkTypePicker.SelectedItem?.ToString() ?? WorkType.Ant.ToString());
            var shiftType = (ShiftType)Enum.Parse(typeof(ShiftType), ShiftTypePicker.SelectedItem?.ToString() ?? ShiftType.Day.ToString());
            var shoes = ShoesSwitch.IsToggled;
            var startDate = StartDatePicker.Date;

            var ant = new Ant(id, name, workType, shiftType, shoes, startDate);
            var added = _registry.AddWorker(id, ant);

            DisplayAlert("Info", added ? "Arbetare tillagd" : "Kunde inte lägga till (id finns redan)", "OK");
            RefreshWorkers();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(IdEntry.Text, out int id))
            {
                DisplayAlert("Fel", "Ogiltigt id", "OK");
                return;
            }
            if (!_registry.SearchWorker(id, out var worker))
            {
                DisplayAlert("Fel", "Arbetare hittades inte", "OK");
                return;
            }

            if (!string.IsNullOrWhiteSpace(NameEntry.Text))
                _registry.UpdateWorkerName(id, NameEntry.Text);

            if (WorkTypePicker.SelectedItem != null)
                _registry.UpdateWorkerType(id, (WorkType)Enum.Parse(typeof(WorkType), WorkTypePicker.SelectedItem.ToString()));

            if (ShiftTypePicker.SelectedItem != null)
                _registry.UpdateWorkerShift(id, (ShiftType)Enum.Parse(typeof(ShiftType), ShiftTypePicker.SelectedItem.ToString()));

            _registry.UpdateWorkerShoes(id, ShoesSwitch.IsToggled);

            DisplayAlert("Info", "Uppdaterad", "OK");
            RefreshWorkers();
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(IdEntry.Text, out int id))
            {
                DisplayAlert("Fel", "Ogiltigt id", "OK");
                return;
            }
            var removed = _registry.RemoveWorker(id);
            DisplayAlert("Info", removed ? "Borttagen" : "Hittades ej", "OK");
            RefreshWorkers();
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            _registry.CreateBackup();
            DisplayAlert("Info", "Backup skapad", "OK");
        }

        private void LoadBackupButton_Click(object sender, EventArgs e)
        {
            
            _registry.LoadBackup();
            DisplayAlert("Info", "Backup inläst", "OK");
            RefreshWorkers();
        }

        private void WorkersView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                var dto = e.CurrentSelection[0] as WorkerDto;
                if (dto != null)
                {
                    IdEntry.Text = dto.Id.ToString();
                    NameEntry.Text = dto.Name;
                    WorkTypePicker.SelectedItem = dto.WorkType;
                    ShiftTypePicker.SelectedItem = dto.ShiftType;
                    ShoesSwitch.IsToggled = dto.HasShoes;
                    StartDatePicker.Date = dto.StartDate;
                }
            }
        }

        
        private void ExitButton_Click(object sender, EventArgs e)
        {
#if WINDOWS
            Microsoft.UI.Xaml.Application.Current.Exit();
#else
            System.Environment.Exit(0);
#endif
        }

        private class WorkerDto
        {
            public int Id { get; init; }
            public string Name { get; init; } = "";
            public string WorkType { get; init; } = "";
            public string ShiftType { get; init; } = "";
            public bool HasShoes { get; init; }
            public DateTime StartDate { get; init; }
            public string Display { get; init; } = "";
            public BackendLibrary.IWorker? Original { get; init; }
        }
    }
}