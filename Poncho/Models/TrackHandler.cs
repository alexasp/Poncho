using System;
using System.Collections.Generic;
using Poncho.Models;
using Poncho.Models.Cargo;
using Poncho.Models.Interfaces;

namespace Poncho.ViewModels
{
    public class TrackHandler : ITrackHandler
    {
        private readonly ITrackStreamPlayer _trackStreamPlayer;
        private readonly ITrackQueue _trackQueue;
        private IStreamManager _streamManager;


        public TrackHandler(IStreamManager streamManager, ITrackStreamPlayer trackStreamPlayer, ITrackQueue trackQueue)
        {
            _streamManager = streamManager;
            _trackStreamPlayer = trackStreamPlayer;
            _trackQueue = trackQueue;
        }


        public void PlayTrack(Track track)
        {

            if (track != null)
                if(!track.Playable)
                    throw new ArgumentException("Track was not playable.");
                else
                    _streamManager.RequestTrackStream(track);
        }

        public void QueueTracks(List<Track> tracks)
        {
            _trackQueue.Enqueue(tracks);
        }

        public void QueueTracks(Track track)
        {
            _trackQueue.Enqueue(track);
        }

        public bool HasNextTrack()
        {
            return _trackQueue.Length > 0;
        }

        public Track NextTrack()
        {
            if (_trackQueue.Length > 0)
                return _trackQueue.Dequeue();
            throw new InvalidOperationException("There was no next track.");
        }

        public void PlayPause(bool isPlaying)
        {
            throw new NotImplementedException();
        }

        public void TrackStreamRetreived(TrackStream trackStream)
        {
            if (trackStream != null) _trackStreamPlayer.PlayStream(trackStream);
        }
    }
}