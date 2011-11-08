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
            _musicServices = MockRepository.GenerateStub<IMusicServices>();
            _loginManager = new LoginManager(_musicServices);
        }

        [Test]
        public void RequestLogin_ProperLoginInfo_CallsInitializeSessionOnIMusicServices()
        {
            var username = "aspis";
            var password = "123asd";

            _musicServices.Expect(x => x.InitializeSession(username, password));

            _loginManager.AttemptLogin(username, password);
        }
    }
}
