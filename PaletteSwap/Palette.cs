using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaletteSwap
{
    // sprite
    // set hat, skin, physcho, etc
    // sprite->as mem
    // sprint -> as mem row 0, row 1, etc
    // sprite -> as act

   // portrait
   // set hat, skin, blood, etc
   // portrait->as mem row 0, 1, etc
   // win portrait as bmp
   // loss portrait as bmp

    public class Portrait
    {
        public static readonly string bis0portrait = @"FF0F D90F 960E 750C 640A 5408 4306 FE0F F90F D50F A00F 8E00 6D03 4C00 2A02 0A00
FF0F D90F 960E 750C 640A 5408 4306 FE0F F90F D50F A00F FF0F CC0C 9909 7707 0A00
FF0F D90F 960E 750C 640A 5408 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00
FF0F D90F 960E 750C 640A 5408 4306 7F09 5D09 3B09 0909 7C00 5B03 4A00 0900 0A00";
        public static readonly string bis1portrait = @"FD0F EB0F B70E 950C 740A 6008 4106 B70F 700F 400C 2008 EB08 C705 9503 7300 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 B70F 700F 400C 2008 FF0F CC0C 9909 7707 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 B70F 700F 400C 2008 9503 7300 6000 4000 0A00";
        public static readonly string bis2portrait = @"D90F B80F 960E 750C 640A 5308 4306 770F 440C 000A 0008 FF0F DD0D BB0B 8808 0A00 
D90F B80F 960E 750C 640A 5308 4306 770F 440C 000A 0008 FF0F CC0C 9909 7707 0A00 
D90F B80F 960E 750C 640A 5308 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
D90F B80F 960E 750C 640A 5308 4306 7F00 5D00 4A00 3700 BB0B 8808 7707 6606 0A00";
        public static readonly string bis3portrait = @"FE0F DB0E B90C 970A 7609 6508 4306 770F 440C 000A 2008 DE06 BC05 8903 6802 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 770F 440C 000A 2008 FF0F CC0C 9909 7707 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 D80F B50D 940B 6008 8903 6802 4600 3400 0A00";
        public static readonly string bis4portrait = @"FF0E CD0D AB0C 890A 7808 6607 5406 9A08 7806 5603 4502 DB0E A80C 860A 7408 0A00 
FF0E CD0D AB0C 890A 7808 6607 5406 9A08 7806 5603 4502 FF0F CC0C 9909 7707 0A00 
FF0E CD0D AB0C 890A 7808 6607 5406 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FF0E CD0D AB0C 890A 7808 6607 5406 550F 000D 000B 0009 860A 7408 6307 5206 0A00";
        public static readonly string bis5portrait = @"FE0F DB0E B90C 970A 7609 6508 4306 FD0A DB06 9703 7500 BF0F 8E0E 6C0C 4909 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 FD0A DB06 9703 7500 FF0F CC0C 9909 7707 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FE0F DB0E B90C 970A 7609 6508 4306 550F 000D 000B 0009 6C0C 4909 0606 4101 0A00";
        public static readonly string bis6portrait = @"FE0F DB0E B90C 970A 7609 6508 4306 EF0E AD0A 7906 5704 9A09 8908 7807 6706 0A00
FE0F DB0E B90C 970A 7609 6508 4306 EF0E AD0A 7906 5704 FF0F CC0C 9909 7707 0A00
FE0F DB0E B90C 970A 7609 6508 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00
FE0F DB0E B90C 970A 7609 6508 4306 000F 000C 000A 0008 7807 6706 5505 4404 0A00";
        public static readonly string bis7portrait = @"FD0F EB0F B70E 950C 740A 6008 4106 EC0F C70E 940B 6008 D90F A70E 850C 6409 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 EC0F C70E 940B 6008 FF0F CC0C 9909 7707 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FD0F EB0F B70E 950C 740A 6008 4106 9C0F 6B0E 590C 4709 850C 750A 5008 3006 0A00";
        public static readonly string bis8portrait = @"FF0F D90F 960E 750C 640A 5408 4306 EF0E AD0A 7906 5705 870F 650E 320C 0009 0A00 
FF0F D90F 960E 750C 640A 5408 4306 EF0E AD0A 7906 5705 FF0F CC0C 9909 7707 0A00 
FF0F D90F 960E 750C 640A 5408 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FF0F D90F 960E 750C 640A 5408 4306 7F00 0D00 0B00 0900 320C 0009 0007 0005 0A00";
        public static readonly string bis9portrait = @"FF0F D90F 960E 750C 640A 5408 4306 EF0E AD0A 7906 5705 870F 650E 320C 0009 0A00 
FF0F D90F 960E 750C 640A 5408 4306 EF0E AD0A 7906 5705 FF0F CC0C 9909 7707 0A00 
FF0F D90F 960E 750C 640A 5408 4306 000F 000C 000A 0008 FF0F CC0C 9909 7707 0A00 
FF0F D90F 960E 750C 640A 5408 4306 7F00 0D00 0B00 0900 320C 0009 0007 0005 0A00";

        public string row1;
        public string row2;
        public string row3;
        public string row4;

        public Color face1;
        public Color face2;
        public Color face3;
        public Color face4;
        public Color face5;
        public Color face6;
        public Color face7;

        public Color teeth1;
        public Color teeth2;
        public Color teeth3;
        public Color teeth4;

        public Color costume1;
        public Color costume2;
        public Color costume3;
        public Color costume4;

        public Color costumeloss1;
        public Color costumeloss2;
        public Color costumeloss3;
        public Color costumeloss4;

        public Color piping1;
        public Color piping2;
        public Color piping3;
        public Color piping4;

        public Color pipingloss1;
        public Color pipingloss2;
        public Color pipingloss3;
        public Color pipingloss4;

        public Color blood1;
        public Color blood2;
        public Color blood3;

        public Portrait(string s)
        {
            var v = s.Split('\n');
            this.row1 = v[0].Trim();
            this.row2 = v[1].Trim();
            this.row3 = v[2].Trim();
            this.row4 = v[3].Trim();

            var r1 = row1.Split(' ');
            face1 = Palette.MemFormatToColor(r1[0]);
            face2 = Palette.MemFormatToColor(r1[1]);
            face3 = Palette.MemFormatToColor(r1[2]);
            face4 = Palette.MemFormatToColor(r1[3]);
            face5 = Palette.MemFormatToColor(r1[4]);
            face6 = Palette.MemFormatToColor(r1[5]);
            face7 = Palette.MemFormatToColor(r1[6]);
            piping1 = Palette.MemFormatToColor(r1[7]);
            piping2 = Palette.MemFormatToColor(r1[8]);
            piping3 = Palette.MemFormatToColor(r1[9]);
            piping4 = Palette.MemFormatToColor(r1[10]);
            costume1 = Palette.MemFormatToColor(r1[11]);
            costume2 = Palette.MemFormatToColor(r1[12]);
            costume3 = Palette.MemFormatToColor(r1[13]);
            costume4 = Palette.MemFormatToColor(r1[14]);

            var r2 = row2.Split(' ');
            teeth1 = Palette.MemFormatToColor(r2[11]);
            teeth2 = Palette.MemFormatToColor(r2[12]);
            teeth3 = Palette.MemFormatToColor(r2[13]);
            teeth4 = Palette.MemFormatToColor(r2[14]);

            var r3 = row3.Split(' ');
            blood1 = Palette.MemFormatToColor(r3[7]);
            blood2 = Palette.MemFormatToColor(r3[8]);
            blood3 = Palette.MemFormatToColor(r3[9]);

            var r4 = row4.Split(' ');
            pipingloss1 = Palette.MemFormatToColor(r4[7]);
            pipingloss2 = Palette.MemFormatToColor(r4[8]);
            pipingloss3 = Palette.MemFormatToColor(r4[9]);
            pipingloss4 = Palette.MemFormatToColor(r4[10]);
            costumeloss1 = Palette.MemFormatToColor(r4[11]);
            costumeloss2 = Palette.MemFormatToColor(r4[12]);
            costumeloss3 = Palette.MemFormatToColor(r4[13]);
            costumeloss4 = Palette.MemFormatToColor(r4[14]);            
        }

        public string facerow()
        {
            return String.Join(" ", new[] { face1, face2, face3, face4, face5, face6, face7}.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string costumerow()
        {
            return String.Join(" ", new[] { costume1, costume2, costume3, costume4}.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string teethrow()
        {
            return String.Join(" ", new[] { teeth1, teeth2, teeth3, teeth4 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string pipingrow()
        {
            return String.Join(" ", new[] { piping1, piping2, piping3, piping4 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string bloodrow()
        {
            return String.Join(" ", new[] { blood1, blood2, blood3 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string costumelossrow()
        {
            return String.Join(" ", new[] { costumeloss1, costumeloss2, costumeloss3, costumeloss4 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string pipinglossrow()
        {
            return String.Join(" ", new[] { pipingloss1, pipingloss2, pipingloss3, pipingloss4 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string portraitmem()
        {
            string s = facerow() + " " + pipingrow() + " " + costumerow() + " 0A00\r\n" +
                facerow() + " " + pipingrow() + " " + teethrow() + " 0A00\r\n" +
                facerow() + " " + bloodrow() + " 0008 " + teethrow() + " 0A00\r\n" +
                facerow() + " " + pipinglossrow() + " " + costumelossrow() + " 0A00";
            return s;
        }

        public Bitmap GenerateVictoryPortrait()
        {
            Bitmap b = new Bitmap(Properties.Resources.dicportraitwin5);
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

        // function that takes an image a, source, and a destination palette, and returns new image
        // with swapped colors
        public static Bitmap PaletteSwap(Bitmap img, Palette p_src, Palette p_dest)
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
                    swappedImg.SetPixel(x,y, swappedColor);
                }
            }
            return swappedImg;
        }

        public static Color ColorSwap(Color c, Palette p_src, Palette p_dest)
        {
            for (int i = 0; i < p_src.colors.Length; i++){
                if (c == p_src.colors[i])
                    return p_dest.colors[i];
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
                        retimg.SetPixel(x, y, Color.FromArgb(0,0,0,0));
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
