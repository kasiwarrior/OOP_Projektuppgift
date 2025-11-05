using BackendLibrary;

namespace VisualInterface;

public partial class SearchWorkerPage : ContentPage
{
	WorkerRegistry _registry;
	public SearchWorkerPage(WorkerRegistry registry)
	{
		_registry = registry;
		InitializeComponent();
	}
    private async void OnSaveWorkerClicked(object sender, EventArgs e)
    {
        // --- 1. DATAVALIDERING OCH HÄMTNING ---
        int? id = null;
        string name = null;
        WorkType? workType = null;
        ShiftType? shiftType = null;
        bool? workShoes = null;
        // ID
        if (int.TryParse(IdEntry.Text, out int tempInt))
        {
            id = tempInt;
        }

        // Namn
        name = NameEntry.Text?.Trim();
        if (string.IsNullOrEmpty(name))
        {
            name = null;
        }

        // Enums (WorkType)
        if (Enum.TryParse(WorkTypeEntry.Text, true, out WorkType tempWorkType))
        {
            workType = tempWorkType;
        }

        // Enums (ShiftType)
        if (Enum.TryParse(ShiftTypeEntry.Text, true, out ShiftType tempShift))
        {
            shiftType = tempShift;
        }

        // Övrig data

        switch (ShoePicker.SelectedItem.ToString())
        {
            default:
                workShoes = null;
                break;
            case "Ignorera skor":
                workShoes = null;
                break;
            case "Har skor":
                workShoes = true;
                break;
            case "Har inte skor":
                workShoes = false;
                break;
        }

        DateTime? startDate = null;
        TimeSortOptions option = TimeSortOptions.Specified;
        switch (TimePicker.SelectedItem.ToString())
        {
            default:
                startDate = null;
                break;
            case "Ingnorera datum":
                startDate = null;
                break;
            case "Specifierat datum":
                startDate = StartDatePicker.Date;
                option = TimeSortOptions.Specified;
                break;
            case "Före":
                startDate = StartDatePicker.Date;
                option = TimeSortOptions.Before;
                break;
            case "Efter":
                startDate = StartDatePicker.Date;
                option = TimeSortOptions.After;
                break;
        }

        var showPage = new ShowWorkersPage(_registry.SearchWorker(id, name, workType, shiftType, workShoes, startDate, option));

        await Navigation.PushAsync(showPage);
    }
}