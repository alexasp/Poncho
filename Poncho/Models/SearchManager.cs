using System;
using System.Collections.Generic;
using Poncho.Models.Cargo;
using Poncho.Models.Interfaces;
using Poncho.Models.services;
using Poncho.ViewModels;

namespace Poncho.Models
{
    public class SearchManager : ISearchManager
    {
        private readonly ISpotifyServices _spotifyServices;
        private readonly ITrackListViewModel _trackListViewModel;

        public SearchManager(ISpotifyServices spotifyServices, ITrackListViewModel trackListViewModel)
        {
            _spotifyServices = spotifyServices;
            _trackListViewModel = trackListViewModel;
        }

        public void SearchResultsRetrieved(SearchResults searchResults)
        {
            _trackListViewModel.TrackList = searchResults.TrackList;
        }

        public void Search(string text)
        {
            _spotifyServices.Search(text);
        }
    }
}