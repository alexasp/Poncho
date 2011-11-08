using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class SearchManagerTests
    {
        private IMusicServices _musicServices;
        private ISearchManager _searchManager;
        private ITrackSubscriber _trackSubscriber;

        [SetUp]
        public void Init()
        {
            _trackSubscriber = MockRepository.GenerateMock<ITrackSubscriber>();
            _musicServices = MockRepository.GenerateMock<IMusicServices>();
            _searchManager = new SearchManager(_musicServices, _trackSubscriber);
        }

        [Test]
        public void Search_ValidSearchText_CallsSearchOnISpotifyServices()
        {
            var searchText = "Seigmen";
            _musicServices.Expect(x => x.Search(searchText));

            _searchManager.Search(searchText);

            _musicServices.VerifyAllExpectations();
        }

        [Test]
        public void SearchResultsRetrieved_PassesToSearchRetrievedSubscribers()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResult(trackList);

            _trackSubscriber.Expect(x => x.TrackList = trackList);

            _searchManager.SearchResultsRetrieved(searchResults);

            _musicServices.VerifyAllExpectations();
        }

        [Test]
        public void SearchResultsRetrieved_SetsLastSearchPropertyToResult()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResult(trackList);

            _trackSubscriber.Expect(x => x.SearchRetrieved(searchResults));

            _searchManager.SearchResultsRetrieved(searchResults);

            _trackSubscriber.VerifyAllExpectations();
        }

    }
}
