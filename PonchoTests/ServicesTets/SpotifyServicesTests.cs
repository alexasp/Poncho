using NUnit.Framework;
using Poncho.Models.Interfaces;
using Poncho.Models.Services;
using Poncho.Models.Services.Enums;
using Rhino.Mocks;

namespace PonchoTests.ServicesTets
{
    [TestFixture]
    public class SpotifyServicesTests
    {
        private SpotifyService _spotifyServices;
        private ISearchManager _searchmanager;


        [SetUp]
        public void Init()
        {
            _searchmanager = MockRepository.GenerateMock<ISearchManager>();
            _spotifyServices = new SpotifyService(_searchmanager);
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

        [TearDown]
        public void TearDown()
        {
            _spotifyServices.Logout();
            _spotifyServices.EndSession();
        }


    }
}