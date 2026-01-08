using Study_Buddy.NewFolder2;

namespace Study_Buddy
{
    public partial class MainPage : TabbedPage
    {

        private static readonly Random _random = new Random();
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            QuoteLabel.Text = Quotes.All[_random.Next(Quotes.All.Count)];
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TimerPage()));
        }
    }
}
