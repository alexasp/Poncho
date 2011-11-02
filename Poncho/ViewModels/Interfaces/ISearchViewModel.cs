using System.Collections.Generic;

namespace Poncho.Models
{
    public interface ISearchViewModel
    {
        string Text { get; set; }
        void Search();
        void SearchResultsRetrieved(List<Track> trackList);
    }
}