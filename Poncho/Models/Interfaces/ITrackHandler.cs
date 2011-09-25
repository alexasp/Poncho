using System.Collections.Generic;
using Poncho.Models.Cargo;

namespace Poncho.Models.Interfaces
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