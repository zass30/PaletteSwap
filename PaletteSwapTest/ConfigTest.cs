using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public void GetSpriteBeginOffsetTest()
        {
            var offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Dictator);
            int offset_expected = 0x42E7E;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Guile);
            offset_expected = 0x40E62;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Claw);
            offset_expected = 0x441C2;
            Assert.AreEqual(offset_expected, offset_result);
        }

        [TestMethod]
        public void GetPortrait1BeginOffsetTest()
        {
            var offset_result = CharacterConfig.GetPortrait1BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Dictator);
            int offset_expected = 0x34448;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetPortrait1BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Guile);
            offset_expected = 0x32B48;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetPortrait1BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Claw);
            offset_expected = 0x35348;
            Assert.AreEqual(offset_expected, offset_result);
        }

    }
}
