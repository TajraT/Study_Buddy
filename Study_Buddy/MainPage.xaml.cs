namespace Study_Buddy
{
    public partial class MainPage : TabbedPage
    {
        

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new TimerPage()));
        }
    }
}
