using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Models.Interfaces;

namespace SpotifyService.Models
{
    public class TrackQueue : ITrackQueue
    {
        private List<Track> _queue;

        public TrackQueue()
        {
            _queue = new List<Track>();
        }

        public void Enqueue(Track track)
        {
            _queue.Insert(0,track);
        }

        public int Length
        {
            get { return _queue.Count; }
        }

        public List<Track> TrackList
        {
            get { return new List<Track>(_queue); }
        }

        public void Append(Track track)
        {
            _queue.Add(track);
        }

        public Track Dequeue()
        {
            int lastItemIndex = _queue.Count - 1;
            Track dequeuedTrack = _queue[lastItemIndex];
            _queue.RemoveAt(lastItemIndex);
            return dequeuedTrack;
        }

        public void Enqueue(List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                _queue.Insert(0, track);
            }
        }
    }
}