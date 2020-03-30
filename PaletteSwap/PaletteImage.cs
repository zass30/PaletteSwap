﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{    public class PaletteImage
    {
        public Bitmap baseImage;
        public ColorMap[] remapTable;

        public PaletteImage(Bitmap b)
        {
            baseImage = b;
        }

        public Bitmap RemappedImage()
        {
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
