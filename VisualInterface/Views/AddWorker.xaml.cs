namespace VisualInterface.Views;

public partial class AddWorker : ContentPage
{
	public AddWorker()
	{
		InitializeComponent();
	}

    private void Backbtn_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }
}