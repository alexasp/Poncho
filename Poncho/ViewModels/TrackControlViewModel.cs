using System;
using Poncho.ViewModels.Interfaces;
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

        public bool IsPlaying { get; set; }

        public void PlayPause()
        {
            IsPlaying = !IsPlaying;
            _trackHandler.PlayPause(IsPlaying);
        }


        public void Something()
        {
            int tall = 5;
        }

    }
}