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
        public void SearchRetrieved_AlertsActiveTrackListListeners()
        {
            var list = new List<Track>();
            var result = new SearchResult(list);
            var trackListener = MockRepository.GenerateStub<IActiveTrackListener>();
            _trackHandler.ActiveTrackListListeners += trackListener.ActiveTracksChanged;

            _trackHandler.SearchRetrieved(result);
        }
    }
}
    