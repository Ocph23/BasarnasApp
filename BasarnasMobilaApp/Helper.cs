using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

#if __ANDROID__
    using BasarnasMobilaApp.Platforms.Android;
#endif


namespace BasarnasMobilaApp
{
    public class Helper
    {
        public static string Url => "https://kb06llx0-5215.asse.devtunnels.ms";
        public static JsonSerializerOptions JsonOption => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public static TService GetService<TService>() => Current.GetService<TService>();

        internal static Task StartForegroundService()
        {
#if __ANDROID__
            Android.Content.Intent intent = new Android.Content.Intent(Android.App.Application.Context,typeof(ForegroundService));
            Android.App.Application.Context.StartForegroundService(intent);
#endif
            return Task.CompletedTask;
        }

        public static IServiceProvider Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
        MauiWinUIApplication.Current.Services;
#elif __ANDROID__
        MauiApplication.Current.Services; 
#elif IOS || MACCATALYST
        MauiUIApplicationDelegate.Current.Services;
#else
        null;
#endif
    }
}
