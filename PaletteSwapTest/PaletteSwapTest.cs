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
        public void overlayTransparency()
        {
        }
    }
}
