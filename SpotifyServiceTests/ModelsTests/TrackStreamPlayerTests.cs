using NUnit.Framework;
using SpotifyService.Models;

namespace SpotifyServiceTests.ModelsTests
{
    [TestFixture]
    public class TrackStreamPlayerTests
    {
        private TrackStreamPlayer _trackStreamPlayer;

        [SetUp]
        public void Init()
        {
            _trackStreamPlayer = new TrackStreamPlayer();
        }

    }
}
