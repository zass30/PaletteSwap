using System;
using System.Collections.ObjectModel;
using System.Drawing;
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
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }

        [TestMethod]
        public void SpritesStream04PhoenixTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxjd04a;
            var bytes_result = gs.sprites_stream04phoenix();
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }

        [TestMethod]
        public void SpritesStream04JapaneseTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxj04a;
            var bytes_result = gs.sprites_stream04japanese();
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }

        [TestMethod]
        public void PortraitsStream04Test()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxe03c;
            var bytes_result = gs.portraits_stream03();
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }

        [TestMethod]
        public void PortraitsDictatorStream04Test()
        {
            var gs = new GameSet();
            gs.characterDictionary[CharacterConfig.CHARACTERS.Dictator] = CharacterSet.GenerateDictatorCharacterSet();
            gs.characterDictionary[CharacterConfig.CHARACTERS.Dictator].characterColors[0].portrait.SetColor("skin1", Color.FromArgb(0, 17, 17, 17));
            var bytes_expected = PaletteSwap.Properties.Resources.sfxe03c;
            var bytes_result = gs.portraits_stream03();
            for (int i = 0; i < 0x34448; i++)
            {
                Assert.AreEqual(bytes_expected[i], bytes_result[i]);
            }
            byte[] a = new byte[0xF];
            byte[] b = new byte[0xF];
            Array.Copy(bytes_expected, 0x34448, a, 0, 0xF);
            Array.Copy(bytes_result, 0x34448, b, 0, 0xF);

            Assert.AreNotEqual(bytes_expected[0x34448], bytes_result[0x34448]);

        }

        [TestMethod]
        public void PortraitsStream04PhoenixTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxjd03c;
            var bytes_result = gs.portraits_stream03phoenix();
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }

        [TestMethod]
        public void PortraitsStream04JapaneseTest()
        {
            var gs = new GameSet();
            var bytes_expected = PaletteSwap.Properties.Resources.sfxj03c;
            var bytes_result = gs.portraits_stream03japanese();
            CollectionAssert.AreEqual(bytes_expected, bytes_result);
        }
    }
}
