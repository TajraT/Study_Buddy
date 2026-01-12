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
	private async void CreateTask_Clicked(object sender, EventArgs e) {
		if (string.IsNullOrWhiteSpace(TitleEntry.Text))
			return;
		var category = CategoryPicker.SelectedItem is TaskCategory selected
			? selected
			: TaskCategory.Study;

		TaskService.Tasks.Add(new TaskItem
		{
			Title = TitleEntry.Text,
			Description = DescriptionEntry.Text,
			Category = category
		});
		TaskStorage.SaveTasks();
		await Navigation.PopModalAsync();
	}
	
}