using System;
using Poncho.Models.Interfaces;
using Poncho.Models.services;

namespace Poncho.Models
{
    public class PlaylistManager : IPlaylistManager
    {
        private readonly ISpotifyServices _spotifyServices;
        private PlayList _playlist;

        public PlaylistManager(ISpotifyServices spotifyServices)
        {
            _spotifyServices = spotifyServices;
        }

        public PlayList SelectedPlayList
        {
            get { return _playlist; }
            set { _playlist = value; _spotifyServices.FetchPlaylistTracks(_playlist);}
        }
    }
}