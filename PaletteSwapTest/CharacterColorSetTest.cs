using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using PaletteSwap.Properties;
using System.IO.Compression;
using System.IO;

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
        public void CharacterSetSpriteByteStreamTest()
        {
            var characterSet = new CharacterSet();
            characterSet.characterColors[0] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            byte[] b = characterSet.sprites_stream04();
            byte[] b_expected = Resources.sfxe04a;
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i]);
            }

            b = characterSet.portraits_stream03();
            b_expected = Resources.sfxe03c;
            for (int i = 0; i < b_expected.Length; i++)
            {
                Assert.AreEqual(b_expected[i], b[i]);
            }
        }

        [TestMethod]
        public void CharacterSetChangeColorByteStreamTest()
        {
            var characterSet = CharacterSet.GenerateDictatorCharacterSet();
            characterSet.characterColors[0] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var byte_expected = characterSet.characterColors[0].sprite.ToByteStream();
            var byte_result = new Byte[byte_expected.Length];
            Array.Copy(characterSet.sprites_stream04(), characterSet.sprite_offset,
                byte_result, 0, byte_result.Length);

            for (int i = 0; i< byte_expected.Length; i++)
            {
                Assert.AreEqual(byte_expected[i], byte_result[i]);
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
            cs.characterColors[6] = cc;
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

            cc_result = cs_result.characterColors[6];
            Assert.IsNotNull(cc_result);
            sprite_result = cc_result.s;
            portrait_result = cc_result.p;
            Assert.IsNotNull(sprite_result);
            Assert.IsNotNull(portrait_result);
            Assert.AreEqual(s.costume1, sprite_result.costume1);
            Assert.AreEqual(p.costume1, portrait_result.costume1);

        }

        [TestMethod]
        public void ColorSetLoadFromZipTestDemo()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = archive.CreateEntry("foo.txt");

                    using (var entryStream = demoFile.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        streamWriter.Write("Bar!");
                    }
                }
                /*
                using (var fileStream = new FileStream(@"C:\Temp\test.zip", FileMode.Create))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    memoryStream.CopyTo(fileStream);
                }*/
            }
        }

        [TestMethod]
        public void ColorSetLoadFromZipStreamTest()
        {
            // create a color set with bis5 as jab
            var cs = new CharacterColorSet();
            var s = new Sprite(Sprite.bis5sprite);
            var p = new Portrait(Portrait.bis5portrait);
            CharacterColor cc = new CharacterColor();
            cc.s = s;
            cc.p = p;
            cs.characterColors[0] = cc;
            cs.characterColors[6] = cc;
            byte[] portraits_stream = cs.portraits_stream03();
            byte[] sprites_stream = cs.sprites_stream04();
            CharacterColorSet cs_result;

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var _03file = archive.CreateEntry("sfxe.03c");
                    using (var entryStream = _03file.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        var c = entryStream.CanSeek;
                        entryStream.Write(portraits_stream, 0, portraits_stream.Length);
                    }

                    var _04file = archive.CreateEntry("sfxe.04a");
                    using (var entryStream = _04file.Open())
                    using (var streamWriter = new StreamWriter(entryStream))
                    {
                        var c = entryStream.CanSeek;
                        entryStream.Write(sprites_stream, 0, sprites_stream.Length);
                    }
                }

                cs_result = CharacterColorSet.CharacterColorSetFromZipStream(memoryStream);
            }

            // now check that cs_result is correctly populated
            Assert.IsNotNull(cs_result);
            var cc_result = cs_result.characterColors[0];
            Assert.IsNotNull(cc_result);
            Assert.IsNotNull(cc_result.p);
            Assert.IsNotNull(cc_result.s);
            Assert.AreEqual(cc.p.costume1, cc_result.p.costume1);
            Assert.AreEqual(cc.p.piping1, cc_result.p.piping1);
            Assert.AreEqual(cc.s.costume1, cc_result.s.costume1);
            Assert.AreEqual(cc.s.pads1, cc_result.s.pads1);
        }

        [TestMethod]
        public void ZipArchiveTest()
        {
            var cs = new CharacterColorSet();
            var s = new Sprite(Sprite.bis5sprite);
            var p = new Portrait(Portrait.bis5portrait);
            CharacterColor cc = new CharacterColor();
            cc.s = s;
            cc.p = p;
            cs.characterColors[0] = cc;
            cs.characterColors[6] = cc;

            var archive = cs.ZipArchive();
            byte[] sprites_array;
            byte[] portraits_array;


            var _04 = archive.GetEntry("sfxe.04a");
            using (var unzippedEntryStream = _04.Open())
            {
                using (var ms = new MemoryStream())
                {
                    unzippedEntryStream.CopyTo(ms);
                    sprites_array = ms.ToArray();
                }
            }
            var _03 = archive.GetEntry("sfxe.03c");
            using (var unzippedEntryStream = _03.Open())
            {
                using (var ms = new MemoryStream())
                {
                    unzippedEntryStream.CopyTo(ms);
                    portraits_array = ms.ToArray();
                }
            }

            var cs_result = CharacterColorSet.CharacterColorSetFromStreams(sprites_array, portraits_array);
            Assert.IsNotNull(cs_result);
            var cc_result = cs_result.characterColors[0];
            Assert.IsNotNull(cc_result);
            Assert.IsNotNull(cc_result.p);
            Assert.IsNotNull(cc_result.s);
            Assert.AreEqual(cc.p.costume1, cc_result.p.costume1);
            Assert.AreEqual(cc.p.piping1, cc_result.p.piping1);
            Assert.AreEqual(cc.s.costume1, cc_result.s.costume1);
            Assert.AreEqual(cc.s.pads1, cc_result.s.pads1);
        }
    }
}
