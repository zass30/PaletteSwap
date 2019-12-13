using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;

namespace PaletteSwapTestsNet
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
        public void PaletteSwapTest()
        {
            Bitmap srcbmp = new Bitmap(15, 1);
            var pal_src = Palette.PaletteFromMem(Palette.bis1Mem);
            var pal_dest = Palette.PaletteFromMem(Palette.bis2Mem);
            for (int i = 0; i < 15; i++)
            {
                srcbmp.SetPixel(i, 0, pal_src.colors[i]);
            }

            var swappedbmp = Palette.PaletteSwap(srcbmp, pal_src, pal_dest);
            for (int i = 0; i < 15; i++)
            {
                Assert.AreEqual(pal_dest.colors[i], swappedbmp.GetPixel(i, 0));
            }
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
        public void spriteNewTest()
        {
            string s = Sprite.bis0sprite;
            var sprite = new Sprite(s);
            Assert.AreEqual("0007 0800 2A02 4C00 6D03 8E00 300A B00F F70F B00F 700F FC0F C80D 7309 4005 0000", sprite.row1);
            Assert.AreEqual("0007 0800 2A02 4C00 6D03 8E00 FF0F E90F A40E 700E 400D FC0F C80D 7309 4005 0000", sprite.row5);
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
        public void spriteColorsTest()
        {
            string s = Sprite.bis5sprite;
            var p = new Sprite(s);
            Assert.AreEqual("FD0A", Palette.ColorToMemFormat(p.pads1));
            Assert.AreEqual("DB06", Palette.ColorToMemFormat(p.pads2));
            Assert.AreEqual("A803", Palette.ColorToMemFormat(p.pads3));
            Assert.AreEqual("6402", Palette.ColorToMemFormat(p.pads4));
            Assert.AreEqual("4101", Palette.ColorToMemFormat(p.pads5));

            Assert.AreEqual("BF0F", Palette.ColorToMemFormat(p.costume1));
            Assert.AreEqual("8E0E", Palette.ColorToMemFormat(p.costume2));
            Assert.AreEqual("6C0C", Palette.ColorToMemFormat(p.costume3));
            Assert.AreEqual("4909", Palette.ColorToMemFormat(p.costume4));
            Assert.AreEqual("0606", Palette.ColorToMemFormat(p.costume5));

            Assert.AreEqual("FE0E", Palette.ColorToMemFormat(p.skin1));
            Assert.AreEqual("B90C", Palette.ColorToMemFormat(p.skin2));
            Assert.AreEqual("7609", Palette.ColorToMemFormat(p.skin3));
            Assert.AreEqual("5307", Palette.ColorToMemFormat(p.skin4));

            Assert.AreEqual("330D", Palette.ColorToMemFormat(p.stripe));

            Assert.AreEqual("FE0D", Palette.ColorToMemFormat(p.psychoglow));

            Assert.AreEqual("FF0E", Palette.ColorToMemFormat(p.psychopunch1));
            Assert.AreEqual("DF0C", Palette.ColorToMemFormat(p.psychopunch2));
            Assert.AreEqual("AF09", Palette.ColorToMemFormat(p.psychopunch3));
            Assert.AreEqual("8F07", Palette.ColorToMemFormat(p.psychopunch4));
            Assert.AreEqual("6F05", Palette.ColorToMemFormat(p.psychopunch5));
            
            s = Sprite.bis1sprite;
            p = new Sprite(s);
            Assert.AreEqual("FE0B", Palette.ColorToMemFormat(p.costume1));
            Assert.AreEqual("EB08", Palette.ColorToMemFormat(p.costume2));
            Assert.AreEqual("C705", Palette.ColorToMemFormat(p.costume3));
            Assert.AreEqual("9503", Palette.ColorToMemFormat(p.costume4));
            Assert.AreEqual("7300", Palette.ColorToMemFormat(p.costume5));
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
        public void GenerateStandSpriteTest()
        {
            Bitmap sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand1);
            var s = new Sprite(Sprite.bis1sprite);
            Bitmap sprite_result = s.GenerateStandingSprite();
            Assert.IsTrue(Palette.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand0);
            s = new Sprite(Sprite.bis0sprite);
            sprite_result = s.GenerateStandingSprite();
            Assert.IsTrue(Palette.areBitmapsSame(sprite_expected, sprite_result));
            // fails here because belt gets improperty colored. look into this.
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
    }
}

