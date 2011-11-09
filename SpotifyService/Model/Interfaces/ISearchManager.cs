using SpotifyService.Cargo;

namespace SpotifyService.Models.Interfaces
{
    public interface ISearchManager
    {
        void Search(string text);
        void SearchResultsRetrieved(SearchResult searchResult);
        SearchResult LastSearch { get; }
    }
}