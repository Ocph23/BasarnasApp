using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;



namespace BasarnasMobilaApp
{
    public class Helper
    {
        public static string Url => "https://5smglw7t-7208.asse.devtunnels.ms";

        public static async Task<byte[]> TakePhoto()
        {
            try
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    using Stream sourceStream = await photo.OpenReadAsync();
                    using MemoryStream localFileStream = new MemoryStream();
                    await sourceStream.CopyToAsync(localFileStream);
                    return localFileStream.ToArray();
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Task ShellMessageShow(string title, string message)
        {
            Shell.Current.DisplayAlert(title, message,"Ok");
            return Task.CompletedTask;
        }

        public static Task AppMessageShow(string title, string message)
        {
            Application.Current.MainPage.DisplayAlert(title, message, "Ok");
            return Task.CompletedTask;
        }

        public static JsonSerializerOptions JsonOption => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public static TService GetService<TService>() => Current.GetService<TService>();

   

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
