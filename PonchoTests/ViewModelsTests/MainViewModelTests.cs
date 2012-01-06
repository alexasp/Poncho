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
    class MainViewModelTests
    {
        private IPlaylistManager _playListManager;
        private IMainViewModel _mainViewModel;
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
            _mainViewModel = new MainViewModel(_trackHandler, _searchManager, _playListManager, _userFeedbackHandler);
        }

        private Track GetNotPlayableTrack()
        {
            return new Track(0, "name", false);
        }

        private Track GetPlayableTrack()
        {
            return new Track(0, "name", true);
        }

        [Test]
        public void PropertySelectedPlaylistChanged_ValidPlaylist_SetsSelectedPlayListOnPlayListManager()
        {
            var playList = new PlayList();

            _playListManager.Expect(x => x.SelectedPlayList = playList);

            _mainViewModel.SelectedPlayList = playList;
        }


        [Test]
        public void Search_ContainsSearchText_SendsSearchToSearchHandler()
        {
            _mainViewModel.SearchText = "SomeSearch";

            _searchManager.Expect(x => x.Search(_mainViewModel.SearchText));

            _mainViewModel.Search();

            _searchManager.VerifyAllExpectations();
        }

        [Test]
        public void Search_ContainsNoText_MessagesUserFeedbackHandler()
        {
            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.NoSearchTextEntered));

            _mainViewModel.SearchText = "";

            _mainViewModel.Search();
        }

        [Test]
        public void PlayPause_IsPlaying_SetsPlaybackStatusToPaused()
        {
            _mainViewModel.PlaybackStatus = PlaybackStatus.Playing;

            _mainViewModel.PlayPause();

            Assert.AreEqual(PlaybackStatus.Paused, _mainViewModel.PlaybackStatus);
        }

        [Test]
        public void PlayPause_IsPaused_SetsPlaybackStatusToPlaying()
        {
            _mainViewModel.PlaybackStatus = PlaybackStatus.Paused;

            _mainViewModel.PlayPause();

            Assert.AreEqual(PlaybackStatus.Playing, _mainViewModel.PlaybackStatus);
        }

        [Test]
        public void PlaySelectedTrack_TrackPlayable_SendsTrackToTrackHandler()
        {
            //playable track in list
            var trackList = new List<Track>();
            var track = GetPlayableTrack();
            trackList.Add(track);

            _mainViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.PlayTrack(track));

            _mainViewModel.PlaySelectedTrack();

            _trackHandler.VerifyAllExpectations();
        }

        [Test]
        public void PlaySelectedTrack_NoTrackSelected_SendsNoTrackSelectedToUserFeedbackHandler()
        {
            //playable track in list
            var trackList = new List<Track>();

            _mainViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.NoTrackSelected));

            _mainViewModel.PlaySelectedTrack();

            _trackHandler.VerifyAllExpectations();
        }


        [Test]
        public void PlaySelectedTrack_TrackNotPlayable_MessagesUserFeedBackHandler()
        {
            //not playable track in list
            var trackList = new List<Track>();
            var track = GetNotPlayableTrack();
            trackList.Add(track);
            _mainViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.TrackNotPlayable));

            _mainViewModel.PlaySelectedTrack();

            _userFeedbackHandler.VerifyAllExpectations();
        }

        [Test]
        public void QueueTrackList_TrackPlayable_SendsTrackToTrackHandler()
        {
            var trackList = new List<Track>();
            _mainViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(trackList));

            _mainViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_TrackNotPlayable_MessagesUserFeedbackHandler()
        {
            var trackList = new List<Track>();
            _mainViewModel.SelectedTracks = trackList;

            _userFeedbackHandler.Expect(x => x.Display(UserFeedback.SomeTracksNotPlayable));

            _mainViewModel.QueueTracks();
        }

        [Test]
        public void QueueTrackList_SomeTracksNotPlayable_QueuesPlayableTracks()
        {
            var trackList = new List<Track>();
            var track1 = GetNotPlayableTrack();
            var track2 = GetPlayableTrack();
            _mainViewModel.SelectedTracks = trackList;

            _trackHandler.Expect(x => x.QueueTracks(Arg<List<Track>>.Matches(y => y.Contains(track2) && !y.Contains(track1))));

            _mainViewModel.QueueTracks();
        }


    }
}
