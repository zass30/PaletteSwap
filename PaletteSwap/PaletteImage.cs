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
        public Bitmap scaledImage;

        public PaletteImage(Bitmap bitmap)
        {
            baseImage = bitmap;
            GenerateScaledImage();
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
            return RemappedImageFromImage(baseImage);
        }

        public Bitmap RemappedScaledImage()
        {
            if (scaledImage == null)
                GenerateScaledImage();
            return RemappedImageFromImage(scaledImage);
        }

            public Bitmap RemappedImageFromImage(Bitmap source)
        {
          //  return source;
            var remap = palette.ColorsFromListOfLabels(labels);
            SetRemapColorArray(remap);
            Bitmap b = new Bitmap(source);
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

        private void GenerateScaledImage()
        {
            int factor = 4;
            var img = this.baseImage;
            if (this.scaledImage == null)
            {
                int neww = img.Width * factor;
                int newh = img.Height * factor;
                Bitmap newbmp = new Bitmap(neww, newh);

                Bitmap bmp = new Bitmap(img);
                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color gotColor = bmp.GetPixel(x, y);
                        for (int i = 0; i < factor; i++)
                        {
                            for (int j = 0; j < factor; j++)
                            {
                                newbmp.SetPixel(factor * x + i, factor * y + j, gotColor);
                            }
                        }
                    }
                }

                this.scaledImage = newbmp;
            }
        }
    }
}
