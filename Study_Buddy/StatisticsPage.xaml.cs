using Microsoft.Maui.Graphics;
using Study_Buddy.Services;
using Microsoft.Maui.Controls;

namespace Study_Buddy;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage()
    {
        InitializeComponent();
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

        ChartView.Drawable = new BarChartDrawable(days, dailyMinutes);
    }
}

public class BarChartDrawable : IDrawable
{
    private readonly List<DateTime> _days;
    private readonly List<int> _values;

    public BarChartDrawable(List<DateTime> days, List<int> values)
    {
        _days = days;
        _values = values;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Colors.Transparent;
        canvas.FillRectangle(dirtyRect);

        float barWidth = dirtyRect.Width / (_values.Count * 2);
        float maxValue = Math.Max(1, _values.Max());

        for (int i = 0; i < _values.Count; i++)
        {
            float barHeight = (_values[i] / maxValue) * (dirtyRect.Height - 40);
            float x = (i * 2 + 1) * barWidth;
            float y = dirtyRect.Height - barHeight - 20;

            canvas.FillColor = Microsoft.Maui.Graphics.Color.FromRgba(0xDF, 0xB3, 0x86, 0xFF);
            canvas.FillRoundedRectangle(x, y, barWidth, barHeight, 8);

            canvas.FontColor = Colors.Black;
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
