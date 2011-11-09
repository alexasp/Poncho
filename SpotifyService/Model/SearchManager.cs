using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyService.Model
{
    public class SearchManager : ISearchManager
    {
        private readonly IMusicServices _musicServices;
        private readonly ITrackSubscriber _trackSubsriber;
        private SearchResult _searchResult;

        public SearchManager(IMusicServices musicServices, ITrackSubscriber trackSubsriber)
        {
            _musicServices = musicServices;
            _trackSubsriber = trackSubsriber;
        }

        public void SearchResultsRetrieved(SearchResult searchResults)
        {
            _trackSubsriber.SearchRetrieved(searchResults);
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