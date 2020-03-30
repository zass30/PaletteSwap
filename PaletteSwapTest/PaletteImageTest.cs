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
            Color c = Color.FromArgb(255, 255, 0, 0);
            b.SetPixel(0, 0, c);
            ColorMap cm = new ColorMap();
            cm.OldColor = c;
            cm.NewColor = Color.FromArgb(255, 0, 0, 255);
            ColorMap[] table = new ColorMap[1];
            table[0] = cm;

            PaletteImage p = new PaletteImage(b);
            p.remapTable = table;
            var result_image = p.RemappedImage();            

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), result_image.GetPixel(0, 0));
        }
    }
}
