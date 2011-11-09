using SpotifyService.Cargo;

namespace SpotifyService.Model.Interfaces
{
    public interface IPlaylistManager
    {
        PlayList SelectedPlayList { get; set; }
    }
}