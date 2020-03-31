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

/*        [TestMethod]
        public void ImageColorRemapSimpleTest()
        {
            Bitmap b = new Bitmap(1, 1);
            var red = Color.FromArgb(255, 255, 0, 0);
            var blue = Color.FromArgb(255, 0, 0, 255);
            Color c = red;
            b.SetPixel(0, 0, c);

            PaletteImage p = new PaletteImage(b, new Color[] { red });
            p.SetRemapColorArray(new Color[] { blue });
            var result_image = p.RemappedImage();

            Assert.AreEqual(blue, result_image.GetPixel(0, 0));
        }

        [TestMethod]
        public void ImageColorRemapTest()
        {
            Bitmap b = new Bitmap(2, 2);
            var red = Color.FromArgb(255, 255, 0, 0);
            var blue = Color.FromArgb(255, 0, 0, 255);
            var green = Color.FromArgb(255, 0, 255, 0);
            var yellow = Color.FromArgb(255, 255, 255, 0);
            b.SetPixel(0, 0, red);
            b.SetPixel(0, 1, blue);
            b.SetPixel(1, 0, green);
            b.SetPixel(1, 1, yellow);

            PaletteImage p = new PaletteImage(b, new Color[] { red, blue, green, yellow });
            p.SetRemapColorArray(new Color[] { blue, green, yellow, red });
            var result_image = p.RemappedImage();

            Assert.AreEqual(blue, result_image.GetPixel(0, 0));
            Assert.AreEqual(green, result_image.GetPixel(0, 1));
            Assert.AreEqual(yellow, result_image.GetPixel(1, 0));
            Assert.AreEqual(red, result_image.GetPixel(1, 1));
        }
        */

/*        [TestMethod]
        public void GenerateDefaultDictatorStandingNeutralImageTest()
        {
            // is the base image for standing neutral green bison?
            var base_image = ImageConfig.GenerateDictatorStandingNeutralBasePaletteImage();
            var sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand1);
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, base_image.baseImage));

            // set remap to blue jab bison
            var dic = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var remap = dic.sprite.ColorsFromListOfLabels(ImageConfig.DictatorStandNeutralLabels());
            base_image.SetRemapColorArray(remap);
            var remapped_image = base_image.RemappedImage();
            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand0);
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, remapped_image));
        }*/
    }
}
