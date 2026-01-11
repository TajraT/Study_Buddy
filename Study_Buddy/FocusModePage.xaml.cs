using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Study_Buddy;

public partial class FocusModePage : ContentPage
{
    private int _seconds;
    private CancellationTokenSource? _cts;

    public FocusModePage(int seconds)
    {
        InitializeComponent();
        _seconds = seconds;
        UpdateLabel();
        StartTimer();
    }

    private void UpdateLabel()
    {
        FocusTimerLabel.Text = TimeSpan
            .FromSeconds(_seconds)
            .ToString(@"mm\:ss");
    }

    private async void StartTimer()
    {
        _cts = new CancellationTokenSource();

        try
        {
            while (_seconds > 0)
            {
                await Task.Delay(1000, _cts.Token);
                _seconds--;
                UpdateLabel();
            }
        }
        catch (TaskCanceledException) { }
       
        ExitButton.IsEnabled = true;
        ExitButton.Opacity = 1;

    }

    private async void ExitFocusMode_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
