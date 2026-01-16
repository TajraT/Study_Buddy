using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Study_Buddy.Services;
using Study_Buddy.Models;

namespace Study_Buddy;

public partial class FocusModePage : ContentPage
{
    private int _seconds;
    private int _initialSeconds;
    private CancellationTokenSource? _cts;
    private bool _sessionSaved = false;

    public FocusModePage(int seconds)
    {
        InitializeComponent();
        _seconds = seconds;
        _initialSeconds = seconds;
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
        SaveSession();
        ExitButton.IsEnabled = true;
        ExitButton.Opacity = 1;

    }

    private async void ExitFocusMode_Clicked(object sender, EventArgs e)
    {
        SaveSession();
        _cts?.Cancel();
        await Navigation.PopModalAsync();
    }
    private void SaveSession()
    {
        if (_sessionSaved) return;
        int spentSeconds = _initialSeconds - _seconds;
        if (spentSeconds < 60)
            return;
        int minutes = spentSeconds / 60;
        var session = new FocusSession
        {
            Date = DateTime.Today,
            DurationMinutes = minutes
        };
        FocusStatsService.SaveSession(session);
        _sessionSaved = true;
    }
}
