using System;
using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Models.Interfaces;

namespace SpotifyService.Models
{
    public class TrackHandler : ITrackHandler
    {
        private readonly ITrackQueue _trackQueue;
        private IStreamManager _streamManager;


        public TrackHandler(IStreamManager streamManager, ITrackQueue trackQueue)
        {
            _streamManager = streamManager;
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

    }
}