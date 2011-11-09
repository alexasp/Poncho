using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model;
using SpotifyService.Model.Interfaces;

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
            _trackSubscriber = MockRepository.GenerateStub<ITrackSubscriber>();
            _musicServices = MockRepository.GenerateStub<IMusicServices>();
            _searchManager = new SearchManager(_musicServices, _trackSubscriber);
        }

        [Test]
        public void Search_ValidSearchText_CallsSearchOnIMusicServices()
        {
            var searchText = "Seigmen";

            _searchManager.Search(searchText);

            _musicServices.AssertWasCalled(x => x.Search(searchText));
        }

        [Test]
        public void SearchResultsRetrieved_PassesToITrackSubscriber()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResult(trackList);

            _searchManager.SearchResultsRetrieved(searchResults);

            _trackSubscriber.AssertWasCalled(x => x.SearchRetrieved(searchResults));
        }

        [Test]
        public void SearchResultsRetrieved_SetsLastSearchPropertyToResult()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResult(trackList);

            _searchManager.SearchResultsRetrieved(searchResults);

            Assert.AreEqual(searchResults, _searchManager.LastSearch);
        }

    }
}
