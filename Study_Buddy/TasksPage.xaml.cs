using System.Threading.Tasks;

namespace Study_Buddy;

public partial class TasksPage : ContentPage
{
	public TasksPage()
	{
		InitializeComponent();
	}

    private async void AddTask_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new NavigationPage(new AddTaskPage()));
    }
}