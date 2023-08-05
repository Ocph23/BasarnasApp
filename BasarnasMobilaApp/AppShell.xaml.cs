namespace BasarnasMobilaApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            #if ANDROID
                    if (!BasarnasMobilaApp.Platforms.Android.AndroidServiceManager.IsRunning)
                    {
                        BasarnasMobilaApp.Platforms.Android.AndroidServiceManager.StartMyService();
                    }
                    else{
                    }
            #endif
        }
    }
}