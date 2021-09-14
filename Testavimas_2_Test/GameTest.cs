using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Testavimas_2;

namespace Testavimas_2_Test
{
    [TestClass]
    public class GameTest
    {
        Game game;
        [SetUp]
        public void SetUp()
        {
            game = new Game(3);
        }


        [TestMethod]
        public void Game_3_()
        {
        }
    }
}
