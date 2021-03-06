﻿using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace PaletteSwap
{
    public class PaletteHelper
    {
        // function that takes byte stream, dictionary, and list of labels
        // outputs an array of colors for those labels
        public static Color[] ColorsFromLabelsAndStream(byte[] b, 
            Dictionary<string, List<int>> offsets, List<string> labels)
        {
            List<Color> colors = new List<Color>();
            foreach (string label in labels)
            {
                int position = offsets[label][0];
                byte[] colbyte = new byte[2];
                colbyte[0] = b[position];
                colbyte[1] = b[position + 1];
                Color col = PaletteHelper.ByteToColor(colbyte);
                colors.Add(col);
            }
            return colors.ToArray();
        }

        public static byte[] patch_memory(byte[] b, int address, string s)
        {
            byte[] p = StringToByteStream(s);
            Array.Copy(p, 0, b, address, p.Length);
            return b;
        }

        public static ColorMap[] GenerateColorMap(Color[] oldcolors, Color[] newcolors)
        {
            ColorMap[] remapTable = new ColorMap[oldcolors.Length];
            for (int j = 0; j < remapTable.Length; j++)
            {
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = oldcolors[j];
                colorMap.NewColor = newcolors[j];
                remapTable[j] = colorMap;
            }
            return remapTable;
        }

        public static string ByteStreamToString(byte[] bytearray)
        {
            StringBuilder s = new StringBuilder();
            int i = 0;
            foreach (byte b in bytearray)
            {
                s.Append(b.ToString("X2"));
                if (i % 2 != 0)
                    s.Append(" ");
                i++;
            }
            return s.ToString().Trim();
        }

        public static byte[] StringToByteStream(string s)
        {
            var v = s.Split(' ');
            byte[] bytearray = new byte[v.Length*2];
            int i = 0;
            foreach (string w in v)
            {
                var a = w[0] + "" + w[1];
                var b = w[2] + "" + w[3];

                bytearray[i] = byte.Parse(a, System.Globalization.NumberStyles.HexNumber);
                bytearray[i+1] = byte.Parse(b, System.Globalization.NumberStyles.HexNumber);

                i += 2;
            }
            return bytearray;
        }

        public static byte[] ColorToByte(Color c)
        {
            byte[] b = new byte[2];
            b[0] = (byte)((c.G / 16) * 16 + (c.B / 16));
            b[1] = (byte)(c.R / 16);
            return b;
        }

        public static Color ByteToColor (byte[] b)
        {
            int R = (b[1] % 16) * 17;
            int B = (b[0] % 16) * 17;
            int G = (b[0] / 16) * 17;
            return Color.FromArgb(255, R, G, B);
        }

        public static Bitmap PaletteSwap(Bitmap img, Color[] p_src, Color[] p_dest)
        {
            // foreach pixel in image
            // get "swap color"
            // replace color 
            Bitmap swappedImg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    Color gotColor = img.GetPixel(x, y);
                    var swappedColor = ColorSwap(gotColor, p_src, p_dest);
                    swappedImg.SetPixel(x, y, swappedColor);
                }
            }
            return swappedImg;
        }


        public static Color ColorSwap(Color c, Color[] p_src, Color[] p_dest)
        {
            for (int i = 0; i < p_src.Length; i++)
            {
                if (c == p_src[i])
                    return p_dest[i];
            }
            return c;
        }

        public static string ColorToMemFormat(Color c)
        {
            string s = (c.G / 16).ToString("X1") + (c.B / 16).ToString("X1") + "0" + (c.R / 16).ToString("X1");
            return s;
        }

        public static Color MemFormatToColor(string s)
        {
            string c = "FF" + s[3].ToString() +
               s[3].ToString() + s[0].ToString() + s[0].ToString()
               + s[1].ToString() + s[1].ToString();

            var cint = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
            return Color.FromArgb(cint);
        }

        public static Color RGBFormatToColor(string s)
        {
            var v = s.Split(' ');
            var R = int.Parse(v[0]);
            var G = int.Parse(v[1]);
            var B = int.Parse(v[2]);
            return Color.FromArgb(255, R, G, B);
        }

        public static string ColorFormatToRGB(Color c)
        {
            return c.R.ToString() + " " + c.G.ToString() + " " + c.B.ToString();
        }

        public static bool areBitmapsSame(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                return false;
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    {
                        var apix = a.GetPixel(x, y);
                        var bpix = b.GetPixel(x, y);
                        if (a.GetPixel(x, y) != b.GetPixel(x, y))
                            return false;
                    }
                }
            }
            return true;
        }


        public static bool areBitmapsSameSkipTransparencies(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                return false;
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    {
                        var apix = a.GetPixel(x, y);
                        var bpix = b.GetPixel(x, y);
                        if (a.GetPixel(x, y) != b.GetPixel(x, y) && apix.A !=0 && bpix.A != 0)
                            return false;
                    }
                }
            }
            return true;
        }

        public static Bitmap overlayImage(Bitmap foreground, Bitmap background)
        {
            if (background.Width != foreground.Width || background.Height != foreground.Height)
            {
                throw new Exception("Incompatible bitmap sizes");
            }
            var retimg = new Bitmap(background.Width, background.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < background.Width; x++)
            {
                for (int y = 0; y < background.Height; y++)
                {
                    Color forecolor = foreground.GetPixel(x, y);
                    if (forecolor.A == 255)
                    {
                        retimg.SetPixel(x, y, forecolor);
                    }
                    else
                    {
                        retimg.SetPixel(x, y, background.GetPixel(x, y));
                    }
                }
            }

            return retimg;
        }

        public static Bitmap createColorMask(Bitmap src_img, Color c)
        {
            var retimg = new Bitmap(src_img.Width, src_img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int x = 0; x < src_img.Width; x++)
            {
                for (int y = 0; y < src_img.Height; y++)
                {
                    Color gotColor = src_img.GetPixel(x, y);
                    if (gotColor == c)
                    {
                        retimg.SetPixel(x, y, c);
                    }
                }
            }
            return retimg;
        }

        public static Bitmap overlayTransparency(Bitmap src_img, Bitmap dest_img)
        {
            // take source file, and for each transparent pixel (0,0,0,0) in it, 
            // make the corresponding pixel in dest file transparent as well
            if (src_img.Width != dest_img.Width || src_img.Height != dest_img.Height)
            {
                throw new Exception("Incompatible bitmap sizes");
            }

            var retimg = new Bitmap(src_img.Width, src_img.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int x = 0; x < src_img.Width; x++)
            {
                for (int y = 0; y < src_img.Height; y++)
                {
                    Color gotColor = src_img.GetPixel(x, y);

                    if (gotColor.A == 0)
                    {
                        retimg.SetPixel(x, y, Color.FromArgb(0, 0, 0, 0));
                    }
                    else
                    {
                        retimg.SetPixel(x, y, dest_img.GetPixel(x, y));
                    }
                }
            }
            return retimg;
        }
    }
}