using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService;

namespace IntegrationTests
{
    [TestFixture]
    public class SpotifyWrapperTests
    {
        [Test]
        public void Login()
        {
            using (var spotifyWrapper = new SpotifyWrapper())
            {

                spotifyWrapper.CreateSession();

                #region logininfo

                string password = "tomater90";
                string username = "AlexBA";

                #endregion

                spotifyWrapper.RequestLogin(username, password);

            }
        }
    }
}
