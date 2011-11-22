using SpotifyService.Cargo;

namespace SpotifyService.Model.Interfaces
{
    public interface ISearchManager
    {
        void Search(string text);
        void SearchResultsRetrieved(SearchResult searchResult);
        SearchResult LastSearch { get; }
    }
}