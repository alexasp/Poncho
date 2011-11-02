using System;
using SpotifyService.Cargo;

namespace SpotifyService.Interfaces
{
    public interface IMusicServices : IDisposable
    {
        void FetchPlaylistTracks(PlayList playlist);
        void Search(string searchText);
        void InitializeSession(string username, string password);
        void SearchRetrieved(SearchResult result);
        void EndSession();
    }
}