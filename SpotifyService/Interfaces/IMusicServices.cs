using System;
using SpotifyService.Cargo;
using SpotifyService.Enums;

namespace SpotifyService.Interfaces
{
    public interface IMusicServices : IDisposable
    {
        void FetchPlaylistTracks(PlayList playlist);
        void Search(string searchText);
        void InitializeSession(string username, string password);
        void EndSession();
        void SearchRetrieved();
        void PlayTrack(Track i);
        void SetPlaybackStatus(PlaybackStatus paused);
        void LoginResponseRetrieved(sp_error error);
    }
}