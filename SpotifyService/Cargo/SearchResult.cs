using System.Collections.Generic;

namespace SpotifyService.Cargo
{
    public class SearchResult
    {
        public List<Track> TrackList { get; set; }
        public string SearchQuery { get; set; }
        public string DidYouMeanText { get; set; }
        public int TrackCount { get; set; }
        public int TotalTrackCount { get; set; }
        public int AlbumCount { get; set; }

        public SearchResult(List<Track> trackList, string searchQuery, string didYouMeanText, int trackCount, int totalTrackCount, int albumCount)
        {
            TrackList = trackList;
            SearchQuery = searchQuery;
            DidYouMeanText = didYouMeanText;
            TrackCount = trackCount;
            TotalTrackCount = totalTrackCount;
            AlbumCount = albumCount;
        }

        public SearchResult(List<Track> trackList)
        {
            TrackList = trackList;
        }
    }
}