using VisualInterface.Views;

namespace VisualInterface
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(EditWorker), typeof(EditWorker));
            Routing.RegisterRoute(nameof(SearchWorker), typeof(SearchWorker));
            Routing.RegisterRoute(nameof(ShowWorkers), typeof(ShowWorkers));
            Routing.RegisterRoute(nameof(AddWorker), typeof(AddWorker));
        }
    }
}
