namespace Poncho.ViewModels.Interfaces
{
    public interface ILoginViewModel
    {
        void InvalidLogin();
        void Login();
        string Username { get; set; }
        string Password { get; set; }
    }
}