using System;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class TrackHandlerTests
    {

        private TrackHandler _trackHandler;
        private ITrackQueue _trackQueue;
        private IMusicServices _musicServices;

        [SetUp]
        public void Init()
        {
            _trackQueue = MockRepository.GenerateMock<ITrackQueue>();
            _musicServices = MockRepository.GenerateMock<IMusicServices>();
            _trackHandler = new TrackHandler(_trackQueue);
        }

        [Test]
        public void PlayTrack_PlayableTrack_CallsPlayTrackOnMusicServices()
        {
            var track = new Track(true);

            _musicServices.Expect(x => x.PlayTrack(track));

            _trackHandler.PlayTrack(track);

            _musicServices.VerifyAllExpectations();
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
            _trackQueue.Expect(x => x.Enqueue(track));

            _trackHandler.QueueTracks(track);

            _trackQueue.VerifyAllExpectations();
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
    }
}
    