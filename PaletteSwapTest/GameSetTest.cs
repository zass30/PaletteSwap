using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class GameSetTest
    {
        [TestMethod]
        public void SpritesStream04Test()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxe04a;
            var bytes_result = gs.sprites_stream04();
            for (int i = 0; i < bytes_expected.Length; i++)
            {
                Assert.AreEqual(bytes_expected[i], bytes_result[i]);
            }
        }

        [TestMethod]
        public void SpritesStream04PhoenixTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxjd04a;
            var bytes_result = gs.sprites_stream04phoenix();
            for (int i = 0; i < bytes_expected.Length; i++)
            {
                Assert.AreEqual(bytes_expected[i], bytes_result[i]);
            }
        }

        [TestMethod]
        public void SpritesStream04JapaneseTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxj04a;
            var bytes_result = gs.sprites_stream04japanese();
            for (int i = 0; i < bytes_expected.Length; i++)
            {
                Assert.AreEqual(bytes_expected[i], bytes_result[i]);
            }
        }

    }
}
