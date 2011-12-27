using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.ViewModels;
using Poncho.ViewModels.Interfaces;
using Rhino.Mocks;
using SpotifyService.Enums;
using SpotifyService.Model.Interfaces;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class TrackControlViewModelTests
    {
        private ITrackControlViewModel _trackControlViewModel;
        private ITrackHandler _trackHandler;

        [SetUp]
        public void Init()
        {
            _trackHandler = MockRepository.GenerateMock<ITrackHandler>();
            _trackControlViewModel = new TrackControlViewModel(_trackHandler);
        }

        [Test]
        public void PlayPause_IsPlaying_SetsIsPlayingToFalse()
        {
            _trackControlViewModel.PlaybackStatus = PlaybackStatus.Playing;

            _trackControlViewModel.PlayPause();

            Assert.AreEqual(false, _trackControlViewModel.PlaybackStatus);
        }

        [Test]
        public void PlayPause_IsPause_SetsIsPlayingToTrue()
        {
            _trackControlViewModel.IsPlaying = false;

            _trackControlViewModel.PlayPause();

            Assert.AreEqual(true, _trackControlViewModel.IsPlaying);
        }

        [Test]
        public void PlayPause_AnyPlayingState_CallsPlayPauseOnTrackHandlerWithChangedIsPlaying()
        {
            _trackControlViewModel.IsPlaying = true;

            _trackHandler.Expect(x => x.SetPlaybackStatus(!_trackControlViewModel.IsPlaying));

            _trackControlViewModel.PlayPause();

            _trackHandler.VerifyAllExpectations();
        }

    }

}
