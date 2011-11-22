using System.Collections.Generic;
using SpotifyService.Cargo;

namespace SpotifyService.Model.Interfaces
{
    public interface ITrackSubscriber
    {
        void SearchRetrieved(SearchResult searchResults);
    }
}