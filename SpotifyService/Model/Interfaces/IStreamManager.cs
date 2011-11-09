using SpotifyService.Cargo;

namespace SpotifyService.Models.Interfaces
{
    public interface IStreamManager
    {
        void RequestTrackStream(Track track);
    }
}