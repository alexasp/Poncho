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
        private ITrackSubscriber _trackListViewModel;

        [SetUp]
        public void Init()
        {
            _trackListViewModel = MockRepository.GenerateMock<ITrackSubscriber>();
            _musicServices = MockRepository.GenerateMock<IMusicServices>();
            _searchManager = new SearchManager(_musicServices, _trackListViewModel);
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
        public void SearchResultsRetrieved_SendsResultingTracklistToTrackListViewModel()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResult(trackList);
            
            _trackListViewModel.Expect(x => x.TrackList = trackList);

            _searchManager.SearchResultsRetrieved(searchResults);

            _musicServices.VerifyAllExpectations();
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
