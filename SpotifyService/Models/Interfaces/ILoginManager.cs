namespace SpotifyService.Models.Interfaces
{
    public interface ILoginManager
    {
        void AttemptLogin(string userName, string password);
    }
}