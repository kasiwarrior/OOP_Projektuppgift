namespace VisualInterface
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            //SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
