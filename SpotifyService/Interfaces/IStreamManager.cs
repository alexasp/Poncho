using SpotifyService.Cargo;

namespace SpotifyService.Model.Interfaces
{
    public interface IStreamManager
    {
        void RequestTrackStream(Track track);
    }
}