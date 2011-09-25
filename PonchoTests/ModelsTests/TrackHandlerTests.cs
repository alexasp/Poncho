using System;
using NUnit.Framework;
using Poncho.Models.Cargo;
using Poncho.Models.Interfaces;
using Poncho.Models;
using Poncho.ViewModels;
using Rhino.Mocks;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class TrackHandlerTests
    {
        private IStreamManager _streamManager;
        private TrackHandler _trackHandler;
        private ITrackStreamPlayer _trackStreamPlayer;
        private ITrackQueue _trackQueue;

        [SetUp]
        public void Init()
        {
            _streamManager = MockRepository.GenerateMock<IStreamManager>();
            _trackStreamPlayer = MockRepository.GenerateMock<ITrackStreamPlayer>();
            _trackQueue = MockRepository.GenerateMock<ITrackQueue>();
            _trackHandler = new TrackHandler(_streamManager, _trackStreamPlayer, _trackQueue);
        }

        [Test]
        public void PlayTrack_PlayableTrack_RequestsTrackStreamFromStreamManager()
        {
            var track = new Track(true);

            _streamManager.Expect(x => x.RequestTrackStream(track));

            _trackHandler.PlayTrack(track);

            _streamManager.VerifyAllExpectations();
        }

        [Test]
        public void TrackStreamRetrieved_ValidTrackStream_SendsStreamToTrackStreamPlayer()
        {
            var trackStream = new TrackStream();

            _trackStreamPlayer.Expect(x => x.PlayStream(trackStream));

            _trackHandler.TrackStreamRetreived(trackStream);

            _trackStreamPlayer.VerifyAllExpectations();
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
