public class TokenStorage : ITokenStorage
{
    private const string TokenKey = "auth_token";

    //Save the token
    public async Task SaveTokenAsync(string token)
    {
        await SecureStorage.Default.SetAsync(TokenKey,token);
    }

    //Get Token
    public async Task GetTokenAsync()
    {
        return await SecureStorage.Default.GetAsync(TokenKey);
    }

    //Clear Token
    public Task ClearTokenAsync()
    {
        SecureStorage.Default.Remove(TokenKey);
        return Task.CompletedTask;
    }
}