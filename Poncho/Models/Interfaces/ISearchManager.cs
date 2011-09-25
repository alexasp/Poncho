using System.Collections.Generic;
using Poncho.Models.Cargo;

namespace Poncho.Models.Interfaces
{
    public interface ISearchManager
    {
        void Search(string text);
        void SearchResultsRetrieved(List<Track> trackList);
    }
}