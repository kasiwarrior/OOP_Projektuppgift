using Microsoft.Maui.Controls;

namespace VisualInterface
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(mainPage);
        }
    }
}