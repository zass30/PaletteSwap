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
        public Bitmap baseImage;
        public ColorMap[] remapTable;

        public PaletteImage(Bitmap b)
        {
            baseImage = b;
        }

        public Bitmap RemappedImage()
        {
            return baseImage;
        }
    }
}
