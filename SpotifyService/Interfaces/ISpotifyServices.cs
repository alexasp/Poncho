using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Enums;

namespace SpotifyService.Interfaces
{
    public interface ISpotifyServices
    {
        void Search(string searchText);
        void PlayTrack(Track track);
        void QueueTracks(List<Track> trackList);
        void ChangePlaybackStatus(PlaybackStatus playing);
    }
}