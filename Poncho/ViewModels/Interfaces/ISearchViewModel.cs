using System.Collections.Generic;
using Poncho.Models.Cargo;

namespace Poncho.Models
{
    public interface ISearchViewModel
    {
        string Text { get; set; }
        void Search();
        void SearchResultsRetrieved(List<Track> trackList);
    }
}