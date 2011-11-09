namespace SpotifyService.Model.Interfaces
{
    public interface ILoginManager
    {
        void AttemptLogin(string userName, string password);
    }
}