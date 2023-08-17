using Blazored.LocalStorage;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace OcphApiAuth.Client
{
    public static class HttpClientExtention
    {
        public static async Task<T> GetResultAsync<T>(this HttpResponseMessage response)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(response);
                var stringContent = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(stringContent))
                {
                    return JsonSerializer.Deserialize<T>(stringContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                throw new Exception("Maaf Terjadi Kesalahan !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static async Task SetToken(this HttpClient httpClient, ILocalStorageService localStorageService)
        {
            try
            {
                var token = await localStorageService.GetItemAsync<string>("token");
                if (!string.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception)
            {
            }
        }

        public static StringContent GenerateHttpContent(this HttpClient client, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }


        public static async Task<string> Error(this HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return $"'{response.RequestMessage.RequestUri.LocalPath}'  Not Found";

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                   
                    return $"'{response.RequestMessage.RequestUri.LocalPath}'  Anda Tidak Memiliki Akses !";
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                    throw new SystemException();

                if (content.Contains("message"))
                {
                    var error = JsonSerializer.Deserialize<ErrorMessage>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
                    return error.Message;
                }
                else if (content.Contains("tools.ietf"))
                {
                    var errors = JsonSerializer.Deserialize<ErrorMessages>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true });
                    return errors.Title;
                }
                return content;
            }
            catch (Exception)
            {
                return "Maaf Terjadi Kesalahan, Silahkan Ulangi Lagi Nanti";
            }
        }
       

    }

    public class ErrorMessage
    {
        public string Message { get; set; }
    }

    public class Errors
    {
        public List<string> Email { get; set; }
    }

    public class ErrorMessages
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Errors Errors { get; set; }
    }

}
