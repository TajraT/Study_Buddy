using System.Threading.Tasks;

namespace Study_Buddy;

public partial class FocusModePage : ContentPage
{
	public FocusModePage()
	{
		InitializeComponent();
	}

    private async void ExitFocusMode_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync();
    }
}