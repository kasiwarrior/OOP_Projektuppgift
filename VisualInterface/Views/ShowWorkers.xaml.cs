namespace VisualInterface.Views;

public partial class ShowWorkers : ContentPage
{
	public ShowWorkers()
	{
		InitializeComponent();
	}

    private void BackbtnShow_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }
}