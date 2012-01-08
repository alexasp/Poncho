using Caliburn.Micro;
using SpotifyService.Messages;

namespace Poncho.ViewModels.Interfaces
{
    public interface IConductorViewModel : IHandle<LoginResultMessage>
    {
    }
}