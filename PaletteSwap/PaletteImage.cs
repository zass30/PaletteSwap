using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{    public class PaletteImage
    {
        public Palette palette;
        public Bitmap baseImage;
        public Color[] baseColors;
        public ColorMap[] remapTable;
        public List<string> labels;

        public PaletteImage(Bitmap bitmap)
        {
            baseImage = bitmap;
        }

        public PaletteImage(Bitmap bitmap, Color[] colors)
        {
            this.baseColors = colors;
            baseImage = bitmap;
        }

        public void SetRemapColorArray(Color[] colors)
        {
            remapTable = PaletteHelper.GenerateColorMap(baseColors, colors);
        }

        public Bitmap RemappedImage()
        {
            var remap = palette.ColorsFromListOfLabels(labels);
            SetRemapColorArray(remap);
            Bitmap b = new Bitmap(baseImage);
            int width = b.Width;
            int height = b.Height;
            Graphics gfb = Graphics.FromImage(b);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            gfb.DrawImage(b, new Rectangle(0, 0, width, height),
                                    0, 0, width, height,
                                    GraphicsUnit.Pixel, imageAttributes);
            return b;
        }
    }
}
