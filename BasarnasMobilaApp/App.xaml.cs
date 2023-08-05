using BasarnasMobilaApp.Pages;

namespace BasarnasMobilaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            
            if(!Account.UserIsLogin)
            {
                MainPage = Helper.GetService<LoginPage>();

            }
            else
            {
                MainPage = new AppShell();
            }

        }
    }
}