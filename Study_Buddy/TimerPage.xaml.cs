using Microsoft.Maui.Controls;
namespace Study_Buddy;

public partial class TimerPage : ContentPage
{
    public TimerPage()
    {
        InitializeComponent();

    }
    private async void HomeImage_Tapped(object sender,EventArgs e)
    {
       await Navigation.PopModalAsync();
    }
    private async void EnterFocusMode_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FocusModePage());
    }
}
    