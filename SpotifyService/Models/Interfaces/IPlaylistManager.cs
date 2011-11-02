using SpotifyService.Cargo;

namespace SpotifyService.Models.Interfaces
{
    public interface IPlaylistManager
    {
        PlayList SelectedPlayList { get; set; }
    }
}