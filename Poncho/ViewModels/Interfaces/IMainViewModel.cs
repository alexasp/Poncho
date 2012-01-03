using System;
using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        PlayList SelectedPlayList { get; set; }
        string SearchText { get; set; }
        PlaybackStatus PlaybackStatus { get; set; }
        List<Track> SelectedTracks { get; set; }
        object TrackList { get; set; }
        void Search();
        void PlayPause();
        void PlaySelectedTrack();
        void QueueTracks();
    }
}