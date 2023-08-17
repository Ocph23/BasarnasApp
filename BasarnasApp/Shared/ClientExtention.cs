
using BasarnasApp.Shared.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

public static class ClientExtention
{


    public static async Task<T> GetResult<T>(this HttpResponseMessage response)
    {
        try
        {
            var stringData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch
        {
            return default(T);
        }
    }


    public static async Task<string> GetError(this HttpResponseMessage response)
    {
        try
        {
            var stringData = await response.Content.ReadAsStringAsync();
            return stringData;
        }
        catch
        {
            return "Maaf terjadi kesalahan, coba ulangi lagi.";
        }
    }


    public static string EnumToString(this Penyebab model) {
       return Regex.Replace(model.ToString(), @"(?<=[a-z])([A-Z])", @" $1");
    }

   

}