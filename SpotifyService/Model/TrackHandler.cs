using System;
using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Model
{
    public class TrackHandler : ITrackHandler
    {
        private readonly ITrackQueue _trackQueue;
        private readonly IMusicServices _musicServices;


        public TrackHandler(ITrackQueue trackQueue, IMusicServices musicServices)
        {
            _trackQueue = trackQueue;
            _musicServices = musicServices;
        }


        public void PlayTrack(Track track)
        {

            if (track != null)
            {
                if (!track.Playable)
                    throw new ArgumentException("Track was not playable.");
                else
                    _musicServices.PlayTrack(track);
            }
            else
            {
                throw new ArgumentException("Track was null");
            }
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

        public void SearchRetrieved(SearchResult searchResults)
        {
            throw new NotImplementedException();
        }
    }
}