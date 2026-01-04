using System.Threading.Tasks;

namespace Study_Buddy;

public partial class AddTaskPage : ContentPage
{
	public AddTaskPage()
	{
		InitializeComponent();
	}

	private async void CancelTask_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}