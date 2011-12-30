using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class ShellViewModelTests
    {
        private IPlaylistManager _playListManager;
        private IShellViewModel _shellViewModel;
        private IUserFeedbackHandler _userFeedbackHandler;
        private ISearchManager _searchManager;
        private ITrackHandler _trackHandler;

        [SetUp]
        public void Init()
        {
            _playListManager = MockRepository.GenerateMock<IPlaylistManager>();
            _userFeedbackHandler = MockRepository.GenerateMock<IUserFeedbackHandler>();
            _searchManager = MockRepository.GenerateMock<ISearchManager>();
            _trackHandler = MockRepository.GenerateMock<ITrackHandler>();
            _shellViewModel = new ShellViewModel(_trackHandler, _searchManager, _playListManager, _userFeedbackHandler);
        }

        [Test]
        public void PropertySelectedPlaylistChanged_ValidPlaylist_SetsSelectedPlayListOnPlayListManager()
        {
            var playList = new PlayList();

            _playListManager.Expect(x => x.SelectedPlayList = playList);

            _shellViewModel.SelectedPlayList = playList;
        }


        [Test]
        public void Search_ContainsSearchText_SendsSearchToSearchHandler()
        {
            _shellViewModel.SearchText = "SomeSearch";

            _searchManager.Expect(x => x.Search(_shellViewModel.SearchText));

            _shellViewModel.Search();

            _searchManager.VerifyAllExpectations();
        }

        [Test]
        public void Search_ContainsNoText_MessagesUserFeedbackHandler()
        {
            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.NoSearchTextEntered));

            _shellViewModel.SearchText = "";

            _shellViewModel.Search();
        }

        [Test]
        public void PlayPause_IsPlaying_SetsPlaybackStatusToPaused()
        {
            _shellViewModel.PlaybackStatus = PlaybackStatus.Playing;

            _shellViewModel.PlayPause();

            Assert.AreEqual(PlaybackStatus.Paused, _shellViewModel.PlaybackStatus);
        }

        [Test]
        public void PlayPause_IsPaused_SetsPlaybackStatusToPlaying()
        {
            _shellViewModel.PlaybackStatus = PlaybackStatus.Paused;

            _shellViewModel.PlayPause();

            Assert.AreEqual(PlaybackStatus.Playing, _shellViewModel.PlaybackStatus);
        }

        [Test]
        public void PlaySelectedTrack_TrackPlayable_SendsTrackToTrackHandler()
        {
            //playable track in list
            var trackList = new List<Track>();
            var track = new Track(true);
            trackList.Add(track);

            _shellViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.PlayTrack(track));

            _shellViewModel.PlaySelectedTrack();

            _trackHandler.VerifyAllExpectations();
        }

        [Test]
        public void PlaySelectedTrack_NoTrackSelected_SendsNoTrackSelectedToUserFeedbackHandler()
        {
            //playable track in list
            var trackList = new List<Track>();

            _shellViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.NoTrackSelected));

            _shellViewModel.PlaySelectedTrack();

            _trackHandler.VerifyAllExpectations();
        }


        [Test]
        public void PlaySelectedTrack_TrackNotPlayable_MessagesUserFeedBackHandler()
        {
            //not playable track in list
            var trackList = new List<Track>();
            var track = new Track(false);
            trackList.Add(track);
            _shellViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.TrackNotPlayable));

            _shellViewModel.PlaySelectedTrack();

            _userFeedbackHandler.VerifyAllExpectations();
        }

        [Test]
        public void QueueTrackList_TrackPlayable_SendsTrackToTrackHandler()
        {
            var trackList = new List<Track>();
            _shellViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(trackList));

            _shellViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_TrackNotPlayable_MessagesUserFeedbackHandler()
        {
            var trackList = new List<Track>();
            _shellViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.SomeTracksNotPlayable));

            _shellViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_SomeTracksNotPlayable_QueuesPlayableTracks()
        {
            var trackList = new List<Track>();
            var track1 = new Track(false);
            var track2 = new Track(true);
            _shellViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(Arg<List<Track>>.Matches(y => y.Contains(track2) && !y.Contains(track1))));

            _shellViewModel.QueueTracks();
        }


    }
}
