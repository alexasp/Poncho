using SpotifyService.Enums;

namespace Poncho.ViewModels.Interfaces
{
    public interface ITrackControlViewModel
    {
        PlaybackStatus PlaybackStatus { get; set; }
        void PlayPause();
    }
}