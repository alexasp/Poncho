using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Model;
using SpotifyService.Model.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class TrackHandlerTests
    {

        private ITrackHandler _trackHandler;
        private ITrackQueue _trackQueue;
        private IMusicServices _musicServices;

        [SetUp]
        public void Init()
        {
            _trackQueue = MockRepository.GenerateStub<ITrackQueue>();
            _musicServices = MockRepository.GenerateStub<IMusicServices>();
            _trackHandler = new TrackHandler(_trackQueue, _musicServices);
        }

        [Test]
        public void PlayTrack_PlayableTrack_CallsPlayTrackOnMusicServices()
        {
            var track = new Track(true);

            _trackHandler.PlayTrack(track);

            _musicServices.AssertWasCalled(x => x.PlayTrack(track));
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void PlayTrack_TrackNotPlayable_ThrowsInvalidArgumentException()
        {
            var track = new Track(false);

            _trackHandler.PlayTrack(track);
        }

        [Test]
        public void QueueTrack_TrackPlayable_AddsTrackToQueue()
        {
            var track = new Track(true);

            _trackHandler.QueueTracks(track);

            _trackQueue.AssertWasCalled(x => x.Enqueue(track));
        }

        [Test]
        public void HasNextTrack_NextTrackAvailable_ReturnTrue()
        {
            _trackQueue.Stub(x => x.Length).Return(1);

            Assert.True(_trackHandler.HasNextTrack());
        }

        [Test]
        public void NextTrack_NextTrackAvailable_ReturnsNextTrack()
        {
            var track = new Track(true);
            _trackQueue.Stub(x => x.Dequeue()).Return(track);
            _trackQueue.Stub(x => x.Length).Return(1);

            Assert.AreEqual(_trackHandler.NextTrack(), track);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [Test]
        public void NextTrack_NoTrackAvailable_ThrowsInvalidOperationException()
        {
            _trackQueue.Stub(x => x.Length).Return(0);
            _trackHandler.NextTrack();
        }

       

        [Test]
        public void OnSelectedPlayListChanged_GetsSelectedPlaylistFromPlaylistManager()
        {
            //_playListManager.Expect(x => x.SelectedPlayList);

            //_trackListViewModel.OnSelectedPlaylistChanged();

            Assert.Fail("This test needs to be changed to using an event. Perhaps there should be some central point for tracks to arrive from SearchManager and PlayListManager? Or is this class that point?");

            //_playListManager.VerifyAllExpectations();
        }

        [Test]
        public void OnSelectedPlayListChanged_FillsTrackListWithSelectedPlayList()
        {
            //var playList = new PlayList();
            //playList.TrackList = new List<Track>() { new Track(true), new Track(false) };
            //_playListManager.Stub(x => x.SelectedPlayList).Return(playList);

            //_trackListViewModel.

            Assert.Fail();

            //Assert.AreEqual(playList.TrackList, _trackListViewModel.TrackList);

        }
    }
}
    