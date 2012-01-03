using Caliburn.Micro;
using SpotifyService.Messages;

namespace Poncho.ViewModels.Interfaces
{
    public interface ILoginViewModel : IHandle<LoginResultMessage>
    {
        void Login();
        string Username { get; set; }
        string Password { get; set; }
        string Output { get; set; }
    }
}