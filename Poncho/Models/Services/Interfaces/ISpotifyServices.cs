namespace Poncho.Models.services
{
    public interface ISpotifyServices
    {
        void FetchPlaylistTracks(PlayList playlist);
        void RequestLogin(string username, string password);
        void Search(string searchText);
    }
}