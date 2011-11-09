using System.Linq;
using NUnit.Framework;
using SpotifyService.Cargo;
using SpotifyService.Model;

namespace SpotifyServiceTests.ModelsTests
{
    class QueueTests
    {
        private TrackQueue _trackQueue;

        [SetUp]
        public void Init()
        {
            _trackQueue = new TrackQueue();
        }

        [Test]
        public void Enqueue_ValidTrack_TrackIsQueued()
        {
            var track = new Track(true);

            _trackQueue.Enqueue(track);

            Assert.AreEqual(track, _trackQueue.TrackList[0]);
        }

        [Test]
        public void Append_ValidTrack_TrackIsAppendedToFrontOfQueue()
        {
            var track = new Track(true);

            _trackQueue.Append(track);

            Assert.AreEqual(track, _trackQueue.TrackList.Last());
        }



        [Test]
        public void Dequeue_NotEmpty_ReturnsFirstInQueue()
        {
            var track1 = new Track(true);
            var track2 = new Track(true);

            _trackQueue.Enqueue(track1);
            _trackQueue.Enqueue(track2);

            Assert.AreEqual(track1, _trackQueue.Dequeue());
        }

    }
}
