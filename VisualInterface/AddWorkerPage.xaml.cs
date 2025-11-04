using BackendLibrary;

namespace VisualInterface;

public partial class AddWorkerPage : ContentPage
{
    // Vi behöver en referens till registret från MainPage
    private WorkerRegistry _registry;

    // Konstruktor som tar emot registret
    public AddWorkerPage(WorkerRegistry registry)
    {
        InitializeComponent();
        _registry = registry;
        // Förfyll startdatum till idag för enkelhetens skull
        StartDatePicker.Date = DateTime.Today;
    }

    private async void OnSaveWorkerClicked(object sender, EventArgs e)
    {
        // --- 1. DATAVALIDERING OCH HÄMTNING ---

        // ID
        if (!int.TryParse(IdEntry.Text, out int id))
        {
            await DisplayAlert("Fel", "Ange ett giltigt numeriskt ID.", "OK");
            return;
        }

        // Namn
        string name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlert("Fel", "Namn får inte vara tomt.", "OK");
            return;
        }

        // Enums (WorkType)
        if (!Enum.TryParse(WorkTypeEntry.Text, true, out WorkType workType))
        {
            await DisplayAlert("Fel", "Ogiltig jobbtyp. Använd t.ex. Ant eller Bee.", "OK");
            return;
        }

        // Enums (ShiftType)
        if (!Enum.TryParse(ShiftTypeEntry.Text, true, out ShiftType shiftType))
        {
            await DisplayAlert("Fel", "Ogiltigt skift. Använd Day eller Night.", "OK");
            return;
        }

        // Övrig data
        bool workShoes = ShoesSwitch.IsToggled;
        DateTime startDate = StartDatePicker.Date;


        // --- 2. SKAPA OCH SPARA ARBETARE -

        //Anropa din metod i WorkerRegistry(du måste ha denna metod implementerad i BackendLibrary)
         if (_registry.AddWorker(id, name, workType, shiftType, workShoes, startDate)) // Exempel på hur det kan se ut
        {
            await DisplayAlert("Framgång!", $"Arbetaren {name} lades till.", "OK");
            await Navigation.PopAsync(); // Gå tillbaka till MainPage
        }
        else
        {
            await DisplayAlert("Fel", "Kunde inte lägga till arbetare (Kanske ID redan finns?).", "OK");
        }
    }
}