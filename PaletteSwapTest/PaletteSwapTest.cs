using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using PaletteSwap.Properties;

namespace PaletteSwapTests
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
        public void PaletteSwapArrayTest()
        {
            Bitmap srcbmp = new Bitmap(30, 1);
            var src = new Portrait(Portrait.bis0portrait);
            var dest = new Portrait(Portrait.bis1portrait);
            var src_colors = src.VictoryColorsArray();
            var dest_colors = src.VictoryColorsArray();
            int l = src_colors.Length;

            for (int i = 0; i < l; i++)
            {
                srcbmp.SetPixel(i, 0, src_colors[i]);
            }

            var swappedbmp = Palette.PaletteSwap(srcbmp, src_colors, dest_colors);
            for (int i = 0; i < l; i++)
            {
                Assert.AreEqual(dest_colors[i], swappedbmp.GetPixel(i, 0));
            }
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
        public void portraitNewTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            Assert.AreEqual("FF0F D90F 960E 750C 640A 5408 4306 FE0F F90F D50F A00F 8E00 6D03 4C00 2A02 0A00", p.row1);
            Assert.AreEqual("FF0F D90F 960E 750C 640A 5408 4306 7F09 5D09 3B09 0909 7C00 5B03 4A00 0900 0A00", p.row4);
        }

        [TestMethod]
        public void portraitColorsTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            Assert.AreEqual("FF0F", Palette.ColorToMemFormat(p.skin1));
            Assert.AreEqual("D90F", Palette.ColorToMemFormat(p.skin2));
            Assert.AreEqual("960E", Palette.ColorToMemFormat(p.skin3));
            Assert.AreEqual("750C", Palette.ColorToMemFormat(p.skin4));
            Assert.AreEqual("640A", Palette.ColorToMemFormat(p.skin5));
            Assert.AreEqual("5408", Palette.ColorToMemFormat(p.skin6));
            Assert.AreEqual("4306", Palette.ColorToMemFormat(p.skin7));

            Assert.AreEqual("FE0F", Palette.ColorToMemFormat(p.piping1));
            Assert.AreEqual("F90F", Palette.ColorToMemFormat(p.piping2));
            Assert.AreEqual("D50F", Palette.ColorToMemFormat(p.piping3));
            Assert.AreEqual("A00F", Palette.ColorToMemFormat(p.piping4));

            Assert.AreEqual("8E00", Palette.ColorToMemFormat(p.costume1));
            Assert.AreEqual("6D03", Palette.ColorToMemFormat(p.costume2));
            Assert.AreEqual("4C00", Palette.ColorToMemFormat(p.costume3));
            Assert.AreEqual("2A02", Palette.ColorToMemFormat(p.costume4));

            Assert.AreEqual("000F", Palette.ColorToMemFormat(p.blood1));
            Assert.AreEqual("000C", Palette.ColorToMemFormat(p.blood2));
            Assert.AreEqual("000A", Palette.ColorToMemFormat(p.blood3));

            Assert.AreEqual("FF0F", Palette.ColorToMemFormat(p.teeth1));
            Assert.AreEqual("CC0C", Palette.ColorToMemFormat(p.teeth2));
            Assert.AreEqual("9909", Palette.ColorToMemFormat(p.teeth3));
            Assert.AreEqual("7707", Palette.ColorToMemFormat(p.teeth4));

            Assert.AreEqual("7F09", Palette.ColorToMemFormat(p.pipingloss1));
            Assert.AreEqual("5D09", Palette.ColorToMemFormat(p.pipingloss2));
            Assert.AreEqual("3B09", Palette.ColorToMemFormat(p.pipingloss3));
            Assert.AreEqual("0909", Palette.ColorToMemFormat(p.pipingloss4));

            Assert.AreEqual("7C00", Palette.ColorToMemFormat(p.costumeloss1));
            Assert.AreEqual("5B03", Palette.ColorToMemFormat(p.costumeloss2));
            Assert.AreEqual("4A00", Palette.ColorToMemFormat(p.costumeloss3));
            Assert.AreEqual("0900", Palette.ColorToMemFormat(p.costumeloss4));
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
        public void portraitRowTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            var expected = "FF0F D90F 960E 750C 640A 5408 4306";
            Assert.AreEqual(expected, p.facerow());
            expected = "FE0F F90F D50F A00F";
            Assert.AreEqual(expected, p.pipingrow());
            expected = "8E00 6D03 4C00 2A02";
            Assert.AreEqual(expected, p.costumerow());
            expected = "000F 000C 000A";
            Assert.AreEqual(expected, p.bloodrow());
            expected = "FF0F CC0C 9909 7707";
            Assert.AreEqual(expected, p.teethrow());
            expected = "7F09 5D09 3B09 0909";
            Assert.AreEqual(expected, p.pipinglossrow());
            expected = "7C00 5B03 4A00 0900";
            Assert.AreEqual(expected, p.costumelossrow());

            var r = p.portraitmem();
            Assert.AreEqual(s, r);
        }

        [TestMethod]
        public void GenerateVictoryPortraitTest()
        {
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            string s = Portrait.bis5portrait;
            var p = new Portrait(s);
            Bitmap portrait_result = p.GenerateVictoryPortrait();
            Assert.IsTrue(Palette.areBitmapsSame(portrait_expected, portrait_result));

            portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin0);
            s = Portrait.bis0portrait;
            p = new Portrait(s);
            portrait_result = p.GenerateVictoryPortrait();
            Assert.IsTrue(Palette.areBitmapsSame(portrait_expected, portrait_result));
        }


        [TestMethod]
        public void GenerateVictoryPortraitPerfTest()
        {
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin0);
            string s = Portrait.bis0portrait;

            for (int i = 0; i < 50; i++)
            {
                var p = new Portrait(s);
                Bitmap portrait_result = p.GenerateVictoryPortrait();
                Assert.IsTrue(Palette.areBitmapsSame(portrait_expected, portrait_result));
            }

            Bitmap portrait_orig = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            Graphics g = Graphics.FromImage(portrait_orig);
            int width = portrait_orig.Width;
            int height = portrait_orig.Height;

            for (int i = 0; i < 2; i++)
            {
                ImageAttributes imageAttributes = new ImageAttributes();
                var orig = new Portrait(Portrait.bis5portrait);
                var orig_colors = orig.VictoryColorsArray();
                var dest = new Portrait(Portrait.bis5portrait);
                var dest_colors = dest.VictoryColorsArray();
                ColorMap[] remapTable = new ColorMap[orig_colors.Length];

                for (int j = 0; j < orig_colors.Length; j++)
                {
                    ColorMap colorMap = new ColorMap();
                    colorMap.OldColor = orig_colors[j];
                    colorMap.NewColor = dest_colors[j];
                    remapTable[j] = colorMap;
                }

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                g.DrawImage(
   portrait_orig,
   new Rectangle(150, 10, width, height),  // destination rectangle 
   0, 0,        // upper-left corner of source rectangle 
   width,       // width of source rectangle
   height,      // height of source rectangle
   GraphicsUnit.Pixel,
   imageAttributes);
            }
        }

        [TestMethod]
        public void GenerateLossPortraitTest()
        { 
            // this one works b/c of flames, eventually fix this test to ignore the flames.
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitloss5);
            string s = Portrait.bis5portrait;
            var p = new Portrait(s);
            Bitmap portrait_result = p.GenerateLossPortrait();
            Assert.IsTrue(Palette.areBitmapsSame(portrait_expected, portrait_result));
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
        public void WriteSPortraitByteStreamBasicTest()
        {
            // test #1
            // check that a bis0sprite's byte representation is what we expect 
            var portrait = new Portrait(Portrait.bis0portrait);
            string s_expected = Portrait.portraitAsTextLine(Portrait.bis0portrait);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            
            var data_result = portrait.ByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                Assert.AreEqual(data_expected[0], data_result[0]);
            }
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
        public void WritePortraitToColFormatTest()
        {
            string s = Portrait.bis0portrait;
            var portrait = new Portrait(s);

            string string_expected = "255 255 255\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            string string_result = portrait.ToColFormat();
            Assert.AreEqual(string_expected, string_result);

            portrait.skin1 = Color.FromArgb(0, 17, 34, 51);
            string_expected = "17 34 51\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            string_result = portrait.ToColFormat();
            Assert.AreEqual(string_expected, string_result);
        }



        [TestMethod]
        public void LoadPortraitFromColFormatTest()
        {
            string s = "17 34 51\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            var portrait = Portrait.LoadFromColFormat(s);
            string string_result = portrait.ToColFormat();
            Assert.AreEqual(s, string_result);
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

