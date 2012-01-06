using System.Collections.Generic;
using Caliburn.Micro;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Messages;

namespace Poncho.ViewModels.Interfaces
{
    public interface IMainViewModel : IHandle<SearchResultMessage>
    {
        PlayList SelectedPlayList { get; set; }
        string SearchText { get; set; }
        PlaybackStatus PlaybackStatus { get; set; }
        List<Track> SelectedTracks { get; set; }
        List<Track> TrackList { get; set; }
        string Output { get; }
        void Search();
        void PlayPause();
        void PlaySelectedTrack();
        void QueueTracks();
        bool CanPlayPause();
    }
}