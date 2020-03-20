using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text.RegularExpressions;
using PaletteSwap.Properties;

namespace PaletteSwap
{
    public class PaletteHelper
    {
        public static Portrait original_portrait = new Portrait(Portrait.bis5portrait);
        public static Color[] orig_victory_colors = original_portrait.VictoryColorsArray();
        public static Color[] orig_losstop_colors = original_portrait.LossTopColorsArray();
        public static Color[] orig_lossbottom_colors = original_portrait.LossBottomColorsArray();

        public static Sprite sprite1 = new Sprite(Sprite.bis1sprite);
        public static Sprite sprite5 = new Sprite(Sprite.bis5sprite);
        public static Color[] standing_sprite_colors1 = sprite1.StandingSpriteColorsArray();
        public static Color[] psychopunch_sprite_colors5 = sprite5.PsychoPunchSpriteColorsArray();
        public static Color[] psychoprep_sprite_colors5 = sprite5.PsychoPrepSpriteColorsArray();
        public static Color[] crusher_sprite_colors5 = sprite5.CrusherSpriteColorsArray();

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
            return Color.AliceBlue;
        }
    }

    public struct CharacterColor
    {
        public Sprite s;
        public Portrait p;
    }

    public class CharacterColorSet
    {
        public CharacterColor[] characterColors;

        public static int offset = 0x00042E7C;
        public static int sprite_length = 0xa2;

        public CharacterColorSet()
        {
            characterColors = new CharacterColor[10];
        }

        public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe1;     
            for (int i=0; i<10; i++)
            {
                if (characterColors[i].s == null)
                    continue;
                var s = characterColors[i].s;
                byte[] color_bytes = s.ByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[offset + i * sprite_length + j] = color_bytes[j];
                }

            }
            return b;
        }
    }
    
    public class Palette
    {
        public static readonly string bis0Mem = "0007 0800 2A02 4C00 6D03 8E00 300A B00F F70F B00F 700F FC0F C80D 7309 4005";
        public static readonly string bis1ACT = "77 00 00 00 77 33 33 99 55 55 CC 77 88 EE BB BB FF EE AA 00 00 CC 44 00 FF BB 77 FF 88 44 DD 55 00 FF EE BB EE BB 77 AA 77 44 66 44 33";
        public static readonly string bis1Mem = "0007 7300 9503 C705 EB08 FE0B 000A 400C B70F 840F 500D EB0F B70E 740A 4306";
        public static readonly string bis2Mem = "0005 5505 8808 BB0B DD0D FF0F 0007 7F00 770F 440C 000A ED0F B90D 760A 4306";
        public static readonly string bis3Mem = "0005 4600 6802 8903 BC05 DE06 0007 D70F 770F 440C 000A FE0E B90C 7609 5307";
        public static readonly string bis4Mem = "3400 4205 6307 8509 A80C DB0E 4500 550F 9A08 7806 5603 FF0E AB0C 7808 4005";
        public static readonly string bis5Mem = "4101 0606 4909 6C0C 8E0E BF0F 6402 330D FD0A DB06 A803 FE0E B90C 7609 5307";
        public static readonly string bis6Mem = "3402 4404 5605 6706 7807 9A09 5604 550F EF0E AD0A 7906 FE0E B90C 7609 5307";
        public static readonly string bis7Mem = "5006 4006 6409 850C A70E D90F 6008 6B0E EC0F C70E 940B EB0F B70E 740A 4306";
        public static readonly string bis8Mem = "3402 0006 0009 320C 650E 870F 5604 7F00 EF0E AD0A 7906 D90F 960E 640A 4306";

        public Color[] colors;

        public Palette()
        {
            colors = new Color[15];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.FromArgb(0, 0, 0, 0);
            }
        }

        public string asACT()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < colors.Length; i++)
            {
                sb.Append(toACTFormat(colors[i]));
                sb.Append(" ");
            }
            return sb.ToString().Trim();
        }

        public string asMem()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < colors.Length; i++)
            {
                sb.Append(ColorToMemFormat(colors[i]));
                sb.Append(" ");
            }
            return sb.ToString().Trim();
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


        public static Color ColorSwap(Color c, Palette p_src, Palette p_dest)
        {
            for (int i = 0; i < p_src.colors.Length; i++)
            {
                if (c == p_src.colors[i])
                    return p_dest.colors[i];
            }
            return c;
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

        public static Palette PaletteFromACT(string s)
        {
            //            if (s == "" || s == " ")
            //                return;
            var pal = new Palette();
            string[] s_colors = s.Split(' ');
            for (int i = 0; i < pal.colors.Length; i++)
            {
                var c = "FF" + s_colors[3 * i] + s_colors[3 * i + 1] + s_colors[3 * i + 2];
                var cint = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                pal.colors[i] = Color.FromArgb(cint);
            }
            return pal;
        }

        public static Palette PaletteFromMem(string s)
        {
            var pal = new Palette();
            string[] s_colors = s.Split(' ');
            for (int i = 0; i < pal.colors.Length; i++)
            {
                string c = "FF" + s_colors[i][3].ToString() +
                    s_colors[i][3].ToString() + s_colors[i][0].ToString() + s_colors[i][0].ToString()
                    + s_colors[i][1].ToString() + s_colors[i][1].ToString();

                var cint = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                pal.colors[i] = Color.FromArgb(cint);
            }
            return pal;
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

        public static Color ColFormatToColor(string s)
        {
            var v = s.Split(' ');
            var R = int.Parse(v[0]);
            var G = int.Parse(v[1]);
            var B = int.Parse(v[2]);
            return Color.FromArgb(255, R, G, B);
        }

        public static string toACTFormat(Color c)
        {
            string s = c.R.ToString("X2") + " " + c.G.ToString("X2") + " " + c.B.ToString("X2");
            return s;
        }

        public static string ACTtoText(byte[] bytearray)
        {
            StringBuilder s = new StringBuilder();
            foreach (byte b in bytearray)
            {
                s.Append(b.ToString("X2"));
                s.Append(" ");
            }
            return s.ToString().Trim();
        }

        public static Boolean areBitmapsSame(Bitmap a, Bitmap b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                return false;
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
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