using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Study_Buddy.Services;

public class TimerService
{
    private readonly Timer _timer;

    public int SecondsLeft { get; private set; }
    public bool IsRunning { get; private set; }

    public event Action<int>? Tick;
    public event Action? Finished;

    public TimerService()
    {
        _timer = new Timer(1000);
        _timer.Elapsed += OnTick;
    }

    public void SetTime(int seconds)
    {
        SecondsLeft = seconds;
        Tick?.Invoke(SecondsLeft);
    }

    public void Start()
    {
        if (IsRunning) return;
        IsRunning = true;
        _timer.Start();
    }

    public void Pause()
    {
        _timer.Stop();
        IsRunning = false;
    }

    public void Reset(int seconds)
    {
        Pause();
        SetTime(seconds);
    }

    private void OnTick(object? sender, System.Timers.ElapsedEventArgs e)
    {
        if (SecondsLeft <= 0)
        {
            Pause();
            Finished?.Invoke();
            return;
        }

        SecondsLeft--;
        Tick?.Invoke(SecondsLeft);
    }
}
