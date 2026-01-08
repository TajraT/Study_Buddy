using Study_Buddy.Services;

namespace Study_Buddy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();

            ThemeMenager.LoadTheme();
            
        }

       
    }
}