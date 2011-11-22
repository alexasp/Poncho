using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Model.Interfaces;

namespace Poncho.ViewModels
{
    public interface ITrackListViewModel : IActiveTracksListener
    {
        void PlaySelectedTrack();
        Track SelectedTrack { get; set; }
        List<Track> TrackList { get; set; }
        List<Track> SelectedTracks { get; set; }
        void QueueTracks();
    }
}