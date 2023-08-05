using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasarnasMobilaApp
{
    public class RestClient:HttpClient
    {
        public RestClient()
        {
            this.BaseAddress = new Uri(Helper.Url);
            this.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            _ = SetToken(Account.Token);
        }

        private Task SetToken(string token)
        {
            this.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return Task.CompletedTask;
        }
    }
}
