using NUnit.Framework;
using Poncho.Models.Services;

namespace PonchoTests.ServicesTets
{
    [TestFixture]
    public class SpotifyServicesTests
    {
        [Test]
        public void SessionCreateAndLogin_ValidLogin_SPErrorOK()
        {
            var spotifyServices = new SpotifyServices();
            spotifyServices.RequestLogin("AlexBA", "tomater90");
        }
        
    }
}