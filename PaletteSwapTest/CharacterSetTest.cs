using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using PaletteSwap.Properties;
using System.IO.Compression;
using System.IO;

namespace PaletteSwapTest
{
    [TestClass]
    public class CharacterSetTest
    {
        [TestMethod]
        public void GetBitmapTest()
        {
            var d = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.lp);
            var b = d.GetBitmap("neutral");
            var b_expected = d.sprite.GetBitmap("neutral");
            Assert.AreEqual(b_expected.Width, b.Width);
        }
    }
}
