using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

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
    }

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


        public static Portrait LoadFromColFormat(string s)
        {
            Portrait p = new Portrait();
            var v = s.Split('\n');
            p.skin1 = Palette.ColFormatToColor(v[0]);
            p.skin2 = Palette.ColFormatToColor(v[1]);
            p.skin3 = Palette.ColFormatToColor(v[2]);
            p.skin4 = Palette.ColFormatToColor(v[3]);
            p.skin5 = Palette.ColFormatToColor(v[4]);
            p.skin6 = Palette.ColFormatToColor(v[5]);
            p.skin7 = Palette.ColFormatToColor(v[6]);
            p.costume1 = Palette.ColFormatToColor(v[7]);
            p.costume2 = Palette.ColFormatToColor(v[8]);
            p.costume3 = Palette.ColFormatToColor(v[9]);
            p.costume4 = Palette.ColFormatToColor(v[10]);
            p.teeth1 = Palette.ColFormatToColor(v[11]);
            p.teeth2 = Palette.ColFormatToColor(v[12]);
            p.teeth3 = Palette.ColFormatToColor(v[13]);
            p.teeth4 = Palette.ColFormatToColor(v[14]);
            p.piping1 = Palette.ColFormatToColor(v[15]);
            p.piping2 = Palette.ColFormatToColor(v[16]);
            p.piping3 = Palette.ColFormatToColor(v[17]);
            p.piping4 = Palette.ColFormatToColor(v[18]);
            p.pipingloss1 = Palette.ColFormatToColor(v[19]);
            p.pipingloss2 = Palette.ColFormatToColor(v[20]);
            p.pipingloss3 = Palette.ColFormatToColor(v[21]);
            p.pipingloss4 = Palette.ColFormatToColor(v[22]);
            p.costumeloss1 = Palette.ColFormatToColor(v[23]);
            p.costumeloss2 = Palette.ColFormatToColor(v[24]);
            p.costumeloss3 = Palette.ColFormatToColor(v[25]);
            p.costumeloss4 = Palette.ColFormatToColor(v[26]);
            p.blood1 = Palette.ColFormatToColor(v[27]);
            p.blood2 = Palette.ColFormatToColor(v[28]);
            p.blood3 = Palette.ColFormatToColor(v[29]);

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
            skin1 = Palette.MemFormatToColor(r1[0]);
            skin2 = Palette.MemFormatToColor(r1[1]);
            skin3 = Palette.MemFormatToColor(r1[2]);
            skin4 = Palette.MemFormatToColor(r1[3]);
            skin5 = Palette.MemFormatToColor(r1[4]);
            skin6 = Palette.MemFormatToColor(r1[5]);
            skin7 = Palette.MemFormatToColor(r1[6]);
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
            return String.Join(" ", new[] { skin1, skin2, skin3, skin4, skin5, skin6, skin7 }.Select(x => Palette.ColorToMemFormat(x)));
        }

        public string costumerow()
        {
            return String.Join(" ", new[] { costume1, costume2, costume3, costume4 }.Select(x => Palette.ColorToMemFormat(x)));
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
            var my_colors = this.VictoryColorsArray();
            var ret = Palette.PaletteSwap(b, PaletteHelper.orig_victory_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateLossTopPortrait()
        {
            var orig = new Portrait(Portrait.bis5portrait);
            var orig_colors = orig.LossTopColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicportraitlosstop5);
            var my_colors = this.LossTopColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateLossBottomPortrait()
        {
            var orig = new Portrait(Portrait.bis5portrait);
            var orig_colors = orig.LossBottomColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicportraitlossbottom5);
            var my_colors = this.LossBottomColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }
        public Bitmap GenerateLossPortrait()
        {
            var top = GenerateLossTopPortrait();
            var bottom = GenerateLossBottomPortrait();
            var loss = Palette.overlayImage(top, bottom);
            return loss;
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

    public class Sprite
    {

        public static readonly string bis0sprite = @"0007 0800 2A02 4C00 6D03 8E00 300A B00F F70F B00F 700F FC0F C80D 7309 4005 0000 
0007 2302 3403 5605 6706 7807 8A08 9B09 F70F B00F 700F FC0F C80D 7309 4005 0000 
5009 6800 7A02 9C04 CE06 EF0B 700B FC0F FF0F F80F B00E DB07 B805 8604 4300 0000 
0007 0800 2A02 4C00 6D03 8E00 FF0F B00F F70F B00F 700F FC0F C80D 7309 4005 0000 
0007 0800 2A02 4C00 6D03 8E00 FF0F E90F A40E 700E 400D FC0F C80D 7309 4005 0000";
        public static readonly string bis1sprite = @"0007 7300 9503 C705 EB08 FE0B 000A 400C B70F 840F 500D EB0F B70E 740A 4306 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
0007 4006 7007 9309 C50C F90D 540A FB0F FF0F CB0F 870D FE0A EA07 C704 9500 0000 
0007 7300 9503 C705 EB08 FE0B EB0F 400C B70F 840F 500D EB0F B70E 740A 4306 0000 
0007 7300 9503 C705 EB08 FE0B FF0D CE07 9D02 7B00 6800 EB0F B70E 740A 4306 0000";
        public static readonly string bis2sprite = @"0005 5505 8808 BB0B DD0D FF0F 0007 7F00 770F 440C 000A ED0F B90D 760A 4306 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 770F 440C 000A ED0F B90D 760A 4306 0000 
3208 6506 890A 9B0C CD0E DE0F 520A A90F D90F A70F 640D FE0B DC09 B906 7604 0000 
0005 5505 8808 BB0B DD0D FF0F A80F 7F00 770F 440C 000A ED0F DC0D 760A 4306 0000 
0005 5505 8808 BB0B DD0D FF0F EF0C DE07 9D02 7B00 3402 ED0F B90D 760A 4306 0000";
        public static readonly string bis3sprite = @"0005 4600 6802 8903 BC05 DE06 0007 D70F 770F 440C 000A FE0E B90C 7609 5307 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 770F 440C 000A FE0E B90C 7609 5307 0000 
3008 7B04 9C05 BD07 CE09 DF0B 500C EC0F CA0F 860F 640D CF0F AB0E 890C 570A 0000 
0005 4600 6802 8903 BC05 DE06 A80F D70F 770F 440C 000A FE0E B90C 7609 5307 0000 
0005 4600 6802 8903 BC05 DE06 DF0F AC0F 7A0D 590B 0809 FE0E B90C 7609 5307 0000";
        public static readonly string bis4sprite = @"3400 4205 6307 8509 A80C DB0E 4500 550F 9A08 7806 5603 FF0E AB0C 7808 4005 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 9A08 7806 5603 FF0E AB0C 7808 4005 0000 
4404 6407 8609 A70B C80D EA0F 6606 FF0F DC0E A90B 8709 E90F A80E 870C 650A 0000 
3400 4205 6307 8509 A80C DB0E CD0B 550F 9A08 7806 5603 FF0E AB0C 7808 4005 0000 
3400 4205 6307 8509 A80C DB0E FF0F DE0D AB0A 8908 6706 FF0E AB0C 7808 4005 0000";
        public static readonly string bis5sprite = @"4101 0606 4909 6C0C 8E0E BF0F 6402 330D FD0A DB06 A803 FE0E B90C 7609 5307 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 FD0A DB06 A803 FE0E B90C 7609 5307 0000 
5200 0606 6A0A 7C0C AE0E DF0F 7305 EF0D FF0E DA08 9506 F80E EC0B CA08 A706 0000 
4101 0606 4909 6C0C 8E0E BF0F FE0D 330D FD0A DB06 A803 FE0E B90C 7609 5307 0000 
4101 0606 4909 6C0C 8E0E BF0F FF0E DF0C AF09 8F07 6F05 FE0E B90C 7608 5307 0000";
        public static readonly string bis6sprite = @"3402 4404 5605 6706 7807 9A09 5604 550F EF0E AD0A 7906 FE0E B90C 7609 5307 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 EF0E AD0A 7906 FE0E B90C 7609 5307 0000 
3402 5504 7706 9907 BB08 CC09 6208 DB0F FF0F CA0D 850A 750F BA0F 770D 440C 0000 
3402 4404 5605 6706 7807 9A09 CD0B 550F EF0E AD0A 7906 FE0E B90C 7609 5307 0000 
3402 4404 5605 6706 7807 9A09 EF0F CE0F 9D0F 7B0E 570E FE0E B90C 7609 5307 0000";
        public static readonly string bis7sprite = @"5006 4006 6409 850C A70E D90F 6008 6B0E EC0F C70E 940B EB0F B70E 740A 4306 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 EC0F C70E 940B EB0F B70E 740A 4306 0000 
5008 7508 970A A80C C90E DB0F 720A FD0F D90F A70F 940D CF0F 9D0B 7A09 5807 0000 
5006 4006 6409 850C A70E D90F E80F 6B0E EC0F C70E 940B EB0F B70E 740A 4306 0000 
5006 4006 6409 850C A70E D90F CF0D 8E0B 5D08 4B07 0806 EB0F B70E 740A 4306 0000";
        public static readonly string bis8sprite = @"3402 0006 0009 320C 650E 870F 5604 7F00 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
5007 0006 600A 720C A50E D80F 6208 EB0F FF0F CA0D 850A EF0C CD09 9B06 6704 0000 
3402 0006 0009 320C 650E 870F EB0F 7F00 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
3402 0006 0009 320C 650E 870F FF0D EC07 D902 B700 8600 D90F 960E 640A 4306 0000 ";
        public static readonly string bis9sprite = @"3400 0006 0009 320C 650E 870F 5604 9C00 FF0C CD09 9A06 D90F 960E 640A 4306 0000 
3402 2302 3403 5605 6706 7807 8A08 9B09 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
5007 0006 600A 720C A50E D80F 6208 EB0F FF0F CA0D 850A EF0C CD09 9B06 6704 0000 
3402 0006 0009 320C 650E 870F EB0F 7F00 EF0E AD0A 7906 D90F 960E 640A 4306 0000 
3402 0006 0009 320C 650E 870F FF0D EC07 D902 B700 8600 D90F 960E 640A 4306 0000";

        public string row1;
        public string row2;
        public string row3;
        public string row4;
        public string row5;

        public Color skin1;
        public Color skin2;
        public Color skin3;
        public Color skin4;

        public Color costume1;
        public Color costume2;
        public Color costume3;
        public Color costume4;
        public Color costume5;

        public Color pads1;
        public Color pads2;
        public Color pads3;
        public Color pads4;
        public Color pads5;

        public Color stripe;
        public Color psychoglow;

        public Color psychopunch1;
        public Color psychopunch2;
        public Color psychopunch3;
        public Color psychopunch4;
        public Color psychopunch5;

        public Color crusherpads1;
        public Color crusherpads2;
        public Color crusherpads3;
        public Color crusherpads4;
        public Color crusherpads5;

        public Color crushercostume1;
        public Color crushercostume2;
        public Color crushercostume3;
        public Color crushercostume4;

        public Color crusherflame1;
        public Color crusherflame2;

        public Color crusherhands1;
        public Color crusherhands2;

        public static int MEMSTART = 0x42E7C;
        public static int MEMEND = 0x42F1E;
        public static int MEMLEN = MEMEND - MEMSTART;

        public enum SPRITE_COLORS
        {
            skin1,
            skin2,
            skin3,
            skin4,
            costume1,
            costume2,
            costume3,
            costume4,
            costume5,
            pads1,
            pads2,
            pads3,
            pads4,
            pads5,
            stripe
        }

        public static Dictionary<SPRITE_COLORS, List<int>> colorsToMemOffsets = new Dictionary<SPRITE_COLORS, List<int>>
        {
            { SPRITE_COLORS.pads5, new List<int>() { 2, 98, 130 } },
            { SPRITE_COLORS.skin1, new List<int>() { 4, 100, 132 } },
        };

        public static Sprite LoadFromColFormat(string s)
        {
            Sprite sp = new Sprite();
            var v = s.Split('\n');
            sp.skin1 = Palette.ColFormatToColor(v[0]);
            sp.skin2 = Palette.ColFormatToColor(v[1]);
            sp.skin3 = Palette.ColFormatToColor(v[2]);
            sp.skin4 = Palette.ColFormatToColor(v[3]);
            sp.costume1 = Palette.ColFormatToColor(v[4]);
            sp.costume2 = Palette.ColFormatToColor(v[5]);
            sp.costume3 = Palette.ColFormatToColor(v[6]);
            sp.costume4 = Palette.ColFormatToColor(v[7]);
            sp.costume5 = Palette.ColFormatToColor(v[8]);
            sp.pads1 = Palette.ColFormatToColor(v[9]);
            sp.pads2 = Palette.ColFormatToColor(v[10]);
            sp.pads3 = Palette.ColFormatToColor(v[11]);
            sp.pads4 = Palette.ColFormatToColor(v[12]);
            sp.pads5 = Palette.ColFormatToColor(v[13]);
            sp.stripe = Palette.ColFormatToColor(v[14]);
            sp.psychoglow = Palette.ColFormatToColor(v[15]);
            sp.psychopunch1 = Palette.ColFormatToColor(v[16]);
            sp.psychopunch2 = Palette.ColFormatToColor(v[17]);
            sp.psychopunch3 = Palette.ColFormatToColor(v[18]);
            sp.psychopunch4 = Palette.ColFormatToColor(v[19]);
            sp.psychopunch5 = Palette.ColFormatToColor(v[20]);
            sp.crushercostume1 = Palette.ColFormatToColor(v[21]);
            sp.crushercostume2 = Palette.ColFormatToColor(v[22]);
            sp.crushercostume3 = Palette.ColFormatToColor(v[23]);
            sp.crushercostume4 = Palette.ColFormatToColor(v[24]);
            sp.crusherpads1 = Palette.ColFormatToColor(v[25]);
            sp.crusherpads2 = Palette.ColFormatToColor(v[26]);
            sp.crusherpads3 = Palette.ColFormatToColor(v[27]);
            sp.crusherpads4 = Palette.ColFormatToColor(v[28]);
            sp.crusherpads5 = Palette.ColFormatToColor(v[29]);
            sp.crusherhands1 = Palette.ColFormatToColor(v[30]);
            sp.crusherhands2 = Palette.ColFormatToColor(v[31]);
            sp.crusherflame1 = Palette.ColFormatToColor(v[32]);
            sp.crusherflame2 = Palette.ColFormatToColor(v[33]);

            return sp;
        }

        public Sprite()
        {

        }

        public Sprite(string s)
        {
            var v = s.Split('\n');
            this.row1 = v[0].Trim();
            this.row2 = v[1].Trim();
            this.row3 = v[2].Trim();
            this.row4 = v[3].Trim();
            this.row5 = v[4].Trim();

            var r1 = row1.Split(' ');
            pads5 = Palette.MemFormatToColor(r1[0]);
            pads4 = Palette.MemFormatToColor(r1[6]);
            pads3 = Palette.MemFormatToColor(r1[10]);
            pads2 = Palette.MemFormatToColor(r1[9]);
            pads1 = Palette.MemFormatToColor(r1[8]);

            costume1 = Palette.MemFormatToColor(r1[5]);
            costume2 = Palette.MemFormatToColor(r1[4]);
            costume3 = Palette.MemFormatToColor(r1[3]);
            costume4 = Palette.MemFormatToColor(r1[2]);
            costume5 = Palette.MemFormatToColor(r1[1]);

            skin1 = Palette.MemFormatToColor(r1[11]);
            skin2 = Palette.MemFormatToColor(r1[12]);
            skin3 = Palette.MemFormatToColor(r1[13]);
            skin4 = Palette.MemFormatToColor(r1[14]);

            stripe = Palette.MemFormatToColor(r1[7]);

            var r2 = row2.Split(' ');

            var r3 = row3.Split(' ');
            crusherpads1 = Palette.MemFormatToColor(r3[8]);
            crusherpads2 = Palette.MemFormatToColor(r3[9]);
            crusherpads3 = Palette.MemFormatToColor(r3[10]);
            crusherpads4 = Palette.MemFormatToColor(r3[6]);
            crusherpads5 = Palette.MemFormatToColor(r3[0]);

            crushercostume1 = Palette.MemFormatToColor(r3[5]);
            crushercostume2 = Palette.MemFormatToColor(r3[4]);
            crushercostume3 = Palette.MemFormatToColor(r3[3]);
            crushercostume4 = Palette.MemFormatToColor(r3[2]);

            crusherhands1 = Palette.MemFormatToColor(r3[12]);
            crusherhands2 = Palette.MemFormatToColor(r3[13]);

            crusherflame1 = Palette.MemFormatToColor(r3[7]);
            crusherflame2 = Palette.MemFormatToColor(r3[11]);

            var r4 = row4.Split(' ');
            psychoglow = Palette.MemFormatToColor(r4[6]);

            var r5 = row5.Split(' ');
            psychopunch1 = Palette.MemFormatToColor(r5[6]);
            psychopunch2 = Palette.MemFormatToColor(r5[7]);
            psychopunch3 = Palette.MemFormatToColor(r5[8]);
            psychopunch4 = Palette.MemFormatToColor(r5[9]);
            psychopunch5 = Palette.MemFormatToColor(r5[10]);
        }

        public Color[] CrusherSpriteColorsArray()
        {
            return new[] { crushercostume1, crushercostume2, crushercostume3, crushercostume4,
                crusherflame1, crusherflame2, crusherhands1, crusherhands2,
                crusherpads1, crusherpads2, crusherpads3, crusherpads4, crusherpads5
            };
        }

        public Color[] StandingSpriteColorsArray()
        {
            return new[] { skin1, skin2, skin3, skin4, stripe, pads1, pads2, pads3, pads4, pads5,
            costume1, costume2, costume3, costume4, costume5};
        }

        public Color[] PsychoPrepSpriteColorsArray()
        {
            return new[] { skin1, skin2, skin3, skin4, stripe, pads1, pads2, pads3, pads4, pads5,
            costume1, costume2, costume3, costume4, costume5, psychoglow};
        }

        public Color[] PsychoPunchSpriteColorsArray()
        {
            return new[] { skin1, skin2, skin3, skin4, stripe, pads1, pads2, pads3, pads4, pads5,
            costume1, costume2, costume3, costume4, costume5, psychopunch1, psychopunch2,
            psychopunch3, psychopunch4, psychopunch5};
        }

        public Color[] FullSpriteColorsArray()
        {
            return new[] { skin1, skin2, skin3, skin4,
            costume1, costume2, costume3, costume4, costume5,
                pads1, pads2, pads3, pads4, pads5,
                stripe, psychoglow,
                psychopunch1, psychopunch2, psychopunch3, psychopunch4, psychopunch5,
                crushercostume1, crushercostume2, crushercostume3, crushercostume4,
                crusherpads1, crusherpads2, crusherpads3, crusherpads4, crusherpads5,
                crusherhands1, crusherhands2, crusherflame1, crusherflame2
            };
        }
        public ColorMap[] StandingSpriteColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.standing_sprite_colors1, StandingSpriteColorsArray());
        }

        public ColorMap[] PsychoPunchColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.psychopunch_sprite_colors5, PsychoPunchSpriteColorsArray());
        }

        public ColorMap[] PsychoPrepColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.psychoprep_sprite_colors5, PsychoPrepSpriteColorsArray());
        }

        public ColorMap[] CrusherColorsRemapTable()
        {
            return PaletteHelper.GenerateColorMap(PaletteHelper.crusher_sprite_colors5, CrusherSpriteColorsArray());
        }

        public Bitmap GenerateStandingSprite()
        {
            var orig = new Sprite(Sprite.bis1sprite);
            var orig_colors = orig.StandingSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicstand1);
            var my_colors = this.StandingSpriteColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GeneratePsychoPunchSprite()
        {
            var orig = new Sprite(Sprite.bis1sprite);
            var orig_colors = orig.PsychoPunchSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicmp1);
            var my_colors = this.PsychoPunchSpriteColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GeneratePsychoPrepSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.PsychoPrepSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicpsychoprep5);
            var my_colors = this.PsychoPrepSpriteColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateCrusherTopSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.CrusherSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.diccrusher1_5);
            var my_colors = this.CrusherSpriteColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateCrusherSideSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.CrusherSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.diccrusher2_5);
            var my_colors = this.CrusherSpriteColorsArray();
            var ret = Palette.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Byte[] ByteStream()
        {
            string s = "0500 0007 0800 2A02 4C00 6D03 8E00 300A B00F F70F B00F 700F FC0F C80D 7309 4005 0000 " +
"0007 2302 3403 5605 6706 7807 8A08 9B09 F70F B00F 700F FC0F C80D 7309 4005 0000 " +
"5009 6800 7A02 9C04 CE06 EF0B 700B FC0F FF0F F80F B00E DB07 B805 8604 4300 0000 " +
"0007 0800 2A02 4C00 6D03 8E00 FF0F B00F F70F B00F 700F FC0F C80D 7309 4005 0000 " +
"0007 0800 2A02 4C00 6D03 8E00 FF0F E90F A40E 700E 400D FC0F C80D 7309 4005 0000";
            byte[] b = PaletteHelper.StringToByteStream(s);

            foreach (var k in colorsToMemOffsets.Keys)
            {
                foreach (int offset in colorsToMemOffsets[k])
                {

                }
            }
            return b;
        }

        public Color ColorFromSpriteColor(SPRITE_COLORS label)
        {
            Type myType = GetType();
            var myPropInfo = myType.GetField("skin1");
            var foo = myPropInfo.GetValue(this);
            var property = this.GetType().GetProperty("skin1");
            return (Color)property.GetValue(this);
            switch (label)
            {
                case SPRITE_COLORS.skin1:
                    return this.skin1;
                case SPRITE_COLORS.skin2:
                    return this.skin2;
                case SPRITE_COLORS.skin3:
                    return this.skin3;
                case SPRITE_COLORS.skin4:
                    return this.skin4;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }

        public string ToColFormat()
        {
            var Colors = FullSpriteColorsArray();
            StringBuilder s = new StringBuilder();
            foreach (Color c in Colors)
            {
                s.Append(c.R.ToString() + " " + c.G.ToString() + " " + c.B.ToString() + System.Environment.NewLine);
            }
            return s.ToString();
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
                    swappedImg.SetPixel(x, y, swappedColor);
                }
            }
            return swappedImg;
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