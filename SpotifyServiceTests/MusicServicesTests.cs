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
        public void SearchResultsRetrieved_ConstructsSearchResultAndPassesToSearchManager()
        {
            var trackList = new List<Track>();
            var result = new SearchResult(trackList);

            _spotifyWrapper.Stub(x => x.GetLastSearchTracks()).Return(trackList);
            _searchManager.Expect(x => x.SearchResultsRetrieved(Arg<SearchResult>.Is.Anything));

            _musicServices.SearchRetrieved(result);

            _searchManager.VerifyAllExpectations();
        }
    }
}
