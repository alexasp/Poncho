using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService;
using SpotifyService.Interfaces;
using SpotifyService.Models.Interfaces;

namespace SpotifyIntegrationTests
{
    //Integrationtests
    [TestFixture]
    public class IntegrationTests
    {
        private IMusicServices _musicServices;
        private ISearchManager _searchmanager;
        private ISpotifyWrapper _spotifyWrapper;


        [SetUp]
        public void Init()
        {
            _searchmanager = MockRepository.GenerateMock<ISearchManager>();
            _spotifyWrapper = new SpotifyWrapper();
            _musicServices = new MusicServices(_searchmanager, _spotifyWrapper);

#region

            var username = "AlexBA";
            var passwd = "tomater90";
#endregion
            _musicServices.InitializeSession(username, passwd);
        }


        [Test]
        public void Search()
        {
            _musicServices.Search("seigmen");
        }

        [TearDown]
        public void TearDown()
        {
            _musicServices.EndSession();
        }


    }
}