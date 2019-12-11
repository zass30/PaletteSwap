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
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), p.face1);
            Assert.AreEqual(Color.FromArgb(255, 255, 221, 153), p.face2);
            Assert.AreEqual(Color.FromArgb(255, 238, 153, 102), p.face3);
            Assert.AreEqual(Color.FromArgb(255, 204, 119, 85), p.face4);
            Assert.AreEqual(Color.FromArgb(255, 170, 102, 68), p.face5);
            Assert.AreEqual(Color.FromArgb(255, 136, 85, 68), p.face6);
            Assert.AreEqual(Color.FromArgb(255, 102, 68, 51), p.face7);
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0), p.blood1);
            Assert.AreEqual(Color.FromArgb(255, 0, 136, 238), p.costume1);
            Assert.AreEqual(Color.FromArgb(255, 0, 119, 204), p.costumeloss1);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 238), p.piping1);
            Assert.AreEqual(Color.FromArgb(255, 153, 119, 255), p.pipingloss1);
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
            Bitmap portrait_expected = new Bitmap(@"..\..\..\PaletteSwap\Resources\dicportraitwin5.png");
            string s = Portrait.bis5portrait;
            var p = new Portrait(s);
            Bitmap portrait_result = p.GenerateVictoryPortrait();
            Assert.IsTrue(Palette.areBitmapsSame(portrait_expected, portrait_result));
        }

        [TestMethod]
        public void areBitmapsSameTest()
        {
            Bitmap a = new Bitmap(@"..\..\..\PaletteSwap\Resources\dicportraitwin5.png");
            Bitmap b = new Bitmap(@"..\..\..\PaletteSwap\Resources\dicportraitwin5.png");
            Assert.IsTrue(Palette.areBitmapsSame(a,b));
            b.SetPixel(10, 10, Color.Chocolate);
            Assert.IsFalse(Palette.areBitmapsSame(a, b));
        }
    }
}

