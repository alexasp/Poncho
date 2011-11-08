using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Enums;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class UserFeedbackHandlerTests
    {
        private UserFeedbackHandler _userFeedbackHandler;
        private IUserFeedBackDisplay _userFeedbackDisplay;

        [SetUp]
        public void Init()
        {
            _userFeedbackDisplay = MockRepository.GenerateStub<IUserFeedBackDisplay>();
            _userFeedbackHandler = new UserFeedbackHandler(_userFeedbackDisplay);
        }

        [Test]
        public void Display_TrackNotPlayable_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackHandler.Display(UserFeedback.TrackNotPlayable);

            _userFeedbackDisplay.AssertWasCalled(x => x.DisplayMessage("This track is not playable."));
        }

        [Test]
        public void Display_TracksNotPlayable_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackHandler.Display(UserFeedback.SomeTracksNotPlayable);

            _userFeedbackDisplay.AssertWasCalled(x => x.DisplayMessage("One or more of these tracks were not playable."));

        }

        [Test]
        public void Display_NoSearchTextEntered_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackHandler.Display(UserFeedback.NoSearchTextEntered);

            _userFeedbackDisplay.AssertWasCalled(x => x.DisplayMessage("You need to enter a search text."));

        }

        [Test]
        public void Display_InvalidLoginInfo_SetsUserMessageOnUserFeedbackViewModel()
        {
            _userFeedbackHandler.Display(UserFeedback.InvalidLoginInfo);

            _userFeedbackDisplay.AssertWasCalled(x => x.DisplayMessage("The login information you provided was incorrect."));
        }
    }
}
