using System.Collections.Generic;
using SpotifyService.Cargo;

namespace SpotifyService.Models.Interfaces
{
    public interface ITrackHandler
    {
        void PlayTrack(Track track);
        void QueueTracks(List<Track> tracks);
        void QueueTracks(Track track);
        bool HasNextTrack();
        Track NextTrack();
        void PlayPause(bool isPlaying);
    }
}