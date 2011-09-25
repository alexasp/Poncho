using System.Collections.Generic;
using Poncho.Models.Cargo;

namespace Poncho.Models.Interfaces
{
    public interface ITrackQueue
    {
        void Enqueue(List<Track> tracks);
        void Enqueue(Track tracks);
        int Length { get; }
        Track Dequeue();
    }
}