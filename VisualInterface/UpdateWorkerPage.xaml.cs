using BackendLibrary;

namespace VisualInterface;

public partial class UpdateWorkerPage : ContentPage
{
    private WorkerRegistry _registry;
    private IWorker _workerToUpdate;

    // Konstruktor som tar emot registret och den arbetare som ska uppdateras
    public UpdateWorkerPage(WorkerRegistry registry, IWorker worker)
    {
        InitializeComponent();
        _registry = registry;
        _workerToUpdate = worker;

        // Fyll fälten med befintlig data (förladdning/pre-filling)
        LoadWorkerData();
    }

    private void LoadWorkerData()
    {
        // Visa ID
        IdLabel.Text = $"Uppdaterar Arbetare ID: {_workerToUpdate.GetId()}";

        // Fylla i Entry-fälten
        NameEntry.Text = _workerToUpdate.GetName();
        WorkTypeEntry.Text = _workerToUpdate.GetWorkType().ToString();
        ShiftTypeEntry.Text = _workerToUpdate.GetShiftType().ToString();

        // Ställ in Switch
        ShoesSwitch.IsToggled = _workerToUpdate.GetWorkShoes();
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        int id = _workerToUpdate.GetId();

        // --- 1. DATAVALIDERING OCH HÄMTNING ---

        // Namn (Enkel validering)
        string newName = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(newName))
        {
            await DisplayAlert("Fel", "Namn får inte vara tomt.", "OK");
            return;
        }

        // Enums (WorkType)
        if (!Enum.TryParse(WorkTypeEntry.Text, true, out WorkType newWorkType))
        {
            await DisplayAlert("Fel", "Ogiltig jobbtyp. Använd t.ex. Ant eller Bee.", "OK");
            return;
        }

        // Enums (ShiftType)
        if (!Enum.TryParse(ShiftTypeEntry.Text, true, out ShiftType newShiftType))
        {
            await DisplayAlert("Fel", "Ogiltigt skift. Använd Day eller Night.", "OK");
            return;
        }

        bool newWorkShoes = ShoesSwitch.IsToggled;


        // --- 2. ANROPA UPPDATERINGSMETODER ---

        bool success = true;

        // Uppdatera Namn
        if (_workerToUpdate.GetName() != newName)
            success &= _registry.UpdateWorkerName(id, newName);

        // Uppdatera Jobbtyp
        if (_workerToUpdate.GetWorkType() != newWorkType)
            success &= _registry.UpdateWorkerType(id, newWorkType);

        // Uppdatera Skift
        if (_workerToUpdate.GetShiftType() != newShiftType)
            success &= _registry.UpdateWorkerShift(id, newShiftType);

        // Uppdatera Skyddsskor
        if (_workerToUpdate.GetWorkShoes() != newWorkShoes)
            success &= _registry.UpdateWorkerShoes(id, newWorkShoes);

        // --- 3. SLUTFÖRANDE ---

        if (success)
        {
            await DisplayAlert("Framgång!", $"Arbetaren med ID {id} uppdaterades framgångsrikt.", "OK");
            await Navigation.PopAsync(); // Gå tillbaka till föregående sida (MainPage)
        }
        else
        {
            await DisplayAlert("Fel", "Kunde inte spara alla ändringar. Kontrollera ID och försök igen.", "OK");
        }
    }
}