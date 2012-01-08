using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Managers
{
    public class SearchManager : ISearchManager
    {
        private readonly IMusicServices _musicServices;

        public SearchManager(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        public void SearchResultsRetrieved(SearchResult searchResults)
        {
            LastSearch = searchResults;
        }

        public SearchResult LastSearch { get; private set; }

        public void Search(string text)
        {
            _musicServices.Search(text);
        }
    }
}