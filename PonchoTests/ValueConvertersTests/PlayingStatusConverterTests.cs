using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using SpotifyService.Cargo;
using SpotifyService.Model.Interfaces;
using Poncho.ValueConverters;

namespace PonchoTests.ViewModelsTests
{
    [TestFixture]
    class PlayingStatusConverterTests
    {
        private PlayingStatusConverter _converter;

        [SetUp]
        public void Init()
        {
            _converter = new PlayingStatusConverter();
        }

        [Test]
        public void ValueIsTrue_ReturnsPauseImage()
        {
            Assert.Fail("Test not implemented.");
        }

        [Test]
        public void ValueIsFalse_ReturnsPlayingImage()
        {
            Assert.Fail("Test not implemented.");
        }


    }
}
