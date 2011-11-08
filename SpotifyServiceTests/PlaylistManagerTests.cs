using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class PlaylistManagerTests
    {
        private IPlaylistManager _playlistManager;
        private IMusicServices _musicServices;

        [SetUp]
        public void Init()
        {
            _musicServices = MockRepository.GenerateStub<IMusicServices>();
            _playlistManager = new PlaylistManager(_musicServices);
        }

        [Test]
        public void SelectedPlayListSet_ValidPlayList_RequestsTrackListFromSpotifyWrapper()
        {
            var playlist = new PlayList();

            _playlistManager.SelectedPlayList = playlist;

            _musicServices.AssertWasCalled(x => x.FetchPlaylistTracks(playlist));
        }
    }
}
