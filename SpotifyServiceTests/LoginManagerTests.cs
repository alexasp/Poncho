using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Enums;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class LoginManagerTests
    {
        private ILoginManager _loginManager;
        private IMusicServices _musicServices;
        private IUserFeedbackHandler _userFeedBackHandler;

        [SetUp]
        public void Init()
        {
            _musicServices = MockRepository.GenerateStub<IMusicServices>();
            _userFeedBackHandler = MockRepository.GenerateStub<IUserFeedbackHandler>();
            _loginManager = new LoginManager(_musicServices);
        }

        [Test]
        public void AttemptLogin_ProperLoginInfo_CallsInitializeSessionOnIMusicServices()
        {
            var username = "aspis";
            var password = "123asd";

            _loginManager.AttemptLogin(username, password);

            _musicServices.AssertWasCalled(x => x.InitializeSession(username, password));
        }

        [Test]
        public void AttemptLogin_InvalidUsername_CallsUserFeedBackHandlerWithEmptyUsername()
        {
            var username = "";
            var password = "123asd";

            _loginManager.AttemptLogin(username, password);

            _userFeedBackHandler.AssertWasCalled(x => x.Display(UserFeedback.EmptyUsername));
        }

        [Test]
        public void AttemptLogin_InvalidUsername_CallsUserFeedBackHandlerWithEmptyPassword()
        {
            var username = "aspis";
            var password = "";

            _loginManager.AttemptLogin(username, password);

            _userFeedBackHandler.AssertWasCalled(x => x.Display(UserFeedback.EmptyPassword));
        }
    }
}
