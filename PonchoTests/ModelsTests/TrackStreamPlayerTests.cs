using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Poncho.Models;

namespace PonchoTests.ModelsTests
{
    [TestFixture]
    class TrackStreamPlayerTests
    {
        private TrackStreamPlayer _trackStreamPlayer;

        [SetUp]
        public void Init()
        {
            _trackStreamPlayer = new TrackStreamPlayer();
        }

    }
}
