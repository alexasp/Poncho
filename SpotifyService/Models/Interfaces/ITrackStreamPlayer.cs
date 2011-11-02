using SpotifyService.Cargo;

namespace SpotifyService.Models.Interfaces
{
    public interface ITrackStreamPlayer
    {
        void PlayStream(TrackStream trackStream);
    }
}