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
            _searchManager = MockRepository.GenerateMock<ISearchManager>();
            _spotifyWrapper = MockRepository.GenerateMock<ISpotifyWrapper>();
            _musicServices = new MusicServices(_searchManager, _spotifyWrapper);
        }

        [Test]  
        public void InitializeSession_CreatesOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";

            _spotifyWrapper.Expect(x => x.CreateSession()).Return(sp_error.SP_ERROR_OK);

            _musicServices.InitializeSession(username, password);
        }

        [Test]
        public void InitializeSession_RequestsLoginOnSpotifyServices()
        {
            string username = "baldi";
            string password = "123321";

            _spotifyWrapper.Expect(x => x.RequestLogin(username, password));

            _musicServices.InitializeSession(username, password);
        }

        [Test]
        public void Search_CallsCreateSearchOnSpotifyServices()
        {
            string search = "Seigmen";

            _spotifyWrapper.Expect(x => x.CreateSearch(search));

            _musicServices.Search(search);
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

            _searchManager.Expect(x => x.SearchResultsRetrieved(Arg<SearchResult>.Is.Anything));

            _musicServices.SearchRetrieved();

            _searchManager.VerifyAllExpectations();
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

            _searchManager.Expect(x => x.SearchResultsRetrieved(Arg<SearchResult>.Matches(
                y => y.TrackList == trackList 
                    && y.SearchQuery == searchText
                    && y.DidYouMeanText == didYouMean
                    && y.TrackCount == trackCount
                    && y.TotalTrackCount == totalTrackCount
                    && y.AlbumCount == albumCount
                    )
                ));


            _musicServices.SearchRetrieved();


            _searchManager.VerifyAllExpectations();
        }

        [Test]
        public void PlayTrack_CallsLoadTrackThenPlayTrackOnWrapper()
        {
            int trackHandle = 135;
            _spotifyWrapper.Expect(x => x.LoadTrack(trackHandle));
            _spotifyWrapper.Expect(x => x.Play(true));

            _musicServices.PlayTrack(trackHandle);

            _spotifyWrapper.VerifyAllExpectations();
        }
    }
}
