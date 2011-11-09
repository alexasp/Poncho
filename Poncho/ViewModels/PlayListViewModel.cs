using System;
using SpotifyService.Cargo;
using SpotifyService.Model.Interfaces;

namespace Poncho.Models
{
    public class PlayListViewModel
    {
        private PlayList _selectedPlayList;
        private readonly IPlaylistManager _playListManager;

        public PlayListViewModel(IPlaylistManager playListManager)
        {
            _playListManager = playListManager;
        }

        public PlayList SelectedPlayList
        {
            get { return _selectedPlayList; }
            set
            {
                _selectedPlayList = value;
                _playListManager.SelectedPlayList = value;
            }
        }
    }
}