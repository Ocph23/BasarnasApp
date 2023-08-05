using BasarnasMobilaApp.Models;
using System.Text.Json;
namespace BasarnasMobilaApp;

public class Account
{
    public static bool UserIsLogin
    {
        get
        {
            return GetUser() != null;
        }
    }

    public static AuthenticateResponse GetUser()
    {
        var userString = Preferences.Get("User", null);
        if (string.IsNullOrEmpty(userString))
            return null;
        else
            return JsonSerializer.Deserialize<AuthenticateResponse>(userString, Helper.JsonOption);
    }

    public static Task SetUser(AuthenticateResponse response)
    {
        try
        {
            var userString = JsonSerializer.Serialize(response);
            Preferences.Set("User", userString);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }

    public static Task LogOut()
    {
        try
        {
            Preferences.Set("User", string.Empty);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new SystemException(ex.Message);
        }
    }


    public static string Token
    {
        get
        {
            var user = GetUser();
            return user == null ? string.Empty : user.Token;
        }
    }
    
}