using System;
using System.Collections.Generic;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Structs;

namespace SpotifyService.Interfaces
{
    public interface ISpotifyWrapper : IDisposable
    {
        void FetchPlaylistTracks(PlayList playlist);
        sp_error CreateSession();
        void RequestLogin(string username, string password);
        void EndSession();
        void CreateSearch(string search);
        string GetSearchQuery();
        string GetSearchDidYouMean();
        int GetSearchTotalTracksFound();
        int GetSearchCountTracksRetrieved();
        int GetSearchCountAlbumsRetrieved();
        IntPtr GetSearchAlbum(int index);
        IntPtr GetSearchArtist(int index);
        IntPtr GetSearchTrack(int index);
        List<Track> GetLastSearchTracks();
    }
}