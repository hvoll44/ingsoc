using Microsoft.IdentityModel.Tokens;

namespace Telescreen.WebApp
{
    public class TokenStorage
    {
        public async Task SetAccessTokenAsync(string email, string password)
        {
            var client = new HttpClient();
            var loginResponse = await client.PostAsJsonAsync<MyLoginInput>("https://localhost:7261/login",
            new MyLoginInput
            {
                Email = email,
                Password = password,
                TwoFactorCode = string.Empty,
                TwoFactorRecoveryCode = string.Empty
            });

            var myLoginResponse = await loginResponse.Content.ReadFromJsonAsync<AccessTokenResponse>();

            var accessToken = myLoginResponse?.AccessToken;

            AccessTokenLookup.Add(email, accessToken ?? string.Empty);
        }

        public string GetAccessToken(string? email)
        {
            if (email.IsNullOrEmpty()) return string.Empty;

            return AccessTokenLookup.TryGetValue(email!, out string? value) ? value : string.Empty;
        }

        private Dictionary<string, string> AccessTokenLookup { get; set; } = [];

        private class MyLoginInput
        {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? TwoFactorCode { get; set; }
            public string? TwoFactorRecoveryCode { get; set; }
        }

        public class AccessTokenResponse
        {
            public string? TokenType { get; set; }
            public string? AccessToken { get; set; }
            public long ExpiresIn { get; set; }
            public string? RefreshToken { get; set; }
        }
    }
}
