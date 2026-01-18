using Microsoft.Maui.Controls;
using Study_Buddy.Services;

namespace Study_Buddy;

public partial class TimerPage : ContentPage
{
    private readonly TimerService _timer = App.TimerService;

    private int _lastEnteredMinutes = 25;
    private const string LastTimerMinutesKey = "last_timer_minutes";

    public TimerPage()
    {
        InitializeComponent();

        _lastEnteredMinutes = Preferences.Get(LastTimerMinutesKey, 25);
        MinutesEntry.Text = _lastEnteredMinutes.ToString();

        _timer.Tick += seconds =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TimerLabel.Text =
                    TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
            });
        };

        _timer.Finished += () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _timer.Reset(_lastEnteredMinutes * 60);
            });
        };

        _timer.SetTime(_lastEnteredMinutes * 60);
    }

    private void Start_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(MinutesEntry.Text, out int minutes) && minutes > 0)
        {
            _lastEnteredMinutes = minutes;
            Preferences.Set(LastTimerMinutesKey, minutes);

            _timer.SetTime(minutes * 60);
            _timer.Start();

            ConfirmText.Text = $"Focus session set to {minutes} minutes";
            ConfirmPopup.IsVisible = true;
        }
    }

    private void Pause_Clicked(object sender, EventArgs e)
    {
        _timer.Pause();
    }

    private void Restart_Clicked(object sender, EventArgs e)
    {
        _timer.Reset(_lastEnteredMinutes * 60);
    }

    private async void EnterFocusMode_Clicked(object sender, EventArgs e)
    {
        // ?? KLJUÈNI DIO
        if (!_timer.IsRunning)
        {
            int minutes = Preferences.Get(LastTimerMinutesKey, 25);
            _timer.SetTime(minutes * 60);
            _timer.Start();
        }

        await Navigation.PushModalAsync(new FocusModePage());
    }

    private async void HomeImage_Tapped(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void ClosePopup_Clicked(object sender, EventArgs e)
    {
        ConfirmPopup.IsVisible = false;
    }

    private void MinutesEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_timer.IsRunning) return;

        if (int.TryParse(e.NewTextValue, out int minutes) && minutes > 0)
        {
            _lastEnteredMinutes = minutes;
            Preferences.Set(LastTimerMinutesKey, minutes);
            _timer.SetTime(minutes * 60);
        }
    }
}
