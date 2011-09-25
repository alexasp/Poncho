using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;
using Poncho.Models.Interfaces;
using Poncho.Models.services;
using Rhino.Mocks;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class LoginManagerTests
    {
        private ILoginManager _loginManager;
        private ISpotifyServices _spotifyServices;

        [SetUp]
        public void Init()
        {
            _spotifyServices = MockRepository.GenerateMock<ISpotifyServices>();
            _loginManager = new LoginManager(_spotifyServices);
        }

        [Test]
        public void RequestLogin_ProperLoginInfo_CallsRequestLoginOnISpotifyServices()
        {
            var username = "aspis";
            var password = "123asd";

            _spotifyServices.Expect(x => x.RequestLogin(username, password));

            _loginManager.AttemptLogin(username, password);
        }
    }
}
