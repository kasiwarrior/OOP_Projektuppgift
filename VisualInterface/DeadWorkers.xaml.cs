using BackendLibrary;

namespace VisualInterface;

public partial class DeadWorkers : ContentPage
{
    private WorkerRegistry _registry;
    public DeadWorkers(WorkerRegistry registry)
	{
		_registry = registry;
        InitializeComponent();
    }

    private async void ShowWorkers_Clicked(object sender, EventArgs e)
    {
        // Skapa en instans av den nya sidan och skicka med WorkerRegistry
        var showPage = new ShowWorkersPage(_registry.GetAllWorkers());

        // Använd Navigation för att gå till listsidan
        await Navigation.PushAsync(showPage);
    }

    private async void RemoveWorker_Clicked(object sender, EventArgs e)
    {
        // Steg 1: Be användaren om ID med en popup
        string idInput = await DisplayPromptAsync(
            "Ta bort arbetare",
            "Ange ID för arbetaren som ska tas bort:",
            "Ta Bort",
            "Avbryt",
            keyboard: Keyboard.Numeric
        );

        // Hantera om användaren avbröt eller lämnade fältet tomt
        if (string.IsNullOrWhiteSpace(idInput)) return;

        // Steg 2: Validera ID
        if (int.TryParse(idInput, out int id))
        {
            // Sök efter arbetaren för att visa bekräftelseinformation
            var worker = _registry.SearchWorker(id).First();

            if (worker != null)
            {
                // Steg 3: Visa bekräftelse och namn/ID
                bool confirmed = await DisplayAlert(
                    "Bekräfta borttagning",
                    $"Är du säker på att du vill ta bort {worker.GetName()} (ID: {id})?",
                    "Ja, ta bort",
                    "Avbryt"
                );

                if (confirmed)
                {
                    // Steg 4: Utför borttagningen
                    if (_registry.RemoveWorker(id))
                    {

                        await DisplayAlert("Framgång!", $"{worker.GetName()} har tagits bort", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Fel", $"Kunde inte ta bort arbetare med ID {id}.", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Sökresultat", $"Ingen arbetare hittades med ID {id}.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Fel", "Ogiltigt ID angivet.", "OK");
        }
    }

    private async void SearchWorker_Clicked(object sender, EventArgs e)
    {
        var searchPage = new SearchWorkerPage(_registry);

        await Navigation.PushAsync(searchPage);
    }
}