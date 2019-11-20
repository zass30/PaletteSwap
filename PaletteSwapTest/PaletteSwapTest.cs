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
            for (int i = 0; i<15; i++)
            {
                srcbmp.SetPixel(i, 0, pal_src.colors[i]);
            }

            var swappedbmp = Palette.PaletteSwap(srcbmp, pal_src, pal_dest);
            for (int i = 0; i < 15; i++)
            {
                Assert.AreEqual(pal_dest.colors[i], swappedbmp.GetPixel(i, 0));
            }
        }
    }
}
