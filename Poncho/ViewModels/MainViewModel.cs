using System;
using System.Collections.Generic;
using System.Diagnostics;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen, IMainViewModel
    {
        private readonly ITrackHandler _trackHandler;
        private readonly ISearchManager _searchManager;
        private readonly IPlaylistManager _playListManager;
        public IUserFeedbackHandler UserFeedbackHandler { get; set; }
        private string _title = "Poncho";
        private PlayList _selectedPlayList;
        private IUserFeedbackHandler _userFeedbackHandler;


        public MainViewModel(ITrackHandler trackHandler, ISearchManager searchManager, IPlaylistManager playListManager, IUserFeedbackHandler userFeedbackHandler)
        {
            _trackHandler = trackHandler;
            _searchManager = searchManager;
            _playListManager = playListManager;
            _userFeedbackHandler = userFeedbackHandler;

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

        public string SearchText { get; set; }

        public PlayList SelectedPlayList
        {
            get { return _selectedPlayList; }
            set
            {
                _selectedPlayList = value;
                _playListManager.SelectedPlayList = value;
            }
        }

        public PlaybackStatus PlaybackStatus { get; set; }

        public List<Track> SelectedTracks { get; set; }

        public Track SelectedTrack
        {
            //Hm. Null makes sense in this context, doesn't it?
            get { return SelectedTracks.Count > 0 ? SelectedTracks[0] : null; }
            set { SelectedTracks.Clear(); SelectedTracks.Add(value); }
        }

        public object TrackList { get; set; }


        public void Search()
        {
            if (SearchText == "")
                _userFeedbackHandler.Display(UserFeedback.NoSearchTextEntered);
            else
                _searchManager.Search(SearchText);
        }

        public void PlayPause()
        {
            PlaybackStatus = PlaybackStatus == PlaybackStatus.Playing ? PlaybackStatus.Paused : PlaybackStatus.Playing;
            _trackHandler.SetPlaybackStatus(PlaybackStatus);
        }

        public void PlaySelectedTrack()
        {
            if (SelectedTracks.Count > 0)
            {
                if (SelectedTrack.Playable)
                    _trackHandler.PlayTrack(SelectedTrack);
                else
                    _userFeedbackHandler.Display(UserFeedback.TrackNotPlayable);
            }
            else
            {
                _userFeedbackHandler.Display(UserFeedback.NoTrackSelected);
            }
        }


        public void QueueTracks()
        {
            foreach (var selectedTrack in SelectedTracks)
            {
                if (selectedTrack.Playable)
                    _trackHandler.QueueTracks(selectedTrack);
            }
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
