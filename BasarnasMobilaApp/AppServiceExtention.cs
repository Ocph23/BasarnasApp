using BasarnasMobilaApp.Pages;
using BasarnasMobilaApp.Services;



namespace BasarnasMobilaApp
{
    public static class AppServiceExtention
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
          
            services.AddSingleton<RestClient>();
            services.AddScoped<IPelaporService, PelaporService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IKejadianService, KejadianService>();
            services.AddScoped<IJenisKejadianService, JenisKejadianService>();
            services.AddScoped<IDistrictService, DistrictService>();
            
            services.AddScoped<LoginPage>();
            services.AddScoped<RegisterPage>();
            services.AddScoped<ProfilePage>();
            services.AddScoped<LaporanBaruPage>();
            services.AddScoped<LaporanPage>();
            services.AddScoped<LogoutPage>();
            services.AddScoped<MainPage>();
            return services;
        }
    }
}
