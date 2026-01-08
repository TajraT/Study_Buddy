using Study_Buddy.NewFolder2;

namespace Study_Buddy;

public partial class StatisticsPage : ContentPage
{
	private static readonly Random _random = new Random();
	public StatisticsPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		QuoteLabel.Text = Quotes.All[_random.Next(Quotes.All.Count)];
    }
}