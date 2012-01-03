using System;
using System.Diagnostics;
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

        public MusicServices(IEventAggregator eventAggregator, ISpotifyWrapper spotifyWrapper)
        {
            _eventAggregator = eventAggregator;
            _spotifyWrapper = spotifyWrapper;
            _spotifyWrapper.SearchRetrieved += SearchRetrieved;
            _spotifyWrapper.LoginResponseRetrieved += LoginResponseRetrieved;
        }

        public void SetPlaybackStatus(PlaybackStatus paused)
        {
            throw new NotImplementedException();
        }

        public void LoginResponseRetrieved(sp_error error)
        {
            Debug.WriteLine("Publishing login result.");

            bool success = error == sp_error.SP_ERROR_OK;


            _eventAggregator.Publish(new LoginResultMessage(success, error));
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
            if (!_spotifyWrapper.ActiveSession())
            {
                sp_error error = _spotifyWrapper.CreateSession();
                Debug.WriteLine(error);
                if (error == (UInt32) sp_error.SP_ERROR_OK)
                    _spotifyWrapper.RequestLogin(username, password);
            }
            else
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
