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
            Assert.AreEqual(c, p.baseImage.GetPixel(0,0));
        }

        [TestMethod]
        public void ImageColorRemapTest()
        {
            Bitmap b = new Bitmap(1, 1);
            var red = Color.FromArgb(255, 255, 0, 0);
            var blue = Color.FromArgb(255, 0, 0, 255);
            Color c = red;
            b.SetPixel(0, 0, c);

            PaletteImage p = new PaletteImage(b, new Color[] { red } );
            p.SetRemapColorArray(new Color[] { blue });
           var result_image = p.RemappedImage();            

            Assert.AreEqual(blue, result_image.GetPixel(0, 0));
        }
    }
}
