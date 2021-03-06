﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poncho.ViewModels.Interfaces;
using SpotifyService;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly ISpotifyServices _spotifyServices;
        public IUserFeedbackHandler UserFeedbackHandler { get; set; }
        public PlaybackStatus PlaybackStatus { get; set; }
        private List<Track> _trackList;
        private const string TrackNotPlayable = "This track is not playable.";
        private const string NoTrackSelected = "No track selected.";
        private const string SearchQueryEmpty = "No search query entered.";
        private const string SearchResultListed = "Search result listed.";
        private const string NoTracksFound = "No tracks found.";
        private string _searchText;
        private string _title = "Poncho";
        private string _output;


        public MainViewModel(ISpotifyServices spotifyServices)
        {
            _spotifyServices = spotifyServices;
            _spotifyServices.EventAggregator.Subscribe(this);

            PlaybackStatus = PlaybackStatus.NoActiveTrack;
            Title = _title;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChangedEventImmediately("Title");
                }
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyOfPropertyChange(() => SearchText);}
        }

        private PlayList _selectedPlayList;

        public PlayList SelectedPlayList
        {
            get { return _selectedPlayList; }
            set { _selectedPlayList = value; NotifyOfPropertyChange(() => SelectedPlayList); }
        }


        public List<Track> TrackList
        {
            get { return _trackList; }
            set { _trackList = value; NotifyOfPropertyChange(() => TrackList); }
        }

        public string Output
        {
            get { return _output; }
            private set { _output = value; NotifyOfPropertyChange(() => Output); }
        }

        public List<Track> SelectedTracks { get; set; }

        public Track SelectedTrack
        {
            get { return SelectedTracks.Count > 0 ? SelectedTracks[0] : null; }
            set { SelectedTracks.Clear(); SelectedTracks.Add(value); }
        }


        public void Search()
        {
            Debug.WriteLine("Search attempted.");
            if (String.IsNullOrEmpty(SearchText))
                Output = SearchQueryEmpty;
            else
                _spotifyServices.Search(SearchText);
        }

        public void PlayPause()
        {
            PlaybackStatus = PlaybackStatus == PlaybackStatus.Playing ? PlaybackStatus.Paused : PlaybackStatus.Playing;
            _spotifyServices.ChangePlaybackStatus(PlaybackStatus);
        }

        public void PlaySelectedTrack()
        {
            if (SelectedTracks.Count > 0)
            {
                if (SelectedTrack.Playable)
                    _spotifyServices.PlayTrack(SelectedTrack);
                else
                    Output = TrackNotPlayable;
            }
            else
            {
                Output = NoTrackSelected;
            }
        }


        public void QueueTracks()
        {
            foreach (var selectedTrack in SelectedTracks)
            {
                if(!selectedTrack.Playable)
                {
                    Output = TrackNotPlayable;
                }
            }
              _spotifyServices.QueueTracks(SelectedTracks);
        }

        public bool CanPlayPause()
        {
            if (PlaybackStatus == PlaybackStatus.NoActiveTrack)
                return false;

            return true;
        }

        public void ActiveTracksChanged(object sender, EventArgs eventArgs)
        {
            var trackHandler = sender as ITrackHandler;
            Debug.Assert(trackHandler != null, "trackHandler != null");
            TrackList = trackHandler.ActiveTrackList;
        }


        public void Handle(SearchResultMessage message)
        {
            if (message.Result.TrackList.Count > 0)
                Output = SearchResultListed;
            else
                Output = NoTracksFound;

            TrackList = message.Result.TrackList;
        }
    }
}
