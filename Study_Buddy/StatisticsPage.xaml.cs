using Study_Buddy.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Study_Buddy;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadChart();
    }

    private void LoadChart()
    {
        var sessions = FocusStatsService.GetSessions();

        var days = Enumerable.Range(0, 7)
            .Select(i => DateTime.Today.AddDays(-6 + i))
            .ToList();

        var dailyMinutes = days
            .Select(day =>
                sessions
                    .Where(s => s.Date == day)
                    .Sum(s => s.DurationMinutes))
            .ToList();

        var primaryColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["PrimaryColor"];
        var secondaryColor = (Microsoft.Maui.Graphics.Color)Application.Current.Resources["SeconderyColor"];

        ChartView.Drawable = new BarChartDrawable(
            days,
            dailyMinutes,
            primaryColor,
            secondaryColor
        );
    }
}

public class BarChartDrawable : IDrawable
{
    private readonly List<DateTime> _days;
    private readonly List<int> _values;
    private readonly Microsoft.Maui.Graphics.Color _primaryColor;
    private readonly Microsoft.Maui.Graphics.Color _secondaryColor;

    public BarChartDrawable(
        List<DateTime> days,
        List<int> values,
        Microsoft.Maui.Graphics.Color primaryColor,
        Microsoft.Maui.Graphics.Color secondaryColor)
    {
        _days = days;
        _values = values;
        _primaryColor = primaryColor;
        _secondaryColor = secondaryColor;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Microsoft.Maui.Graphics.Colors.Transparent;
        canvas.FillRectangle(dirtyRect);

        float barWidth = dirtyRect.Width / (_values.Count * 2);
        float maxValue = Math.Max(1, _values.Max());

        for (int i = 0; i < _values.Count; i++)
        {
            float barHeight = (_values[i] / maxValue) * (dirtyRect.Height - 40);
            float x = (i * 2 + 1) * barWidth;
            float y = dirtyRect.Height - barHeight - 20;

            var day = _days[i].DayOfWeek;

            bool usePrimary =
                day == DayOfWeek.Monday ||
                day == DayOfWeek.Wednesday ||
                day == DayOfWeek.Friday ||
                day == DayOfWeek.Sunday;

            canvas.FillColor = usePrimary ? _primaryColor : _secondaryColor;

            canvas.FillRoundedRectangle(x, y, barWidth, barHeight, 8);

            canvas.FontColor = Microsoft.Maui.Graphics.Colors.Black;
            canvas.FontSize = 12;
            canvas.DrawString(
                _values[i].ToString(),
                x,
                y - 15,
                barWidth,
                15,
                HorizontalAlignment.Center,
                VerticalAlignment.Bottom
            );

            canvas.DrawString(
                _days[i].ToString("ddd"),
                x,
                dirtyRect.Height - 18,
                barWidth,
                18,
                HorizontalAlignment.Center,
                VerticalAlignment.Top
            );
        }
    }
}