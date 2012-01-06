using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Model.Interfaces;

namespace SpotifyServiceTests
{
    [TestFixture]
    class SpotifyServicesTests
    {
        private ISpotifyServices _spotifyServices;
        private ISearchManager _searchManager;
        private ITrackHandler _trackHandler;

        [Test]
        public void Init()
        {
            _searchManager = MockRepository.GenerateStub<ISearchManager>();
            _trackHandler = MockRepository.GenerateStub<ITrackHandler>();

            _spotifyServices = new SpotifyServices(_searchManager, _trackHandler);
        }

        private Track GetPlayableTrack()
        {
            return new Track(0, "name", true);
        }

        private Track GetNotPlayableTrack()
        {
            return new Track(0, "name", true);
        }

        [Test]
        public void Search_ValidSearchString_CallsSearchOnSearchManager()
        {
            string search = "Seigmen";
            _searchManager.Expect(x => x.Search(search));

            _spotifyServices.Search(search);

            _searchManager.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_StringIsNull_ThrowsArgumentException()
        {
            _searchManager.Expect(x => x.Search(null));

            _spotifyServices.Search(null);

            _searchManager.VerifyAllExpectations();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Search_StringIsEmpty_ThrowsArgumentException()
        {
            string search = "";
            _searchManager.Expect(x => x.Search(search));

            _spotifyServices.Search(search);
        }

        [Test]
        public void PlayTrack_PlayableTrack_CallsTrackHandlerPlayTrack()
        {
            var track = GetPlayableTrack();
            _trackHandler.Expect(x => x.PlayTrack(track));

            _spotifyServices.PlayTrack(track);

            _trackHandler.VerifyAllExpectations();
        }

        [Test]
        public void QueueTracks_PlayableTracks_CallsTrackHandlerQueueTracks()
        {
            var track = GetPlayableTrack();
            var trackList = new List<Track>();
            trackList.Add(track);
            _trackHandler.Expect(x => x.QueueTracks(trackList));

            _spotifyServices.QueueTracks(trackList);

            _trackHandler.VerifyAllExpectations();
        }

        [Test]
        public void ChangePlaybackStatus_CallsTrackHandler()
        {
            _trackHandler.Expect(x => x.ChangePlaybackStatus(PlaybackStatus.Playing));

            _spotifyServices.ChangePlaybackStatus(PlaybackStatus.Playing);

            _trackHandler.VerifyAllExpectations();
        }
    }
}
