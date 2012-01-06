﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Enums;
using SpotifyService.Interfaces;
using SpotifyService.Model.Enums;
using SpotifyService.Model.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class MainViewModelTests
    {
        private IMainViewModel _mainViewModel;
        private ISpotifyServices _spotifyServices;

        [SetUp]
        public void Init()
        {
            _spotifyServices = MockRepository.GenerateStub<ISpotifyServices>();

            _mainViewModel = new MainViewModel(_spotifyServices);
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
        public void Search_ContainsSearchText_SendsSearchToSpotifyServices()
        {
            _mainViewModel.SearchText = "SomeSearch";

            _spotifyServices.Expect(x => x.Search(_mainViewModel.SearchText));

            _mainViewModel.Search();

            _spotifyServices.VerifyAllExpectations();
        }

        [Test]
        public void Search_ContainsNoText_SetsOutputWarning()
        {
            _mainViewModel.SearchText = "";

            _mainViewModel.Search();

            Assert.AreEqual("No search query entered.", _mainViewModel.Output);
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
        public void PlayPause_IsAny_CallsSpotifyServicesToChangeStatus()
        {
            _mainViewModel.PlaybackStatus = PlaybackStatus.Paused;
            _spotifyServices.Expect(x => x.ChangePlaybackStatus(PlaybackStatus.Playing));

            _mainViewModel.PlayPause();

            Assert.AreEqual(PlaybackStatus.Playing, _mainViewModel.PlaybackStatus);
        }

        [Test]
        public void PlaySelectedTrack_TrackPlayable_SendsTrackToSpotifyServices()
        {
            //playable track in list
            var trackList = new List<Track>();
            var track = GetPlayableTrack();
            trackList.Add(track);
            _mainViewModel.SelectedTracks = trackList;
            _spotifyServices.Expect(x => x.PlayTrack(track));

            _mainViewModel.PlaySelectedTrack();

            _spotifyServices.VerifyAllExpectations();
        }

        [Test]
        public void PlaySelectedTrack_NoTrackSelected_SetsOutputMessage()
        {
            //playable track in list
            var trackList = new List<Track>();
            _mainViewModel.TrackList = trackList;
            
            _mainViewModel.PlaySelectedTrack();

            Assert.AreEqual("No track selected.", _mainViewModel.Output);
        }


        [Test]
        public void PlaySelectedTrack_TrackNotPlayable_SetsOutputMessage()
        {
            //not playable track in list
            var trackList = new List<Track>();
            var track = GetNotPlayableTrack();
            trackList.Add(track);
            _mainViewModel.SelectedTracks = trackList;

            _mainViewModel.PlaySelectedTrack();

            Assert.AreEqual("This track is not playable.", _mainViewModel.Output);
        }

        [Test]
        public void QueueTrackList_TrackPlayable_SendsTrackToSpotifyServices()
        {
            var trackList = new List<Track>();
            trackList.Add(GetPlayableTrack());
            _mainViewModel.SelectedTracks = trackList;

            _spotifyServices.Expect(x => x.QueueTracks(trackList));

            _mainViewModel.QueueTracks();

            _spotifyServices.VerifyAllExpectations();
        }

        [Test]
        public void QueueTrackList_TrackNotPlayable_SetsOutput()
        {
            var trackList = new List<Track>();
            trackList.Add(GetNotPlayableTrack());
            _mainViewModel.SelectedTracks = trackList;

            _mainViewModel.QueueTracks();

            Assert.AreEqual("This track is not playable.", _mainViewModel.Output);
        }

        [Test]
        public void QueueTrackList_SomeTracksNotPlayable_QueuesPlayableTracks()
        {
            var trackList = new List<Track>();
            var track1 = GetNotPlayableTrack();
            var track2 = GetPlayableTrack();
            _mainViewModel.SelectedTracks = trackList;

            _spotifyServices.Expect(x => x.QueueTracks(Arg<List<Track>>.Matches(y => y.Contains(track2) && !y.Contains(track1))));

            _mainViewModel.QueueTracks();

            _spotifyServices.VerifyAllExpectations();
        }


    }
}
