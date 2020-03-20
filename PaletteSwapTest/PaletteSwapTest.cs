using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using PaletteSwap.Properties;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteSwapTests
    {
        [TestMethod]
        public void PaletteACTTest()
        {
            var pal = new Palette();
            string sACT = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            string result = pal.asACT();
            Assert.AreEqual(sACT, pal.asACT());
        }

        [TestMethod]
        public void PaletteMemTest()
        {
            var pal = new Palette();
            string sMem = "0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000";
            string result = pal.asMem();
            Assert.AreEqual(sMem, result);
        }

        [TestMethod]
        public void PaletteFromACTTest()
        {
            string s = Palette.bis1ACT;
            var pal = Palette.PaletteFromACT(s);
            string result = pal.asACT();
            Assert.AreEqual(s, result);

            s = Palette.bis1Mem;
            result = pal.asMem();
            Assert.AreEqual(s, result);
        }

        [TestMethod]
        public void PaletteFromMemTest()
        {
            string s = Palette.bis1Mem;
            var pal = Palette.PaletteFromMem(s);
            string result = pal.asMem();
            Assert.AreEqual(s, result);

            s = Palette.bis1ACT;
            result = pal.asACT();
            Assert.AreEqual(s, result);
        }


        [TestMethod]
        public void ACTtoTextTest()
        {
            byte[] b = {0x77, 0, 0, 0, 0x77, 0x33, 0x33,
                0x99, 0x55, 0x55, 0xCC, 0x77, 0x88, 0xEE, 0xBB, 0xBB };
            string expected = "77 00 00 00 77 33 33 99 55 55 CC 77 88 EE BB BB";
            string result = Palette.ACTtoText(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void overlayTransparencyTest()
        {
            Bitmap srcbmp = new Bitmap(20, 1);
            Bitmap destbmp = new Bitmap(20, 1);
            for (int i = 0; i < 20; i++)
            {
                srcbmp.SetPixel(i, 0, Color.FromArgb(255, 25, 3, 5));
                destbmp.SetPixel(i, 0, Color.FromArgb(255, 25, 3, 5));
            }
            srcbmp.SetPixel(5, 0, Color.FromArgb(0, 0, 0, 0));
            srcbmp.SetPixel(10, 0, Color.FromArgb(0, 0, 0, 0));
            var resultbmp = Palette.overlayTransparency(srcbmp, destbmp);
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(10, 0));
            Assert.AreEqual(Color.FromArgb(255, 25, 3, 5), resultbmp.GetPixel(17, 0));
        }

        [TestMethod]
        public void createColorMaskTest()
        {

            Bitmap srcbmp = new Bitmap(20, 1);
            for (int i = 0; i < 20; i++)
            {
                srcbmp.SetPixel(i, 0, Color.FromArgb(255, 0, 0, 255));
            }
            srcbmp.SetPixel(5, 0, Color.FromArgb(255, 255, 0, 255));
            srcbmp.SetPixel(10, 0, Color.FromArgb(255, 255, 0, 255));

            var resultbmp = Palette.createColorMask(srcbmp, Color.FromArgb(255, 255, 0, 255));

            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(0, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(15, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(19, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(10, 0));
        }

        [TestMethod]
        public void overlayImageTest()
        {

            Bitmap foreground = new Bitmap(20, 1);
            Bitmap background = new Bitmap(20, 1);
            for (int i = 0; i < 20; i++)
            {
                background.SetPixel(i, 0, Color.FromArgb(255, 0, 0, 255));
                foreground.SetPixel(i, 0, Color.FromArgb(0, 0, 0, 0));
            }
            foreground.SetPixel(5, 0, Color.FromArgb(255, 255, 0, 255));
            foreground.SetPixel(10, 0, Color.FromArgb(255, 255, 0, 255));

            var resultbmp = Palette.overlayImage(foreground, background);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(0, 0));
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(15, 0));
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(19, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(10, 0));
        }


        [TestMethod]
        public void colorToMemFormatTest()
        {
            Color c = Color.FromArgb(255, 255, 255, 255);
            var s = Palette.ColorToMemFormat(c);
            Assert.AreEqual("FF0F", s);

            c = Color.FromArgb(255, 0, 255, 255);
            s = Palette.ColorToMemFormat(c);
            Assert.AreEqual("FF00", s);

            c = Color.FromArgb(255, 0, 0, 255);
            s = Palette.ColorToMemFormat(c);
            Assert.AreEqual("0F00", s);

            c = Color.FromArgb(255, 0, 0, 0);
            s = Palette.ColorToMemFormat(c);
            Assert.AreEqual("0000", s);
        }

        [TestMethod]
        public void MemFormatToColorTest()
        {
            var s = "FF0F";
            var c = Palette.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), c);

            s = "0000";
            c = Palette.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 0), c);

            s = "FF00";
            c = Palette.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 255), c);

            s = "0F00";
            c = Palette.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), c);
        }


        [TestMethod]
        public void ColFormatToColorTest()
        {
            var s = "255 255 255";
            var c = Palette.ColFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), c);

            s = "17 34 51";
            c = Palette.ColFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 17, 34, 51), c);
        }



        [TestMethod]
        public void areBitmapsSameTest()
        {
            Bitmap a = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            Bitmap b = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            Assert.IsTrue(Palette.areBitmapsSame(a,b));
            b.SetPixel(10, 10, Color.Chocolate);
            Assert.IsFalse(Palette.areBitmapsSame(a, b));
        }


        [TestMethod]
        public void ByteStreamToStringTest()
        {
            string expected = "0000 000F";
            byte[] b = new byte[] { 0x00, 0x00, 0x00, 0x0F };

            string result = PaletteHelper.ByteStreamToString(b);
            Assert.AreEqual(expected, result);

            expected = "0500 0007 0800 2A02";
            b = new byte[] { 0x05, 0x00, 0x00, 0x07, 0x08, 0x00, 0x2A, 0x02 };

            result = PaletteHelper.ByteStreamToString(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void StringToByteStreamTest()
        {
            byte[] expected = new byte[] { 0x00, 0x00, 0x00, 0x0F };
            byte[] result = PaletteHelper.StringToByteStream("0000 000F");

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }


            expected = new byte[] { 0x05, 0x00, 0x00, 0x07, 0x08, 0x00, 0x2A, 0x02 };
            result = PaletteHelper.StringToByteStream("0500 0007 0800 2A02");

            for (int i = 0; i < result.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i]);
            }

        }


        [TestMethod]
        public void ColorToByteTest()
        {
            Color c = Color.FromArgb(0, 0, 0, 0);
            byte[] expected = new byte[] { 0x00, 0x00 };
            byte[] result = PaletteHelper.ColorToByte(c);
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);

            c = Color.FromArgb(0, 0, 255, 255);
            expected = new byte[] { 0xFF, 0x00 };
            result = PaletteHelper.ColorToByte(c);
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);

            c = Color.FromArgb(0, 0, 0, 255);
            expected = new byte[] { 0x0F, 0x00 };
            result = PaletteHelper.ColorToByte(c);
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);

            c = Color.FromArgb(0, 255, 255, 255);
            expected = new byte[] { 0xFF, 0x0F };
            result = PaletteHelper.ColorToByte(c);
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
        }

        [TestMethod]
        public void ByteToColorTest()
        {
            Color expected = Color.FromArgb(0, 0, 0, 0);
            byte[] b = new byte[] { 0x00, 0x00 };
            Color result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);
        }

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
        public void CharacterColorSetTest()
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
                Assert.AreEqual(b_expected[i], b[i + CharacterColorSet.offset + 9*CharacterColorSet.sprite_length]);
            }
        }
    }
}

