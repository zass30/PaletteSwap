using System;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PaletteSwap
{
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

        public static string portraitAsTextLine(string s)
        {
            string s_expected = Regex.Replace(s, @"\t|\n|\r", "");
            return s_expected;
        }

        public string row1;
        public string row2;
        public string row3;
        public string row4;

        public Color skin1;
        public Color skin2;
        public Color skin3;
        public Color skin4;
        public Color skin5;
        public Color skin6;
        public Color skin7;

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

        public enum PORTRAIT_COLORS
        {
            skin1,
            skin2,
            skin3,
            skin4,
            skin5,
            skin6,
            skin7,
            teeth1,
            teeth2,
            teeth3,
            teeth4,
            costume1,
            costume2,
            costume3,
            costume4,
            costumeloss1,
            costumeloss2,
            costumeloss3,
            costumeloss4,
            piping1,
            piping2,
            piping3,
            piping4,
            pipingloss1,
            pipingloss2,
            pipingloss3,
            pipingloss4,
            blood1,
            blood2,
            blood3,
        }

        public static Portrait LoadFromColFormat(string s)
        {
            Portrait p = new Portrait();
            var v = s.Split('\n');
            p.skin1 = PaletteHelper.RGBFormatToColor(v[0]);
            p.skin2 = PaletteHelper.RGBFormatToColor(v[1]);
            p.skin3 = PaletteHelper.RGBFormatToColor(v[2]);
            p.skin4 = PaletteHelper.RGBFormatToColor(v[3]);
            p.skin5 = PaletteHelper.RGBFormatToColor(v[4]);
            p.skin6 = PaletteHelper.RGBFormatToColor(v[5]);
            p.skin7 = PaletteHelper.RGBFormatToColor(v[6]);
            p.costume1 = PaletteHelper.RGBFormatToColor(v[7]);
            p.costume2 = PaletteHelper.RGBFormatToColor(v[8]);
            p.costume3 = PaletteHelper.RGBFormatToColor(v[9]);
            p.costume4 = PaletteHelper.RGBFormatToColor(v[10]);
            p.teeth1 = PaletteHelper.RGBFormatToColor(v[11]);
            p.teeth2 = PaletteHelper.RGBFormatToColor(v[12]);
            p.teeth3 = PaletteHelper.RGBFormatToColor(v[13]);
            p.teeth4 = PaletteHelper.RGBFormatToColor(v[14]);
            p.piping1 = PaletteHelper.RGBFormatToColor(v[15]);
            p.piping2 = PaletteHelper.RGBFormatToColor(v[16]);
            p.piping3 = PaletteHelper.RGBFormatToColor(v[17]);
            p.piping4 = PaletteHelper.RGBFormatToColor(v[18]);
            p.pipingloss1 = PaletteHelper.RGBFormatToColor(v[19]);
            p.pipingloss2 = PaletteHelper.RGBFormatToColor(v[20]);
            p.pipingloss3 = PaletteHelper.RGBFormatToColor(v[21]);
            p.pipingloss4 = PaletteHelper.RGBFormatToColor(v[22]);
            p.costumeloss1 = PaletteHelper.RGBFormatToColor(v[23]);
            p.costumeloss2 = PaletteHelper.RGBFormatToColor(v[24]);
            p.costumeloss3 = PaletteHelper.RGBFormatToColor(v[25]);
            p.costumeloss4 = PaletteHelper.RGBFormatToColor(v[26]);
            p.blood1 = PaletteHelper.RGBFormatToColor(v[27]);
            p.blood2 = PaletteHelper.RGBFormatToColor(v[28]);
            p.blood3 = PaletteHelper.RGBFormatToColor(v[29]);

            return p;
        }

        public Portrait()
        {

        }

        public Portrait(string s)
        {
            var v = s.Split('\n');
            this.row1 = v[0].Trim();
            this.row2 = v[1].Trim();
            this.row3 = v[2].Trim();
            this.row4 = v[3].Trim();

            var r1 = row1.Split(' ');
            skin1 = PaletteHelper.MemFormatToColor(r1[0]);
            skin2 = PaletteHelper.MemFormatToColor(r1[1]);
            skin3 = PaletteHelper.MemFormatToColor(r1[2]);
            skin4 = PaletteHelper.MemFormatToColor(r1[3]);
            skin5 = PaletteHelper.MemFormatToColor(r1[4]);
            skin6 = PaletteHelper.MemFormatToColor(r1[5]);
            skin7 = PaletteHelper.MemFormatToColor(r1[6]);
            piping1 = PaletteHelper.MemFormatToColor(r1[7]);
            piping2 = PaletteHelper.MemFormatToColor(r1[8]);
            piping3 = PaletteHelper.MemFormatToColor(r1[9]);
            piping4 = PaletteHelper.MemFormatToColor(r1[10]);
            costume1 = PaletteHelper.MemFormatToColor(r1[11]);
            costume2 = PaletteHelper.MemFormatToColor(r1[12]);
            costume3 = PaletteHelper.MemFormatToColor(r1[13]);
            costume4 = PaletteHelper.MemFormatToColor(r1[14]);

            var r2 = row2.Split(' ');
            teeth1 = PaletteHelper.MemFormatToColor(r2[11]);
            teeth2 = PaletteHelper.MemFormatToColor(r2[12]);
            teeth3 = PaletteHelper.MemFormatToColor(r2[13]);
            teeth4 = PaletteHelper.MemFormatToColor(r2[14]);

            var r3 = row3.Split(' ');
            blood1 = PaletteHelper.MemFormatToColor(r3[7]);
            blood2 = PaletteHelper.MemFormatToColor(r3[8]);
            blood3 = PaletteHelper.MemFormatToColor(r3[9]);

            var r4 = row4.Split(' ');
            pipingloss1 = PaletteHelper.MemFormatToColor(r4[7]);
            pipingloss2 = PaletteHelper.MemFormatToColor(r4[8]);
            pipingloss3 = PaletteHelper.MemFormatToColor(r4[9]);
            pipingloss4 = PaletteHelper.MemFormatToColor(r4[10]);
            costumeloss1 = PaletteHelper.MemFormatToColor(r4[11]);
            costumeloss2 = PaletteHelper.MemFormatToColor(r4[12]);
            costumeloss3 = PaletteHelper.MemFormatToColor(r4[13]);
            costumeloss4 = PaletteHelper.MemFormatToColor(r4[14]);
        }

        public Color[] VictoryColorsArray()
        {
            return new[]{ skin1, skin2, skin3, skin4, skin5, skin6, skin7,
                costume1, costume2, costume3, costume4,
                 teeth1, teeth2, teeth3, teeth4,
                 piping1, piping2, piping3, piping4,
            };
        }

        public Color[] LossTopColorsArray()
        {
            return new[]{ skin1, skin2, skin3, skin4, skin5, skin6, skin7,
                 teeth1, teeth2, teeth3, teeth4,
                 pipingloss1, pipingloss2, pipingloss3, pipingloss4,
                 costumeloss1, costumeloss2, costumeloss3, costumeloss4,
                 blood1, blood2, blood3,
            };
        }

        public Color[] LossBottomColorsArray()
        {
            return new[]{ skin1, skin2, skin3, skin4, skin5, skin6, skin7,
                costume1, costume2, costume3, costume4,
                 piping1, piping2, piping3, piping4,
            };
        }

        public Color[] FullPortraitColorsArray()
        {
            return new[]
            {
                skin1, skin2, skin3, skin4, skin5, skin6, skin7,
                costume1, costume2, costume3, costume4,
                 teeth1, teeth2, teeth3, teeth4,
                 piping1, piping2, piping3, piping4,
                 pipingloss1, pipingloss2, pipingloss3, pipingloss4,
                 costumeloss1, costumeloss2, costumeloss3, costumeloss4,
                 blood1, blood2, blood3,
            };
        }

        public ColorMap[] VictoryColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.orig_victory_colors, VictoryColorsArray());
        }

        public ColorMap[] LossBottomColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.orig_lossbottom_colors, LossBottomColorsArray());
        }

        public ColorMap[] LossTopColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.orig_losstop_colors, LossTopColorsArray());
        }

        public string facerow()
        {
            return String.Join(" ", new[] { skin1, skin2, skin3, skin4, skin5, skin6, skin7 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string costumerow()
        {
            return String.Join(" ", new[] { costume1, costume2, costume3, costume4 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string teethrow()
        {
            return String.Join(" ", new[] { teeth1, teeth2, teeth3, teeth4 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string pipingrow()
        {
            return String.Join(" ", new[] { piping1, piping2, piping3, piping4 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string bloodrow()
        {
            return String.Join(" ", new[] { blood1, blood2, blood3 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string costumelossrow()
        {
            return String.Join(" ", new[] { costumeloss1, costumeloss2, costumeloss3, costumeloss4 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
        }

        public string pipinglossrow()
        {
            return String.Join(" ", new[] { pipingloss1, pipingloss2, pipingloss3, pipingloss4 }.Select(x => PaletteHelper.ColorToMemFormat(x)));
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
            var my_colors = this.VictoryColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, PaletteHelper.orig_victory_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateLossTopPortrait()
        {
            var orig = new Portrait(Portrait.bis5portrait);
            var orig_colors = orig.LossTopColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicportraitlosstop5);
            var my_colors = this.LossTopColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateLossBottomPortrait()
        {
            var orig = new Portrait(Portrait.bis5portrait);
            var orig_colors = orig.LossBottomColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicportraitlossbottom5);
            var my_colors = this.LossBottomColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }
        public Bitmap GenerateLossPortrait()
        {
            var top = GenerateLossTopPortrait();
            var bottom = GenerateLossBottomPortrait();
            var loss = PaletteHelper.overlayImage(top, bottom);
            return loss;
        }

        public Byte[] ByteStream()
        {
            string s = portraitAsTextLine(bis0portrait);
            byte[] b = PaletteHelper.StringToByteStream(s);

            /**            foreach (var k in colorsToMemOffsets.Keys)
                        {
                            Color col = this.ColorFromSpriteColor(k);
                            byte[] c = PaletteHelper.ColorToByte(col);
                            foreach (int offset in colorsToMemOffsets[k])
                            {
                                b[offset] = c[0];
                                b[offset + 1] = c[1];
                            }
                        }*/
            return b;
        }

        public Color ColorFromSpriteColor(PORTRAIT_COLORS label)
        {
            Type myType = GetType();
            var myFieldInfo = myType.GetField(label.ToString());
            return (Color)myFieldInfo.GetValue(this);
        }

        public string ToColFormat()
        {
            var Colors = FullPortraitColorsArray();
            StringBuilder s = new StringBuilder();
            foreach (Color c in Colors)
            {
                s.Append(c.R.ToString() + " " + c.G.ToString() + " " + c.B.ToString() + System.Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
