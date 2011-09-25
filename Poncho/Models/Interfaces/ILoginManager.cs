namespace Poncho.Models.Interfaces
{
    public interface ILoginManager
    {
        void AttemptLogin(string userName, string password);
    }
}