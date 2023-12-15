namespace OfficeManagerBlazorServer.Services.Interfaces
{
    public interface ITokenProvider
    {
        public ValueTask CleanTokenAsync();
        public ValueTask<string> GetToken();
        public ValueTask SetToken(string token);
    }
}
