using System.Collections.Generic;
using SpotifyService.Cargo;

namespace Poncho.ViewModels
{
    public interface ITrackListViewModel
    {
        void PlaySelectedTrack();
        Track SelectedTrack { get; set; }
        List<Track> TrackList { get; set; }
    }
}