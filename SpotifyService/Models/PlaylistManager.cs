using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyService.Models
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly IMusicServices _musicServices;
        private PlayList _playlist;

        public PlaylistManager(IMusicServices musicServices)
        {
            _musicServices = musicServices;
        }

        public PlayList SelectedPlayList
        {
            get { return _playlist; }
            set { _playlist = value; _musicServices.FetchPlaylistTracks(_playlist);}
        }
    }
}