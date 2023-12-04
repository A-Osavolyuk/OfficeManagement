namespace OfficeManagerMVC.Services.Interfaces
{
    public interface ITokenProvider
    {
        public void CleanToken();
        public string GetToken();
        public void SetToken(string token);
    }
}
