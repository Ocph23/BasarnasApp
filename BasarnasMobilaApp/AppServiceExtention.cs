using BasarnasMobilaApp.Pages;
using BasarnasMobilaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            services.AddScoped<LoginPage>();
            services.AddScoped<RegisterPage>();
            services.AddScoped<ProfilePage>();
            services.AddScoped<LaporanBaruPage>();
            services.AddScoped<LaporanPage>();
            return services;
        }
    }
}
