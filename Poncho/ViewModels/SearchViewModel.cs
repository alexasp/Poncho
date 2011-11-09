using System;
using System.Collections.Generic;
using Poncho.ViewModels;
using SpotifyService.Cargo;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.Models
{
    public class SearchViewModel : ISearchViewModel
    {
        private readonly ISearchManager _searchManager;
        private readonly IUserFeedbackHandler _userFeedbackHandler;
        private ITrackListViewModel _trackListViewModel;

        public SearchViewModel(ISearchManager searchManager, IUserFeedbackHandler userFeedbackHandler, ITrackListViewModel trackListViewModel)
        {
            _searchManager = searchManager;
            _trackListViewModel = trackListViewModel;
            _userFeedbackHandler = userFeedbackHandler;
            Text = "";
        }

        public string Text { get; set; }

        public void Search()
        {
            if (Text == "")
                _userFeedbackHandler.Display(UserFeedback.NoSearchTextEntered);
            else
                _searchManager.Search(Text);
        }


        public void SearchResultsRetrieved(List<Track> trackList)
        {
            throw new NotImplementedException();
        }
    }
}