using System;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyService
{
    public class MusicServices : IMusicServices
    {
        private readonly ISearchManager _searchManager;
        private ISpotifyWrapper _spotifyWrapper;

        public MusicServices(ISearchManager searchManager, ISpotifyWrapper spotifyWrapper)
        {
            _searchManager = searchManager;
            _spotifyWrapper = spotifyWrapper;
            _spotifyWrapper.SearchRetrieved += SearchRetrieved;
        }

        public void SearchRetrieved()
        {
            var searchQuery = _spotifyWrapper.GetSearchQuery();
            var didYouMeanText = _spotifyWrapper.GetSearchDidYouMean();
            var trackCount = _spotifyWrapper.GetSearchCountTracksRetrieved();
            var totalTrackCount = _spotifyWrapper.GetSearchTotalTracksFound();
            var albumCount = _spotifyWrapper.GetSearchCountAlbumsRetrieved();
            var tracks = _spotifyWrapper.GetSearchTracks();

            _searchManager.SearchResultsRetrieved(new SearchResult(tracks, searchQuery, didYouMeanText, trackCount, totalTrackCount, albumCount));
        }

        public void PlayTrack(int i)
        {
            throw new NotImplementedException();
        }

        public void InitializeSession(string username, string password)
        {
            sp_error error = _spotifyWrapper.CreateSession();

            if(error == (UInt32)sp_error.SP_ERROR_OK)
                _spotifyWrapper.RequestLogin(username, password);
        }


        public void EndSession()
        {
            _spotifyWrapper.EndSession();
        }

        public void FetchPlaylistTracks(PlayList playlist)
        {
            throw new NotImplementedException();
        }

        public void Search(string searchText)
        {
            _spotifyWrapper.CreateSearch(searchText);
        }


        public void Dispose()
        {
            _spotifyWrapper.Dispose();
        }
    }


}
