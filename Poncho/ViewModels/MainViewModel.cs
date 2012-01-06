using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly ISpotifyServices _spotifyServices;
        public IUserFeedbackHandler UserFeedbackHandler { get; set; }
        private string _title = "Poncho";


        public MainViewModel(ISpotifyServices spotifyServices)
        {
            _spotifyServices = spotifyServices;

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

        private string _searchText;
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

        public PlaybackStatus PlaybackStatus { get; set; }
        private List<Track> _trackList;
        private const string TrackNotPlayable = "This track is not playable.";
        private const string NoTrackSelected = "No track selected.";
        private const string SearchQueryEmpty = "No search query entered.";

        public List<Track> TrackList
        {
            get { return _trackList; }
            set { _trackList = value; NotifyOfPropertyChange(() => TrackList); }
        }

        public string Output { get; private set; }

        public List<Track> SelectedTracks { get; set; }

        public Track SelectedTrack
        {
            get { return SelectedTracks.Count > 0 ? SelectedTracks[0] : null; }
            set { SelectedTracks.Clear(); SelectedTracks.Add(value); }
        }


        public void Search()
        {
            Debug.WriteLine("Search attempted.");
            if (SearchText == "")
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
              _spotifyServices.QueueTracks(SelectedTracks);
        }

        public void ActiveTracksChanged(object sender, EventArgs eventArgs)
        {
            var trackHandler = sender as ITrackHandler;
            Debug.Assert(trackHandler != null, "trackHandler != null");
            TrackList = trackHandler.ActiveTrackList;
        }

        public void SearchRetrieved(SearchResult searchResults)
        {
            throw new NotImplementedException();
        }
    }
}
