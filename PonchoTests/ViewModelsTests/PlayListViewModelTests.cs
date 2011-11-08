using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Models.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class PlayListViewModelTests
    {
        private IPlaylistManager _playListManager;
        private PlayListViewModel _playListViewModel;

        [SetUp]
        public void Init()
        {
            _playListManager = MockRepository.GenerateMock<IPlaylistManager>();
            _playListViewModel = new PlayListViewModel(_playListManager);
        }

        [Test]
        public void PropertySelectedPlaylistChanged_ValidPlaylist_SetsSelectedPlayListOnPlayListManager()
        {
            var playList = new PlayList();

            _playListManager.Expect(x => x.SelectedPlayList = playList);

            _playListViewModel.SelectedPlayList = playList;
        }


    }
}
