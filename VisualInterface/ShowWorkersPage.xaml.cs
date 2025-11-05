using BackendLibrary;

namespace VisualInterface;


    public partial class ShowWorkersPage : ContentPage
    {
        private List<IWorker> _workers;

        public ShowWorkersPage(List<IWorker> workers)
        {
            InitializeComponent();
            _workers = workers;

            // Ladda datan när sidan skapas
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            WorkersListView.ItemsSource = _workers;

            if (_workers == null || _workers.Count == 0)
            {
                Title = "Inga Arbetare Hittades";
            }
        }
    }