using SpotifyService.Cargo;

namespace SpotifyService.Messages
{
    public class SearchResultMessage
    {
        public SearchResult Result { get; set; }

        public SearchResultMessage(SearchResult Result)
        {
            this.Result = Result;
        }
    }
}