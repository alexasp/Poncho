using System.Collections.Generic;

namespace SpotifyService.Cargo
{
    public class SearchResult
    {
        private readonly List<Track> _trackList;

        public SearchResult(List<Track> trackList)
        {
            _trackList = trackList;
        }

        public List<Track> TrackList { get; set; }
    }
}