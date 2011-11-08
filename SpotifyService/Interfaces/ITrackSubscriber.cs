using System.Collections.Generic;
using SpotifyService.Cargo;

namespace SpotifyService.Interfaces
{
    public interface ITrackSubscriber
    {
        List<Track> TrackList { get; set; }
        void SearchRetrieved(SearchResult searchResults);
    }
}