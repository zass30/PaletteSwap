﻿using System;
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
        public void CharacterSetGenericByteStreamTest()
        {
            var characterSet = new CharacterSet();
            byte[] b = characterSet.sprites_stream04();
            byte[] b_expected = Resources.sfxe04a;
            CollectionAssert.AreEqual(b_expected, b);

            b = characterSet.portraits_stream03();
            b_expected = Resources.sfxe03c;
            CollectionAssert.AreEqual(b_expected, b);
        }

        [TestMethod]
        public void CharacterSetDefaultColorByteStreamTest()
        {
            var characterSet = CharacterSet.GenerateDictatorCharacterSet();
            characterSet.characterColors[0] = Character.createDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.lp);
            var byte_expected = characterSet.characterColors[0].sprite.ToByteStream();
            var byte_result = new Byte[byte_expected.Length];
            Array.Copy(characterSet.sprites_stream04(), characterSet.sprite_offset,
                byte_result, 0, byte_result.Length);
            CollectionAssert.AreEqual(byte_expected, byte_result);

            byte_expected = characterSet.characterColors[0].portrait.ToByteStream();
            byte_result = new Byte[byte_expected.Length];
            Array.Copy(characterSet.portraits_stream03(), characterSet.portrait_offset,
                byte_result, 0, byte_result.Length);
            CollectionAssert.AreEqual(byte_expected, byte_result);
        }

        [TestMethod]
        public void CharacterSetChangeColorByteStreamTest()
        {
            var characterSet = CharacterSet.GenerateDictatorCharacterSet();
            characterSet.characterColors[0] = Character.createDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk);
            var byte_expected = characterSet.characterColors[0].sprite.ToByteStream();
            var byte_result = new Byte[byte_expected.Length];
            Array.Copy(characterSet.sprites_stream04(), characterSet.sprite_offset,
                byte_result, 0, byte_result.Length);
            CollectionAssert.AreEqual(byte_expected, byte_result);

            byte_expected = characterSet.characterColors[0].portrait.ToByteStream();
            byte_result = new Byte[byte_expected.Length];
            Array.Copy(characterSet.portraits_stream03(), characterSet.portrait_offset,
                byte_result, 0, byte_result.Length);
            CollectionAssert.AreEqual(byte_expected, byte_result);
        }

        [TestMethod]
        public void CharacterSetLoadFromBytesTest()
        {
            // create a color set with bis5 as jab
            var cs = CharacterSet.GenerateDictatorCharacterSet();
            cs.characterColors[0].sprite.LoadStream(cs.characterColors[5].sprite.ToByteStream());
            cs.characterColors[0].portrait.LoadStream(cs.characterColors[5].portrait.ToByteStream());
            byte[] portraits_stream = cs.portraits_stream03();
            byte[] sprites_stream = cs.sprites_stream04();
            var s = cs.characterColors[0].sprite;
            var p = cs.characterColors[0].portrait;

            var cs_result = CharacterSet.CharacterColorSetFromStreams(sprites_stream, portraits_stream);
            var cc_result = cs_result.characterColors[0];
            Assert.IsNotNull(cc_result);
            var sprite_result = cc_result.sprite;
            var portrait_result = cc_result.portrait;
            Assert.IsNotNull(sprite_result);
            Assert.IsNotNull(portrait_result);
            Assert.AreEqual(s.GetColor("costume1"), sprite_result.GetColor("costume1"));
            Assert.AreEqual(p.GetColor("costume1"), portrait_result.GetColor("costume1"));
        }
    }
}
