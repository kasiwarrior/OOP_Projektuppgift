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

    private void RemoveWorker_Clicked(object sender, EventArgs e)
    {
        // Logik för att ta bort en arbetare
        DisplayAlert("Status", "Funktion: Ta bort arbetare (måste implementeras)", "OK");
    }

    private async void SearchWorker_Clicked(object sender, EventArgs e)
    {
        var searchPage = new SearchWorkerPage(_registry);

        await Navigation.PushAsync(searchPage);
    }
}