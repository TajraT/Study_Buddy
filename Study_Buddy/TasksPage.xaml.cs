using System.Threading.Tasks;

namespace Study_Buddy;

public partial class TasksPage : ContentPage
{
	public TasksPage()
	{
		InitializeComponent();
		BindingContext = TaskService.Tasks;
	}

    private async void AddTask_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new NavigationPage(new AddTaskPage()));
    }
	private void DeleteTask_Clicked(object sender, EventArgs e)
	{
		if (sender is Button btn && btn.BindingContext is TaskItem task)
		{
			TaskService.Tasks.Remove(task);
			TaskStorage.SaveTasks();
		}
	}
}