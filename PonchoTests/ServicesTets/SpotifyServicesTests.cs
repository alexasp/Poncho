using NUnit.Framework;
using Poncho.Models.Services;
using Poncho.Models.Services.Enums;

namespace PonchoTests.ServicesTets
{
    [TestFixture]
    public class SpotifyServicesTests
    {
        [Test]
        public void InitializeSession_ReturnsSPErrorOK()
        {
            var spotifyServices = new SpotifyService();
            Assert.AreEqual(sp_error.SP_ERROR_OK, spotifyServices.InitializeSession());
            spotifyServices.EndSession();
        }

        [Test]
        public void RequestLogin()
        {
            var spotifyServices = new SpotifyService();
            #region
            spotifyServices.RequestLogin("AlexBA", "tomater90");
            #endregion
            spotifyServices.EndSession();
        }

    }
}