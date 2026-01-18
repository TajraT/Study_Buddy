using Study_Buddy.Services;

namespace Study_Buddy
{
    public partial class App : Application
    {
        public static TimerService TimerService { get; } = new();

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();

            ThemeMenager.LoadTheme();
            TaskStorage.LoadTasks();
            
        }

       
    }
}