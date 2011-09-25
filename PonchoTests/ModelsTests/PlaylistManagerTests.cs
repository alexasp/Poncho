using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Poncho.Models.Interfaces;
using Poncho.Models.services;
using Rhino.Mocks;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class PlaylistManagerTests
    {
        private IPlaylistManager _playlistManager;
        private ISpotifyServices _spotifyServices;

        [SetUp]
        public void Init()
        {
            _spotifyServices = MockRepository.GenerateMock<ISpotifyServices>();
            _playlistManager = new PlaylistManager(_spotifyServices);
        }

        [Test]
        public void SelectedPlayListSet_ValidPlayList_RequestsTrackListFromSpotifyWrapper()
        {
            var playlist = new PlayList();

            _spotifyServices.Expect(x => x.FetchPlaylistTracks(playlist));

            _playlistManager.SelectedPlayList = playlist;
        }
    }
}
