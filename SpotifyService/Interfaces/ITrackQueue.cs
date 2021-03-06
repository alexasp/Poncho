using System.Collections.Generic;
using SpotifyService.Cargo;

namespace SpotifyService.Model.Interfaces
{
    public interface ITrackQueue
    {
        void Enqueue(List<Track> tracks);
        void Enqueue(Track tracks);
        int Length { get; }
        Track Dequeue();
    }
}