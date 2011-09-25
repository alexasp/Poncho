using System;
using System.Collections.Generic;
using Poncho.Models;
using Poncho.Models.Cargo;
using Poncho.Models.Enums;
using Poncho.Models.Interfaces;

namespace Poncho.ViewModels
{
    public class TrackListViewModel : ITrackListViewModel
    {
        private readonly ITrackHandler _trackHandler;
        private readonly IUserFeedbackHandler _userFeedbackHandler;
        private IPlaylistManager _playListManager;

        public Track SelectedTrack
        {
            get { return SelectedTracks[0]; }
            set { SelectedTracks.Clear(); SelectedTracks.Add(value);}
        }

        public List<Track> SelectedTracks { get; set; }

        public List<Track> TrackList { get; set; }


        public TrackListViewModel(ITrackHandler trackHandler, IUserFeedbackHandler userFeedbackHandler, IPlaylistManager playListManager)
        {
            SelectedTracks = new List<Track>();
            _trackHandler = trackHandler;
            _playListManager = playListManager;
            _userFeedbackHandler = userFeedbackHandler;
        }



        public void PlaySelectedTrack()
        {
            if (SelectedTrack.Playable)
                _trackHandler.PlayTrack(SelectedTrack);
            else
                _userFeedbackHandler.Display(UserFeedback.TrackNotPlayable);
        }


        public void QueueTracks()
        {
            foreach (var selectedTrack in SelectedTracks)
            {
                if(selectedTrack.Playable)
                    _trackHandler.QueueTracks(selectedTrack);
            }
        }

        public void OnSelectedPlaylistChanged()
        {
            TrackList = _playListManager.SelectedPlayList.TrackList;
        }
    }
}