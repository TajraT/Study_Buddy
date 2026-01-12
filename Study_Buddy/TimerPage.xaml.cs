using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
namespace Study_Buddy;

public partial class TimerPage : ContentPage
{

    private int _seconds;
    private int _lastEnteredMinutes = 25;
    private CancellationTokenSource? _cts;

    public TimerPage()
    {
        InitializeComponent();
        SetDefaultTime();
    }

    private void SetDefaultTime()
    {
        _seconds = _lastEnteredMinutes * 60;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        TimerLabel.Text = TimeSpan
            .FromSeconds(_seconds)
            .ToString(@"mm\:ss");
    }

    private async void Start_Clicked(object sender, EventArgs e)
    {
        if (_cts != null)
            return;

        if (int.TryParse(MinutesEntry.Text, out int minutes) && minutes > 0)
        {
            _seconds = minutes * 60;
            _lastEnteredMinutes = minutes;
            UpdateLabel();


            ConfirmText.Text = $"Focus session set to {minutes} minutes";
            ConfirmPopup.IsVisible = true;
        }

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
        finally
        {
            _cts = null;
        }
    }

    private void ClosePopup_Clicked(object sender, EventArgs e)
    {
        ConfirmPopup.IsVisible = false;
    }

    private void Pause_Clicked(object sender, EventArgs e)
    {
        _cts?.Cancel();
        _cts = null;
    }

    private void Restart_Clicked(object sender, EventArgs e)
    {
        _cts?.Cancel();
        SetDefaultTime();
    }

    private async void EnterFocusMode_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new FocusModePage(_seconds));

    }


    private async void HomeImage_Tapped(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void MinutesEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(int.TryParse(MinutesEntry.Text,out int minutes) && minutes > 0)
        {
            _seconds = minutes * 60;
            _lastEnteredMinutes = minutes;
            UpdateLabel();
        }else if (string.IsNullOrEmpty(MinutesEntry.Text))
        {
            _seconds = _lastEnteredMinutes * 60;
            UpdateLabel();
        }
    }
}


