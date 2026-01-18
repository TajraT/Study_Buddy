using Microsoft.Maui.Controls;
using Study_Buddy.Services;
using Study_Buddy.Models;
using Study_Buddy.Services;

namespace Study_Buddy;

public partial class FocusModePage : ContentPage
{
    private readonly TimerService _timer = App.TimerService;

    private bool _focusFinished = false;
    private bool _sessionSaved = false;

    public FocusModePage()
    {
        InitializeComponent();

        _timer.Tick += seconds =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FocusTimerLabel.Text =
                    TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
            });
        };

        _timer.Finished += () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _focusFinished = true;
                SaveSession();
                ExitButton.IsEnabled = true;
                ExitButton.Opacity = 1;
            });
        };
    }

    private async void ExitFocusMode_Clicked(object sender, EventArgs e)
    {
        if (!_focusFinished) return;

        int minutes = Preferences.Get("last_timer_minutes", 25);
        _timer.Reset(minutes * 60);

        await Navigation.PopModalAsync();
    }

    protected override bool OnBackButtonPressed()
    {
        return !_focusFinished;
    }

    private void SaveSession()
    {
        if (_sessionSaved) return;

        int minutes = Preferences.Get("last_timer_minutes", 25);

        var session = new FocusSession
        {
            Date = DateTime.Today,
            DurationMinutes = minutes
        };

        FocusStatsService.SaveSession(session);
        _sessionSaved = true;
    }
}
