using Microsoft.Extensions.Logging;




namespace BasarnasMobilaApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Montserrat-Regular.ttf", "Montserrat");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "Montserrat Semibold");

                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "Font Awesome 6 Free Regular");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "Font Awesome 6 Free Solid");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "Font Awesome 6 Free Brand Regular");
                }).Services.AddAppService();

        #if DEBUG
		    builder.Logging.AddDebug();
        #endif

            return builder.Build();
        }
    }
}