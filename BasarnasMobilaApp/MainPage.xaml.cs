namespace BasarnasMobilaApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
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