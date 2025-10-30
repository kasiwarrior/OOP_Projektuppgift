//using Intents;
using VisualInterface.Views;

namespace VisualInterface
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void Addbtn_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(AddWorker));
        }

        private void Searchbtn_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(SearchWorker));
        }

        private void Showbtn_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(ShowWorkers));
        }

        private async void Backupbtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Backup", "Den är inte klar","Lägg in funktionen när det passar"); 
        }

        private async void Shutdownbtn_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Avsluta", "Den är inte klar", "Lägg in funktionen när det passar");
        }
    }
}
