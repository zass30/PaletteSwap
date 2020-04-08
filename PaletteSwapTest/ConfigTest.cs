using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public void CharacterConfigTest()
        {
            var offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Dictator);
            int offset_expected = 0x00042E7E;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Guile);
            offset_expected = 0x00040E62;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetSpriteBeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Claw);
            offset_expected = 0x000441C2;
            Assert.AreEqual(offset_expected, offset_result);
        }
    }
}
