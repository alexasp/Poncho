using Caliburn.Micro;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Interfaces;
using SpotifyService.Messages;
using SpotifyService.Model;
using SpotifyService.Model.Enums;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class UserFeedbackHandlerTests
    {
        private UserFeedbackHandler _userFeedbackHandler;
        private IEventAggregator _eventAggregator;

        [SetUp]
        public void Init()
        {
            _eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            _userFeedbackHandler = new UserFeedbackHandler(_eventAggregator);
        }

        [Test]
        public void Display_TrackNotPlayable_SetsUserMessageOnUserFeedbackDisplay()
        {
            _userFeedbackHandler.Display(UserFeedback.TrackNotPlayable);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<UserFeedbackMessage>.Matches(y => y.Text == "This track is not playable.")));
        }

        [Test]
        public void Display_TracksNotPlayable_SetsUserMessageOnUserFeedbackDisplay()
        {
            _userFeedbackHandler.Display(UserFeedback.SomeTracksNotPlayable);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<UserFeedbackMessage>.Matches(y => y.Text == "One or more of these tracks were not playable.")));

        }

        [Test]
        public void Display_NoSearchTextEntered_SetsUserMessageOnUserFeedbackDisplay()
        {
            _userFeedbackHandler.Display(UserFeedback.NoSearchTextEntered);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<UserFeedbackMessage>.Matches(y => y.Text == "You need to enter a search text.")));

        }

        [Test]
        public void Display_InvalidLoginInfo_SetsUserMessageOnUserFeedbackDisplay()
        {
            _userFeedbackHandler.Display(UserFeedback.InvalidLoginInfo);

            _eventAggregator.AssertWasCalled(x => x.Publish(Arg<UserFeedbackMessage>.Matches(y => y.Text == "The login information you provided was incorrect.")));
        }
    }
}
