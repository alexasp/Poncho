using System;
using Caliburn.Micro;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Interfaces;

namespace SpotifyService
{
    public class MusicServices : IMusicServices
    {
        private readonly IEventAggregator _eventAggregator;
        private ISpotifyWrapper _spotifyWrapper;

        public void SetPlaybackStatus(PlaybackStatus paused)
        {
            throw new NotImplementedException();
        }

        public void LoginResponseRetrieved(sp_error error)
        {

            bool success = error == sp_error.SP_ERROR_OK;

            //Kill Spotify session if login failed.
            if (!success)
                _spotifyWrapper.EndSession();

            _eventAggregator.Publish(new LoginResultMessage(success));
        }

        public MusicServices(IEventAggregator eventAggregator, ISpotifyWrapper spotifyWrapper)
        {
            _eventAggregator = eventAggregator;
            _spotifyWrapper = spotifyWrapper;
            _spotifyWrapper.SearchRetrieved += SearchRetrieved;
            _spotifyWrapper.LoginResponseRetrieved += LoginResponseRetrieved;
        }

        public void SearchRetrieved()
        {
            var searchQuery = _spotifyWrapper.GetSearchQuery();
            var didYouMeanText = _spotifyWrapper.GetSearchDidYouMean();
            var trackCount = _spotifyWrapper.GetSearchCountTracksRetrieved();
            var totalTrackCount = _spotifyWrapper.GetSearchTotalTracksFound();
            var albumCount = _spotifyWrapper.GetSearchCountAlbumsRetrieved();
            var tracks = _spotifyWrapper.GetSearchTracks();

            _eventAggregator.Publish(new SearchResultMessage(
                new SearchResult(tracks, searchQuery, didYouMeanText, trackCount, totalTrackCount, albumCount)
                ));
        }

        

        public void PlayTrack(Track i)
        {
            _spotifyWrapper.LoadTrack(i.Handle);
            _spotifyWrapper.Play(true);
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
