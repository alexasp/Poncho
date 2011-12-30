using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Model
{
    public class SearchManager : ISearchManager
    {
        private readonly IMusicServices _musicServices;
        private SearchResult _searchResult;

        public SearchManager(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        public void SearchResultsRetrieved(SearchResult searchResults)
        {
            LastSearch = searchResults;
        }

        public SearchResult LastSearch
        {
            get { return _searchResult; }
            private set { _searchResult = value; }
        }

        public void Search(string text)
        {
            _musicServices.Search(text);
        }
    }
}