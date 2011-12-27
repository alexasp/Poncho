using System;
using Poncho.ViewModels.Interfaces;
using SpotifyService.Enums;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    public class TrackControlViewModel : ITrackControlViewModel
    {
        private readonly ITrackHandler _trackHandler;

        public TrackControlViewModel(ITrackHandler trackHandler)
        {
            _trackHandler = trackHandler;
        }


        public PlaybackStatus PlaybackStatus { get; set; }

        public void PlayPause()
        {
            PlaybackStatus = PlaybackStatus == PlaybackStatus.Playing ? PlaybackStatus.Paused : PlaybackStatus.Playing;
            _trackHandler.SetPlaybackStatus(PlaybackStatus);
        }


        public void Something()
        {
            int tall = 5;
        }

    }
}