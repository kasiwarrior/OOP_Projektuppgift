using BackendLibrary;

namespace VisualInterface
{
    public partial class MainPage : ContentPage
    {
        private WorkerRegistry workerRegistry;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            workerRegistry = new WorkerRegistry();
            workerRegistry.LoadBackup("WorkerRegistry");
        }

        private async void ShowWorkers_Clicked(object sender, EventArgs e)
        {
            // Skapa en instans av den nya sidan och skicka med WorkerRegistry
            var showPage = new ShowWorkersPage(workerRegistry);

            // Använd Navigation för att gå till listsidan
            await Navigation.PushAsync(showPage);
        }

        private async void SearchWorker_Clicked(object sender, EventArgs e)
        {
            // Använd MAUI-popup (DisplayPromptAsync) istället för Console.ReadLine()
            string idInput = await DisplayPromptAsync("Sök arbetare", "Ange ID:", "OK", "Avbryt", keyboard: Keyboard.Numeric);

            if (int.TryParse(idInput, out int id))
            {
                var worker = workerRegistry.SearchWorker(id).First();

                if (worker != null)
                    await DisplayAlert("Hittad", worker.ToString(), "OK");
                else
                    await DisplayAlert("Sökresultat", "Ingen arbetare hittades med det ID:t.", "OK");
            }
        }

        // 3. UPPDATERA ARBETARE (Menyval 3)
        private async void UpdateWorker_Clicked(object sender, EventArgs e)
        {
            string idInput = await DisplayPromptAsync("Uppdatera arbetare", "Ange ID för arbetaren du vill uppdatera:", "Fortsätt", "Avbryt", keyboard: Keyboard.Numeric);

            if (string.IsNullOrWhiteSpace(idInput)) return;

            if (int.TryParse(idInput, out int id))
            {
                IWorker worker = workerRegistry.SearchWorker(id: id).First(); // Använder den enkla SearchWorker(id)

                if (worker != null)
                {
                    // NAVIGERA TILL UPPDATERINGSSIDAN
                    var updatePage = new UpdateWorkerPage(workerRegistry, worker);
                    await Navigation.PushAsync(updatePage);
                }
                else
                {
                    await DisplayAlert("Fel", $"Ingen arbetare hittades med ID {id}.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Fel", "Ogiltigt ID angivet.", "OK");
            }
        }

        // 4. TA BORT ARBETARE
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
                var worker = workerRegistry.SearchWorker(id).First();

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
                        if (workerRegistry.RemoveWorker(id))
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

        // 5. SKAPA BACKUP
        private void CreateBackup_Clicked(object sender, EventArgs e)
        {
            workerRegistry.CreateBackup("WorkerRegistry");
            DisplayAlert("Backup", "Backup skapad!", "OK");
        }

        // EXTRA: LÄGG TILL ARBETARE
        private async void AddWorker_Clicked(object sender, EventArgs e)
        {
            // Skapa en instans av den nya sidan och skicka med WorkerRegistry
            var addPage = new AddWorkerPage(workerRegistry);

            // Använd Navigation för att gå till den nya sidan
            await Navigation.PushAsync(addPage);
        }

        private void ExitApp_Clicked(object sender, EventArgs e)
        {
            // I MAUI avslutar man normalt inte appen helt. Logik för de?
            DisplayAlert("Avslutar", "Applikationen fortsätter i bakgrunden. Data sparat.", "OK");
        }
    }
}