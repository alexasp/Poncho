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
            var spotifyServices = new SpotifyServices();
            Assert.AreEqual(sp_error.SP_ERROR_OK, spotifyServices.InitializeSession());
            spotifyServices.EndSession();
        }
        
    }
}