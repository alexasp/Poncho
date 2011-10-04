using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Poncho.Models.Cargo;
using Poncho.Models.Interfaces;
using Poncho.Models.services;
using Poncho.ViewModels;
using Rhino.Mocks;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class SearchManagerTests
    {
        private ISpotifyServices _spotifyServices;
        private ISearchManager _searchManager;
        private ITrackListViewModel _trackListViewModel;

        [SetUp]
        public void Init()
        {
            _trackListViewModel = MockRepository.GenerateMock<ITrackListViewModel>();
            _spotifyServices = MockRepository.GenerateMock<ISpotifyServices>();
            _searchManager = new SearchManager(_spotifyServices, _trackListViewModel);
        }

        [Test]
        public void Search_ValidSearchText_CallsSearchOnISpotifyServices()
        {
            var searchText = "Seigmen";
            _spotifyServices.Expect(x => x.Search(searchText));

            _searchManager.Search(searchText);

            _spotifyServices.VerifyAllExpectations();
        }

        [Test]
        public void SearchResultsRetrieved_SendsResultingTracklistToTrackListViewModel()
        {
            var trackList = new List<Track>();
            var searchResults = new SearchResults(trackList);
            
            _trackListViewModel.Expect(x => x.TrackList = trackList);

            _searchManager.SearchResultsRetrieved(searchResults);

            _spotifyServices.VerifyAllExpectations();
        }

        public SearchManagerTests(int i)
    }
}
