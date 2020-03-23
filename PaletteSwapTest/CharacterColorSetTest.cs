﻿using System;
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
        public void CharacterColorSetSpriteByteStreamTest()
        {
            var cs = new CharacterColorSet();
            byte[] b = cs.sprites_stream04();
            byte[] b_expected = Resources.sfxe04a;

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
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.sprite_offset]);
            }


            // make color 9 bis5color
            cs.characterColors[9] = cc;
            b = cs.sprites_stream04();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.sprite_offset + 9 * CharacterColorSet.sprite_length]);
            }
        }


        [TestMethod]
        public void CharacterColorSetPortraitByteStreamTest()
        {
            var cs = new CharacterColorSet();
            byte[] b = cs.portraits_stream03();
            byte[] b_expected = Resources.sfxe03c;

            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i]);
            }

            var p = new Portrait(Portrait.bis5portrait);
            CharacterColor cc = new CharacterColor();
            cc.p = p;

            // make color 0 bis5color
            cs.characterColors[0] = cc;
            b = cs.portraits_stream03();
            b_expected = p.ByteStream();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.portrait_offset]);
            }


            // make color 9 bis5color
            cs.characterColors[9] = cc;
            b = cs.portraits_stream03();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.portrait_offset + 9 * CharacterColorSet.portrait_length]);
            }
        }

        [TestMethod]
        public void CharacterColorSetByteStreamPhoenixTest()
        {
            var cs = new CharacterColorSet();
            byte[] b = cs.sprites_stream04phoenix();
            byte[] b_expected = Resources.sfxjd04a;

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
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.sprite_offset]);
            }


            // make color 9 bis5color
            cs.characterColors[9] = cc;
            b = cs.sprites_stream04();
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.sprite_offset + 9 * CharacterColorSet.sprite_length]);
            }
        }

        [TestMethod]
        public void ColorSetLoadFromBytesTest()
        {
            // create a color set with bis5 as jab
            var cs = new CharacterColorSet();
            var s = new Sprite(Sprite.bis5sprite);
            var p = new Portrait(Portrait.bis5portrait);
            CharacterColor cc = new CharacterColor();
            cc.s = s;
            cc.p = p;
            cs.characterColors[0] = cc;
            byte[] portraits_stream = cs.portraits_stream03();
            byte[] sprites_stream = cs.sprites_stream04();

            var cs_result = CharacterColorSet.CharacterColorSetFromStreams(sprites_stream, portraits_stream);
            var cc_result = cs_result.characterColors[0];
            Assert.IsNotNull(cc_result);
            var sprite_result = cc_result.s;
            var portrait_result = cc_result.p;
            Assert.IsNotNull(sprite_result);
            Assert.IsNotNull(portrait_result);
            Assert.AreEqual(s.costume1, sprite_result.costume1);
            Assert.AreEqual(p.costume1, portrait_result.costume1);

        }
    }
}
