using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteImageTest
    {
        [TestMethod]
        public void BasicImageTest()
        {
            Bitmap b = new Bitmap(1, 1);
            Color c = Color.FromArgb(255, 255, 0, 0);
            b.SetPixel(0, 0, c);
            PaletteImage p = new PaletteImage(b);
            Assert.AreEqual(c, p.baseImage.GetPixel(0, 0));
        }

        [TestMethod]
        public void ScaledImageTest()
        {
/*            Bitmap b = new Bitmap(1, 1);
            Color c = Color.FromArgb(255, 255, 0, 0);
            b.SetPixel(0, 0, c);
            PaletteImage p = new PaletteImage(b);
            Assert.AreEqual(c, p.baseImage.GetPixel(0, 0));
            var scaledImage = p.ScaledImage();
            Assert.AreEqual(4, scaledImage.Height);
            Assert.AreEqual(4, scaledImage.Width);*/
        }

    }
}
