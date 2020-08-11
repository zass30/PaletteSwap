using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteHelperTest
    {
        [TestMethod]
        public void OverlayTransparencyTest()
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
            var resultbmp = PaletteHelper.overlayTransparency(srcbmp, destbmp);
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(10, 0));
            Assert.AreEqual(Color.FromArgb(255, 25, 3, 5), resultbmp.GetPixel(17, 0));
        }

        [TestMethod]
        public void CreateColorMaskTest()
        {

            Bitmap srcbmp = new Bitmap(20, 1);
            for (int i = 0; i < 20; i++)
            {
                srcbmp.SetPixel(i, 0, Color.FromArgb(255, 0, 0, 255));
            }
            srcbmp.SetPixel(5, 0, Color.FromArgb(255, 255, 0, 255));
            srcbmp.SetPixel(10, 0, Color.FromArgb(255, 255, 0, 255));

            var resultbmp = PaletteHelper.createColorMask(srcbmp, Color.FromArgb(255, 255, 0, 255));

            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(0, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(15, 0));
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), resultbmp.GetPixel(19, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(10, 0));
        }

        [TestMethod]
        public void OverlayImageTest()
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

            var resultbmp = PaletteHelper.overlayImage(foreground, background);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(0, 0));
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(15, 0));
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), resultbmp.GetPixel(19, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(5, 0));
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 255), resultbmp.GetPixel(10, 0));
        }

        [TestMethod]
        public void ColorToMemFormatTest()
        {
            Color c = Color.FromArgb(255, 255, 255, 255);
            var s = PaletteHelper.ColorToMemFormat(c);
            Assert.AreEqual("FF0F", s);

            c = Color.FromArgb(255, 0, 255, 255);
            s = PaletteHelper.ColorToMemFormat(c);
            Assert.AreEqual("FF00", s);

            c = Color.FromArgb(255, 0, 0, 255);
            s = PaletteHelper.ColorToMemFormat(c);
            Assert.AreEqual("0F00", s);

            c = Color.FromArgb(255, 0, 0, 0);
            s = PaletteHelper.ColorToMemFormat(c);
            Assert.AreEqual("0000", s);
        }

        [TestMethod]
        public void MemFormatToColorTest()
        {
            var s = "FF0F";
            var c = PaletteHelper.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), c);

            s = "0000";
            c = PaletteHelper.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 0), c);

            s = "FF00";
            c = PaletteHelper.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 255), c);

            s = "0F00";
            c = PaletteHelper.MemFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255), c);
        }

        [TestMethod]
        public void ColFormatToColorTest()
        {
            var s = "255 255 255";
            var c = PaletteHelper.RGBFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), c);

            s = "17 34 51";
            c = PaletteHelper.RGBFormatToColor(s);
            Assert.AreEqual(Color.FromArgb(255, 17, 34, 51), c);
        }

        [TestMethod]
        public void AreBitmapsSameTest()
        {
            Bitmap a = new Bitmap(PaletteSwap.Properties.Resources.DIC_portraitwin5);
            Bitmap b = new Bitmap(PaletteSwap.Properties.Resources.DIC_portraitwin5);
            Assert.IsTrue(PaletteHelper.areBitmapsSame(a,b));
            b.SetPixel(10, 10, Color.Chocolate);
            Assert.IsFalse(PaletteHelper.areBitmapsSame(a, b));
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
            CollectionAssert.AreEqual(expected, result);

            expected = new byte[] { 0x05, 0x00, 0x00, 0x07, 0x08, 0x00, 0x2A, 0x02 };
            result = PaletteHelper.StringToByteStream("0500 0007 0800 2A02");
            CollectionAssert.AreEqual(expected, result);
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
            Color expected = Color.FromArgb(255, 0, 0, 0);
            byte[] b = new byte[] { 0x00, 0x00 };
            Color result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);

            expected = Color.FromArgb(255, 0, 255, 255);
            b = new byte[] { 0xFF, 0x00 };
            result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);

            expected = Color.FromArgb(255, 0, 0, 255);
            b = new byte[] { 0x0F, 0x00 };
            result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);

            expected = Color.FromArgb(255, 255, 255, 255);
            b = new byte[] { 0xFF, 0x0F };
            result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);

            expected = Color.FromArgb(255, 255, 0, 0);
            b = new byte[] { 0x00, 0x0F };
            result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);

            expected = Color.FromArgb(255, 0, 255, 0);
            b = new byte[] { 0xF0, 0x00 };
            result = PaletteHelper.ByteToColor(b);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void PatchMemoryTest()
        {
            byte[] b = new byte[10];
            PaletteHelper.patch_memory(b, 0, "1234");
            Assert.AreEqual(b[0], 0x12);
            Assert.AreEqual(b[1], 0x34);

            b = new byte[10];
            PaletteHelper.patch_memory(b, 2, "1234");
            Assert.AreEqual(b[2], 0x12);
            Assert.AreEqual(b[3], 0x34);

            b = new byte[10];
            PaletteHelper.patch_memory(b, 4, "1234 5678");
            Assert.AreEqual(b[4], 0x12);
            Assert.AreEqual(b[5], 0x34);
            Assert.AreEqual(b[6], 0x56);
            Assert.AreEqual(b[7], 0x78);


            byte[] r1 = PaletteSwap.Properties.Resources.sfxe04a;
            byte[] r2 = PaletteSwap.Properties.Resources.sfxe04a;

            r1[0x448B0] = 0x55;
           r1[0x448B1] = 0x05;

            PaletteHelper.patch_memory(r2, 0x448B0, "5505");

            CollectionAssert.AreEqual(r1, r2);
        }
    }
}

