using BackendLibrary;

namespace VisualInterface;


    public partial class ShowWorkersPage : ContentPage
    {
        private WorkerRegistry _registry;

        public ShowWorkersPage(WorkerRegistry registry)
        {
            InitializeComponent();
            _registry = registry;

            // Ladda datan när sidan skapas
            LoadWorkers();
        }

        private void LoadWorkers()
        {
            // Hämta listan från din BackendLibrary
            List<IWorker> workers = _registry.GetAllWorkers();

            // Koppla listan till CollectionView's ItemsSource
            WorkersListView.ItemsSource = workers;

            if (workers == null || workers.Count == 0)
            {
                Title = "Inga Arbetare Hittades";
            }
        }
    }