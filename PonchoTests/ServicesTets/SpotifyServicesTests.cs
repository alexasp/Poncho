using NUnit.Framework;
using Poncho.Models.Services;
using Poncho.Models.Services.Enums;

namespace PonchoTests.ServicesTets
{
    [TestFixture]
    public class SpotifyServicesTests
    {
        private SpotifyService _spotifyServices;


        [SetUp]
        public void Init()
        {
            _spotifyServices = new SpotifyService();
            //Login
            #region
            _spotifyServices.RequestLogin("AlexBA", "tomater90");
            #endregion
        }


        [Test]
        public void Search()
        {
            _spotifyServices.Search("seigmen");
        }

        [Test]
        public void SearchCallback_CreatesTrackListAndSendsToSearchManager()
        {
            _spotifyServices.Search("seigmen");
        }

        [TearDown]
        public void TearDown()
        {
            _spotifyServices.Logout();
            _spotifyServices.EndSession();
        }


    }
}