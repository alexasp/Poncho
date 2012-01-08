using System.Collections.Generic;
using Caliburn.Micro;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Interfaces;

// ReSharper disable InconsistentNaming

namespace SpotifyServiceTests
{
    /// <summary>
    //Responsible for interactions with the Spotify library.
    /// </summary>
    [TestFixture]
    class MusicServicesTests
    {
        private IMusicServices _musicServices;
        private ISpotifyWrapper _spotifyWrapper;
        private IEventAggregator _eventAggregator;

        [SetUp]
        public void Init()
        {
            _eventAggregator = MockRepository.GenerateMock<IEventAggregator>();
            _spotifyWrapper = MockRepository.GenerateMock<ISpotifyWrapper>();
            _musicServices = new MusicServices(_eventAggregator, _spotifyWrapper);
        }

        private Track GetNotPlayableTrack()
        {
            return new Track(0, "name", "artist", "album", false);
        }

        private Track GetPlayableTrack()
        {
            return new Track(0, "name", "artist", "album", true);
        }

        [Test]  
        public void InitializeSession_NoExistingSession_CallsCreateSessionOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";
            _spotifyWrapper.Stub(x => x.CreateSession()).Return(sp_error.SP_ERROR_OK);
            _spotifyWrapper.Stub(x => x.ActiveSession()).Return(false);

            _musicServices.InitializeSession(username, password);

            _spotifyWrapper.AssertWasCalled(x => x.CreateSession());
        }

        [Test]
        public void InitializeSession_ExistingSession_DoesNotCallCreateSessionOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";

            _spotifyWrapper.Stub(x => x.CreateSession()).Return(sp_error.SP_ERROR_OK);
            _spotifyWrapper.Stub(x => x.ActiveSession()).Return(true);

            _musicServices.InitializeSession(username, password);

            _spotifyWrapper.AssertWasNotCalled(x => x.CreateSession());
        }

        [Test]
        public void InitializeSession_RequestsLoginOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";

            _musicServices.InitializeSession(username, password);

            _spotifyWrapper.AssertWasCalled(x => x.RequestLogin(username, password));
        }

        [Test]
        public void Search_CallsCreateSearchOnSpotifyServices()
        {
            string search = "Seigmen";

            _musicServices.Search(search);

            _spotifyWrapper.AssertWasCalled(x => x.CreateSearch(search));
        }


        [Test]
        public void SearchResultsRetrieved_ConstructsSearchResultAndPublishesAsSearchResultMessage()
        {
            var trackList = new List<Track>();

            _spotifyWrapper.Stub(x => x.GetSearchTracks()).Return(trackList);
            _spotifyWrapper.Stub(x => x.GetSearchQuery()).Return("Seigmenn");
            _spotifyWrapper.Stub(x => x.GetSearchDidYouMean()).Return("Seigmen");
            _spotifyWrapper.Stub(x => x.GetSearchCountTracksRetrieved()).Return(40);
            _spotifyWrapper.Stub(x => x.GetSearchTotalTracksFound()).Return(120);
            _spotifyWrapper.Stub(x => x.GetSearchCountAlbumsRetrieved()).Return(8);

            _musicServices.SearchRetrieved();

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<SearchResultMessage >.Is.Anything));
        }

        [Test]
        public void SearchResultsRetrieved_ConstructsSearchResultCorrectly()
        {
            SearchResult searchResult;
            var trackList = new List<Track>();
            var searchText = "Seigmenn";
            var didYouMean = "Seigmen";
            var trackCount = 40;
            var totalTrackCount = 120;
            var albumCount = 8;

            _spotifyWrapper.Stub(x => x.GetSearchTracks()).Return(trackList);
            _spotifyWrapper.Stub(x => x.GetSearchQuery()).Return(searchText);
            _spotifyWrapper.Stub(x => x.GetSearchDidYouMean()).Return(didYouMean);
            _spotifyWrapper.Stub(x => x.GetSearchCountTracksRetrieved()).Return(trackCount);
            _spotifyWrapper.Stub(x => x.GetSearchTotalTracksFound()).Return(totalTrackCount);
            _spotifyWrapper.Stub(x => x.GetSearchCountAlbumsRetrieved()).Return(albumCount);

            _musicServices.SearchRetrieved();

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<SearchResultMessage>.Matches(
                y => y.Result.TrackList == trackList
                    && y.Result.SearchQuery == searchText
                    && y.Result.DidYouMeanText == didYouMean
                    && y.Result.TrackCount == trackCount
                    && y.Result.TotalTrackCount == totalTrackCount
                    && y.Result.AlbumCount == albumCount
                    )
                    )
                );
        }

        [Test]
        public void LoginResponseRetrieved_LoginSuccesful_PublishesLoginResultMessageWithSuccess()
        {
            _musicServices.LoginResponseRetrieved(sp_error.SP_ERROR_OK);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<LoginResultMessage>.Matches(y => y.Success == true)));
        }

        [Test]
        public void LoginResponseRetrieved_LoginNotSuccesful_PublishesLoginResultMessageWithFailure()
        {
            _musicServices.LoginResponseRetrieved(sp_error.SP_ERROR_BAD_USERNAME_OR_PASSWORD);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<LoginResultMessage>.Matches(y => y.Success == false)));
        }

        //Is there some way to test for order as well? If there is, it's on mocks, so we're leaving this a mock for now.
        [Test]
        public void PlayTrack_CallsLoadTrackThenPlayTrackOnWrapper()
        {
            var track = GetPlayableTrack();
            _spotifyWrapper.Expect(x => x.LoadTrack(track.Handle));
            _spotifyWrapper.Expect(x => x.Play(true));

            _musicServices.PlayTrack(track);

            _spotifyWrapper.VerifyAllExpectations();
        }
        
        [Test]
        public void SetPlaybackStatus_ToPaused_CallsSpotifyWrapperPlayWithFalse()
        {
            _spotifyWrapper.Expect(x => x.Play(false));

            _musicServices.SetPlaybackStatus(PlaybackStatus.Paused);

            _spotifyWrapper.VerifyAllExpectations();
        }

        [Test]
        public void SetPlaybackStatus_ToPlaying_CallsSpotifyWrapperPlayWithTrue()
        {
            _spotifyWrapper.Expect(x => x.Play(true));

            _musicServices.SetPlaybackStatus(PlaybackStatus.Playing);

            _spotifyWrapper.VerifyAllExpectations();
        }

    }
}
