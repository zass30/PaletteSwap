using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using PaletteSwap.Properties;

namespace PaletteSwapTest
{
    [TestClass]
    public class CharacterColorSetTest
    {

        [TestMethod]
        public void CharacterColorTest()
        {
            var p = new Portrait(Portrait.bis5portrait);
            var s = new Sprite(Sprite.bis5sprite);
            CharacterColor cs = new CharacterColor();
            cs.p = p;
            cs.s = s;
            Assert.AreEqual(p, cs.p);
            Assert.AreEqual(s, cs.s);
        }

        [TestMethod]
        public void CharacterColorSetNewTest()
        {
            var p = new Portrait(Portrait.bis5portrait);
            var s = new Sprite(Sprite.bis5sprite);
            CharacterColor cc = new CharacterColor();
            cc.p = p;
            cc.s = s;

            var cs = new CharacterColorSet();
            for (int i = 0; i < 10; i++)
            {
                cs.characterColors[i] = cc;
            }
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(cc, cs.characterColors[i]);
            }
        }

        [TestMethod]
        public void CharacterColorSetByteStreamTest()
        {
            var cs = new CharacterColorSet();
            byte[] b = cs.sprites_stream04();
            byte[] b_expected = Resources.sfxe1;

            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i]);
            }

            var s = new Sprite(Sprite.bis5sprite);
            CharacterColor cc = new CharacterColor();
            cc.s = s;

            // make color 0 bis5color
            cs.characterColors[0] = cc;
            b = cs.sprites_stream04();
            b_expected = s.ByteStream();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.offset]);
            }


            // make color 9 bis5color
            cs.characterColors[9] = cc;
            b = cs.sprites_stream04();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.offset + 9 * CharacterColorSet.sprite_length]);
            }
        }
    }
}
