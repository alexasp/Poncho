using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Interfaces;
using SpotifyService.Models;
using SpotifyService.Models.Interfaces;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    class LoginManagerTests
    {
        private ILoginManager _loginManager;
        private IMusicServices _musicServices;

        [SetUp]
        public void Init()
        {
            _musicServices = MockRepository.GenerateMock<IMusicServices>();
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
            var username = "aspis";
            var password = "123asd";

            _loginManager.AttemptLogin(username, password);

            _musicServices.AssertWasCalled(x => x.InitializeSession(username, password));

            Assert.Fail();
        }
    }
}
