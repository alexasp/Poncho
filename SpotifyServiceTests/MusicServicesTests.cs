using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests
{
    //Responsible for interactions with the Spotify library.
    [TestFixture]
    class MusicServicesTests
    {
        private IMusicServices _musicServices;
        private ISearchManager _searchManager;
        private ISpotifyWrapper _spotifyWrapper;

        [SetUp]
        public void Init()
        {
            _searchManager = MockRepository.GenerateStub<ISearchManager>();
            _spotifyWrapper = MockRepository.GenerateMock<ISpotifyWrapper>();
            _musicServices = new MusicServices(_searchManager, _spotifyWrapper);
        }

        [Test]  
        public void InitializeSession_CallsCreateSessionOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";

            _spotifyWrapper.Stub(x => x.CreateSession()).Return(sp_error.SP_ERROR_OK);

            _musicServices.InitializeSession(username, password);

            _spotifyWrapper.AssertWasCalled(x => x.CreateSession());
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
        public void SearchResultsRetrieved_ConstructsSearchResultAndPassesToSearchResultSubscribers()
        {
            var trackList = new List<Track>();

            _spotifyWrapper.Stub(x => x.GetSearchTracks()).Return(trackList);
            _spotifyWrapper.Stub(x => x.GetSearchQuery()).Return("Seigmenn");
            _spotifyWrapper.Stub(x => x.GetSearchDidYouMean()).Return("Seigmen");
            _spotifyWrapper.Stub(x => x.GetSearchCountTracksRetrieved()).Return(40);
            _spotifyWrapper.Stub(x => x.GetSearchTotalTracksFound()).Return(120);
            _spotifyWrapper.Stub(x => x.GetSearchCountAlbumsRetrieved()).Return(8);

            _musicServices.SearchRetrieved();

            _searchManager.AssertWasCalled(x => x.SearchResultsRetrieved(Arg<SearchResult>.Is.Anything));
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

            _searchManager.AssertWasCalled(x => x.SearchResultsRetrieved(Arg<SearchResult>.Matches(
                y => y.TrackList == trackList
                    && y.SearchQuery == searchText
                    && y.DidYouMeanText == didYouMean
                    && y.TrackCount == trackCount
                    && y.TotalTrackCount == totalTrackCount
                    && y.AlbumCount == albumCount
                    )
                ));
        }

        //Is there some way to test for order as well? If there is, it's on mocks, so we're leaving this a mock for now.
        [Test]
        public void PlayTrack_CallsLoadTrackThenPlayTrackOnWrapper()
        {
            var track = new Track(true);
            _spotifyWrapper.Expect(x => x.LoadTrack(track.Handle));
            _spotifyWrapper.Expect(x => x.Play(true));

            _musicServices.PlayTrack(track);

            _spotifyWrapper.VerifyAllExpectations();
        }
    }
}
