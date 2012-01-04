using Caliburn.Micro;
using SpotifyService.Messages;

namespace Poncho.ViewModels.Interfaces
{
    public interface IShellViewModel : IHandle<LoginResultMessage>
    {
    }
}