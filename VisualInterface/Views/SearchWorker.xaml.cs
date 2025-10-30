namespace VisualInterface.Views;

public partial class SearchWorker : ContentPage
{
	public SearchWorker()
	{
		InitializeComponent();
	}

    private void BackbtnSearch_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
}