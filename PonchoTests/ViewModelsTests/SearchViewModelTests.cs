using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Poncho.ViewModels;
using Rhino.Mocks;
using SpotifyService.Models.Enums;
using SpotifyService.Models.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    public class SearchViewModelTests
    {
        private ISearchManager _searchManager;
        private ISearchViewModel _searchViewModel;
        private IUserFeedbackHandler _userFeedbackHandler;
        private ITrackListViewModel _trackListViewModel;

        [SetUp]
        public void Init()
        {
            _trackListViewModel = MockRepository.GenerateMock<ITrackListViewModel>();
            _userFeedbackHandler = MockRepository.GenerateMock<IUserFeedbackHandler>();
            _searchManager = MockRepository.GenerateMock<ISearchManager>();
            _searchViewModel = new SearchViewModel(_searchManager, _userFeedbackHandler, _trackListViewModel);
        }

        [Test]
        public void Search_ContainsSearchText_SendsSearchToSearchHandler()
        {
            _searchViewModel.Text = "SomeSearch";

            _searchManager.Expect(x => x.Search(_searchViewModel.Text));

            _searchViewModel.Search();
            
            _searchManager.VerifyAllExpectations();
        }

        [Test]
        public void Search_ContainsNoText_MessagesUserFeedbackHandler()
        {
            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.NoSearchTextEntered));

            _searchViewModel.Text = "";

            _searchViewModel.Search();
        }

        

    }
}
