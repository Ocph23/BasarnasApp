﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OcphApiAuth.Client;
public class OcphAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClient _client;

    public OcphAuthStateProvider(HttpClient client, ILocalStorageService _localStorageService)
    {
        localStorageService = _localStorageService;
        _client = client;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        IdentityModelEventSource.ShowPII = true;
        var token =await localStorageService.GetItemAsync<string>("token");

       // string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY0MzRkZDJiLWRmNzYtNGYyMC1hNjcwLTgxMjk5MTRiNTdmZiIsIm5hbWUiOiJBZG1pbiIsInN1YiI6Im9jcGgyMy50ZXN0QGdtYWlsLmNvbSIsImVtYWlsIjoib2NwaDIzLnRlc3RAZ21haWwuY29tIiwianRpIjoiNjcwY2MwNmMtN2NlMi00M2MwLTk5NGQtMmU5MTljMTk2ODU2IiwibmJmIjoxNjg1Mzc3MzIzLCJleHAiOjE2ODU5ODIxMjMsImlhdCI6MTY4NTM3NzMyMywiaXNzIjoiaHR0cHM6Ly9zZG5pbnByZXN0YW5qdW5ncmlhLmFwc3BhcHVhLmNvbS8iLCJhdWQiOiJodHRwczovL3NkbmlucHJlc3Rhbmp1bmdyaWEuYXBzcGFwdWEuY29tLyJ9.w_VqVaq_J_CMVH5M7eBTDbOJ3q7CDPmLypQ5-ep2mNt32jNXw1-NQ-KASIjq6QQLUa8x9CnxH9jD7JV8EgDc3g";

        ClaimsIdentity identity = new();
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        try
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nameof(token));
            string secret = "nUcE2ItIDCCgZdFEZ7nZJwqgNEcp0QKOr4OwZqob/G7SIbA0BJ1WQYEl7BbqTwUv";
            string issuer = "https://basarnas.apspapua.com/";
            await Task.Delay(100);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            ArgumentNullException.ThrowIfNull(jwtToken, "Jwt Token Not Created");
            string id = jwtToken.Claims.FirstOrDefault(x => x.Type == "id").Value;
            var name = jwtToken.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var sub = jwtToken.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var roles = jwtToken.Claims.Where(x => x.Type == "role").ToList();
            var claims = new List<Claim>
                {
                               new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                               new Claim(ClaimTypes.Name, name),
                               new Claim(ClaimTypes.Email, email),
                };

            foreach (var claim in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, claim.Value));
            }
            identity = new ClaimsIdentity(claims, "jwt");
            user = new ClaimsPrincipal(identity);
            state = new AuthenticationState(user);
            
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            await _client.SetToken(localStorageService);
            return state;
        }
        catch (Exception)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }
    }
}
