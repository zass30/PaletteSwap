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

        [TestMethod]
        public void GetPortrait2BeginOffsetTest()
        {
            var offset_result = CharacterConfig.GetPortrait2BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Dictator);
            int offset_expected = 0x394FE;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetPortrait2BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Guile);
            offset_expected = 0x37BFE;
            Assert.AreEqual(offset_expected, offset_result);

            offset_result = CharacterConfig.GetPortrait2BeginOffset(PaletteSwap.CharacterConfig.CHARACTERS.Claw);
            offset_expected = 0x3A3FE;
            Assert.AreEqual(offset_expected, offset_result);
        }

        [TestMethod]
        public void GetByteStreamPairTest()
        {
            var stream_result = CharacterConfig.GetByteStreamPair(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.lp);
            var sprite_result = stream_result.spriteStream;
            var sprite_expected = PaletteHelper.StringToByteStream(PaletteSwap.Properties.Resources.bis0sprite);
            CollectionAssert.AreEqual(sprite_expected, sprite_result);
        }
    }
}
