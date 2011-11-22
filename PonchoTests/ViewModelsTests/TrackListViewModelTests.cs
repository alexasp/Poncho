using System;
using System.Collections.Generic;
using NUnit.Framework;
using Poncho.Models;
using Poncho.ViewModels;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    public class TrackListViewModelTests
    {
        private ITrackListViewModel _trackListViewModel;
        private ITrackHandler _trackHandler;
        private IUserFeedbackHandler _userFeedbackHandler;
        private IPlaylistManager _playListManager;

        [SetUp]
        public void Init()
        {
            _trackHandler = MockRepository.GenerateMock<ITrackHandler>();
            _playListManager = MockRepository.GenerateMock<IPlaylistManager>();
            _userFeedbackHandler = MockRepository.GenerateMock<IUserFeedbackHandler>();
            _trackListViewModel = new TrackListViewModel(_trackHandler, _userFeedbackHandler, _playListManager);
        }



        [Test]
        public void PlaySelectedTrack_TrackPlayable_SendsTrackToTrackHandler()
        {
            //playabletrack
            var track = new Track(true);
            _trackListViewModel.SelectedTrack = track;

            _trackHandler.Expect(x => x.PlayTrack(track));

            _trackListViewModel.PlaySelectedTrack();

            _trackHandler.VerifyAllExpectations();
        }


        [Test]
        public void PlaySelectedTrack_TrackNotPlayable_MessagesUserFeedBackHandler()
        {
            //not playable track
            var track = new Track(false);
            _trackListViewModel.SelectedTrack = track;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.TrackNotPlayable));

            _trackListViewModel.PlaySelectedTrack();

            _userFeedbackHandler.VerifyAllExpectations();
        }

        [Test]
        public void QueueTrackList_TrackPlayable_SendsTrackToTrackHandler()
        {
            var trackList = new List<Track>();
            _trackListViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(trackList));

            _trackListViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_TrackNotPlayable_MessagesUserFeedbackHandler()
        {
            var trackList = new List<Track>();
            _trackListViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.SomeTracksNotPlayable));

            _trackListViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_SomeTracksNotPlayable_QueuesPlayableTracks()
        {
            var trackList = new List<Track>();
            var track1 = new Track(false);
            var track2 = new Track(true);
            _trackListViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(Arg<List<Track>>.Matches(y => y.Contains(track2) && !y.Contains(track1))));

            _trackListViewModel.QueueTracks();
        }

        [Test]
        public void ActiveTracksChanged_SetsTrackListToSenderActiveTrackList()
        {
            var list = new List<Track>();
            _trackHandler.Stub(x => x.ActiveTrackList).Return(list);

            _trackListViewModel.ActiveTracksChanged(_trackHandler, new EventArgs());

            Assert.AreEqual(list, _trackListViewModel.TrackList);
        }

    }
}
