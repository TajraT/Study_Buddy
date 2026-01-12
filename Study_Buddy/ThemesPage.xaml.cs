using Study_Buddy.Services;

namespace Study_Buddy;

public partial class ThemesPage : ContentPage
{
	public ThemesPage()
	{
		InitializeComponent();
	}

    private void Neutral_Clicked(object sender, EventArgs e)
    {
        ThemeMenager.ApplyTheme("Neutral");
    }

    private void Warm_Clicked(object sender, EventArgs e)
    {
        ThemeMenager.ApplyTheme("Warm");
    }

    private void Cold_Clicked(object sender, EventArgs e)
    {
        ThemeMenager.ApplyTheme("Cold");
    }

    private void Pink_Clicked(object sender, EventArgs e)
    {
        ThemeMenager.ApplyTheme("Pink");
    }
}