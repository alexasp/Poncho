namespace SpotifyService.Interfaces
{
    public interface ILoginManager
    {
        void AttemptLogin(string userName, string password);
    }
}