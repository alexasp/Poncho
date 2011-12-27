using System;
using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Enums;

namespace SpotifyService.Model.Interfaces
{
    public interface ITrackHandler : ITrackSubscriber
    {
        void PlayTrack(Track track);
        void QueueTracks(List<Track> tracks);
        void QueueTracks(Track track);
        bool HasNextTrack();
        Track NextTrack();
        void SetPlaybackStatus(PlaybackStatus isPlaying);
        event EventHandler ActiveTrackListListeners;
        List<Track> ActiveTrackList { get; }
    }
}