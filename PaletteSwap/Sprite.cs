using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PaletteSwap
{/*
    // abstract class for sprite and portrait
    // might be better to just combine them into one big color set that has several
    // images. maybe not b/c each one has its own byte representation
    public abstract class Palette_
    {
        // a set of "name", "color" pairs

        // ability to get "color" from a given name. Dictionary?

        // abilty to get a Byte[] representation 

        // constructor from Byte[] or string.

        // generate image (enum image types)

        // has several palette images

        // sprite inherits from this
        // eg dicsprite IS a sprite
        // sprite IS a palette



        // portrait inherits from this
        // is there a diff between sprite and portrait? maybenot
    }

    public abstract class PaletteImage_
    {
        // a base bitmap (static)

        // a color remap table

        // ability to output remapped Image

        // access colors from parent palette
    }

    public class Sprite
    {
        // todo: add 0500 to all of these and fix tests
        // todo: create a base abstract class to parent sprite and portrait
        // todo: create an 'image'? class that represents a neutral sprite, a
        // crusher top sprite, etc. This would have a base image and a remap table
        // and this class would have a list of these images objects, along with a 
        // byteArray output and a .col output
        // question: the base image should have access to colors from parent sprite
        // or portrait
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

        public static string spriteAsTextLine(string s)
        {
            s = "0500 " + s;
            string s_expected = Regex.Replace(s, @"\t|\n|\r", "");
            return s_expected;
        }

        public static int ROWLEN = 32;

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
            stripe,
            psychoglow,
            psychopunch1,
            psychopunch2,
            psychopunch3,
            psychopunch4,
            psychopunch5,
            crusherpads1,
            crusherpads2,
            crusherpads3,
            crusherpads4,
            crusherpads5,
            crushercostume1,
            crushercostume2,
            crushercostume3,
            crushercostume4,
            crusherflame1,
            crusherflame2,
            crusherhands1,
            crusherhands2,
        }

        public static Sprite LoadFromStream(byte[] b)
        {
            Sprite s = new Sprite();
            foreach (var k in colorsToMemOffsets.Keys)
            {
                int offset = colorsToMemOffsets[k][0];
                byte[] colbyte = new byte[2];
                colbyte[0] = b[offset];
                colbyte[1] = b[offset + 1];
                Color c_transparent = PaletteHelper.ByteToColor(colbyte);
                Color c = Color.FromArgb(255, c_transparent);
                s.SetColorFromAttributeLabel(k, c);
            }
            return s;
        }

        public static Sprite LoadFromColFormat(string s)
        {
            Sprite sp = new Sprite();
            var v = s.Split('\n');
            sp.skin1 = PaletteHelper.RGBFormatToColor(v[0]);
            sp.skin2 = PaletteHelper.RGBFormatToColor(v[1]);
            sp.skin3 = PaletteHelper.RGBFormatToColor(v[2]);
            sp.skin4 = PaletteHelper.RGBFormatToColor(v[3]);
            sp.costume1 = PaletteHelper.RGBFormatToColor(v[4]);
            sp.costume2 = PaletteHelper.RGBFormatToColor(v[5]);
            sp.costume3 = PaletteHelper.RGBFormatToColor(v[6]);
            sp.costume4 = PaletteHelper.RGBFormatToColor(v[7]);
            sp.costume5 = PaletteHelper.RGBFormatToColor(v[8]);
            sp.pads1 = PaletteHelper.RGBFormatToColor(v[9]);
            sp.pads2 = PaletteHelper.RGBFormatToColor(v[10]);
            sp.pads3 = PaletteHelper.RGBFormatToColor(v[11]);
            sp.pads4 = PaletteHelper.RGBFormatToColor(v[12]);
            sp.pads5 = PaletteHelper.RGBFormatToColor(v[13]);
            sp.stripe = PaletteHelper.RGBFormatToColor(v[14]);
            sp.psychoglow = PaletteHelper.RGBFormatToColor(v[15]);
            sp.psychopunch1 = PaletteHelper.RGBFormatToColor(v[16]);
            sp.psychopunch2 = PaletteHelper.RGBFormatToColor(v[17]);
            sp.psychopunch3 = PaletteHelper.RGBFormatToColor(v[18]);
            sp.psychopunch4 = PaletteHelper.RGBFormatToColor(v[19]);
            sp.psychopunch5 = PaletteHelper.RGBFormatToColor(v[20]);
            sp.crushercostume1 = PaletteHelper.RGBFormatToColor(v[21]);
            sp.crushercostume2 = PaletteHelper.RGBFormatToColor(v[22]);
            sp.crushercostume3 = PaletteHelper.RGBFormatToColor(v[23]);
            sp.crushercostume4 = PaletteHelper.RGBFormatToColor(v[24]);
            sp.crusherpads1 = PaletteHelper.RGBFormatToColor(v[25]);
            sp.crusherpads2 = PaletteHelper.RGBFormatToColor(v[26]);
            sp.crusherpads3 = PaletteHelper.RGBFormatToColor(v[27]);
            sp.crusherpads4 = PaletteHelper.RGBFormatToColor(v[28]);
            sp.crusherpads5 = PaletteHelper.RGBFormatToColor(v[29]);
            sp.crusherhands1 = PaletteHelper.RGBFormatToColor(v[30]);
            sp.crusherhands2 = PaletteHelper.RGBFormatToColor(v[31]);
            sp.crusherflame1 = PaletteHelper.RGBFormatToColor(v[32]);
            sp.crusherflame2 = PaletteHelper.RGBFormatToColor(v[33]);

            return sp;
        }

        public Sprite()
        {

        }

        public static Dictionary<int, bool> unusedOffsets = new Dictionary<int, bool>
        {
            { 34, true },
            { 35, true },
            { 68, true },
            { 69, true },
            { 94, true },
            { 95, true },
        };

        public static Dictionary<SPRITE_COLORS, List<int>> colorsToMemOffsets = new Dictionary<SPRITE_COLORS, List<int>>
        {
            { SPRITE_COLORS.pads5, new List<int>() { 2, ROWLEN * 3 + 2, ROWLEN * 4 + 2 } },
            { SPRITE_COLORS.costume5, new List<int>() { 4, ROWLEN * 3 + 4, ROWLEN * 4 + 4 } },
            { SPRITE_COLORS.costume4, new List<int>() { 6, ROWLEN * 3 + 6, ROWLEN * 4 + 6 } },
            { SPRITE_COLORS.costume3, new List<int>() { 8, ROWLEN * 3 + 8, ROWLEN * 4 + 8 } },
            { SPRITE_COLORS.costume2, new List<int>() { 10, ROWLEN * 3 + 10, ROWLEN * 4 + 10 } },
            { SPRITE_COLORS.costume1, new List<int>() { 12, ROWLEN * 3 + 12, ROWLEN * 4 + 12 } },
            { SPRITE_COLORS.pads4, new List<int>() { 14 } },
            { SPRITE_COLORS.stripe, new List<int>() { 16, ROWLEN * 3 + 16 } },
            { SPRITE_COLORS.pads1, new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
            { SPRITE_COLORS.pads2, new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
            { SPRITE_COLORS.pads3, new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22 } },
            { SPRITE_COLORS.skin1, new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24, ROWLEN * 4 + 24 } },
            { SPRITE_COLORS.skin2, new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 3 + 26, ROWLEN * 4 + 26 } },
            { SPRITE_COLORS.skin3, new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 3 + 28, ROWLEN * 4 + 28 } },
            { SPRITE_COLORS.skin4, new List<int>() { 30, ROWLEN * 1 + 30, ROWLEN * 3 + 30, ROWLEN * 4 + 30 } },
            { SPRITE_COLORS.crusherpads5, new List<int>() { ROWLEN * 2 + 2 } },
            { SPRITE_COLORS.crushercostume4, new List<int>() { ROWLEN * 2 + 6 } },
            { SPRITE_COLORS.crushercostume3, new List<int>() { ROWLEN * 2 + 8 } },
            { SPRITE_COLORS.crushercostume2, new List<int>() { ROWLEN * 2 + 10 } },
            { SPRITE_COLORS.crushercostume1, new List<int>() { ROWLEN * 2 + 12 } },
            { SPRITE_COLORS.crusherpads4, new List<int>() { ROWLEN * 2 + 14 } },
            { SPRITE_COLORS.crusherflame1, new List<int>() { ROWLEN * 2 + 16 } },
            { SPRITE_COLORS.crusherpads1, new List<int>() { ROWLEN * 2 + 18 } },
            { SPRITE_COLORS.crusherpads2, new List<int>() { ROWLEN * 2 + 20 } },
            { SPRITE_COLORS.crusherpads3, new List<int>() { ROWLEN * 2 + 22 } },
            { SPRITE_COLORS.crusherflame2, new List<int>() { ROWLEN * 2 + 24 } },
            { SPRITE_COLORS.crusherhands1, new List<int>() { ROWLEN * 2 + 26 } },
            { SPRITE_COLORS.crusherhands2, new List<int>() { ROWLEN * 2 + 28 } },
            { SPRITE_COLORS.psychoglow, new List<int>() { ROWLEN * 3 + 14 } },
            { SPRITE_COLORS.psychopunch1, new List<int>() { ROWLEN * 4 + 14 } },
            { SPRITE_COLORS.psychopunch2, new List<int>() { ROWLEN * 4 + 16 } },
            { SPRITE_COLORS.psychopunch3, new List<int>() { ROWLEN * 4 + 18 } },
            { SPRITE_COLORS.psychopunch4, new List<int>() { ROWLEN * 4 + 20 } },
            { SPRITE_COLORS.psychopunch5, new List<int>() { ROWLEN * 4 + 22 } },
        };


        public Sprite(string s)
        {
            /* var b = PaletteHelper.StringToByteStream("0500 " + s);

             foreach (var k in colorsToMemOffsets.Keys)
             {
                 int offset = colorsToMemOffsets[k][0];
                 // field.kname = b[offset
                 /*Color col = this.ColorFromSpriteColor(k);
                 byte[] c = PaletteHelper.ColorToByte(col);
                 foreach (int offset in colorsToMemOffsets[k])
                 {
                     b[offset] = c[0];
                     b[offset + 1] = c[1];
                 }
             }*/
             /*
            var v = s.Split('\n');
            this.row1 = v[0].Trim();
            this.row2 = v[1].Trim();
            this.row3 = v[2].Trim();
            this.row4 = v[3].Trim();
            this.row5 = v[4].Trim();

            var r1 = row1.Split(' ');
            pads5 = PaletteHelper.MemFormatToColor(r1[0]);
            pads4 = PaletteHelper.MemFormatToColor(r1[6]);
            pads3 = PaletteHelper.MemFormatToColor(r1[10]);
            pads2 = PaletteHelper.MemFormatToColor(r1[9]);
            pads1 = PaletteHelper.MemFormatToColor(r1[8]);

            costume1 = PaletteHelper.MemFormatToColor(r1[5]);
            costume2 = PaletteHelper.MemFormatToColor(r1[4]);
            costume3 = PaletteHelper.MemFormatToColor(r1[3]);
            costume4 = PaletteHelper.MemFormatToColor(r1[2]);
            costume5 = PaletteHelper.MemFormatToColor(r1[1]);

            skin1 = PaletteHelper.MemFormatToColor(r1[11]);
            skin2 = PaletteHelper.MemFormatToColor(r1[12]);
            skin3 = PaletteHelper.MemFormatToColor(r1[13]);
            skin4 = PaletteHelper.MemFormatToColor(r1[14]);

            stripe = PaletteHelper.MemFormatToColor(r1[7]);

            var r2 = row2.Split(' ');

            var r3 = row3.Split(' ');
            crusherpads1 = PaletteHelper.MemFormatToColor(r3[8]);
            crusherpads2 = PaletteHelper.MemFormatToColor(r3[9]);
            crusherpads3 = PaletteHelper.MemFormatToColor(r3[10]);
            crusherpads4 = PaletteHelper.MemFormatToColor(r3[6]);
            crusherpads5 = PaletteHelper.MemFormatToColor(r3[0]);

            crushercostume1 = PaletteHelper.MemFormatToColor(r3[5]);
            crushercostume2 = PaletteHelper.MemFormatToColor(r3[4]);
            crushercostume3 = PaletteHelper.MemFormatToColor(r3[3]);
            crushercostume4 = PaletteHelper.MemFormatToColor(r3[2]);

            crusherhands1 = PaletteHelper.MemFormatToColor(r3[12]);
            crusherhands2 = PaletteHelper.MemFormatToColor(r3[13]);

            crusherflame1 = PaletteHelper.MemFormatToColor(r3[7]);
            crusherflame2 = PaletteHelper.MemFormatToColor(r3[11]);

            var r4 = row4.Split(' ');
            psychoglow = PaletteHelper.MemFormatToColor(r4[6]);

            var r5 = row5.Split(' ');
            psychopunch1 = PaletteHelper.MemFormatToColor(r5[6]);
            psychopunch2 = PaletteHelper.MemFormatToColor(r5[7]);
            psychopunch3 = PaletteHelper.MemFormatToColor(r5[8]);
            psychopunch4 = PaletteHelper.MemFormatToColor(r5[9]);
            psychopunch5 = PaletteHelper.MemFormatToColor(r5[10]);
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

        public Bitmap GenerateStandingSpriteFromRemap()
        {
            Bitmap b = new Bitmap(Properties.Resources.dicstand1);
            var remapTable = StandingSpriteColorsRemapTable();
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

        public Bitmap GenerateStandingSprite()
        {
            var orig = new Sprite(Sprite.bis1sprite);
            var orig_colors = orig.StandingSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicstand1);
            var my_colors = this.StandingSpriteColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GeneratePsychoPunchSprite()
        {
            var orig = new Sprite(Sprite.bis1sprite);
            var orig_colors = orig.PsychoPunchSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicmp1);
            var my_colors = this.PsychoPunchSpriteColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GeneratePsychoPrepSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.PsychoPrepSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.dicpsychoprep5);
            var my_colors = this.PsychoPrepSpriteColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateCrusherTopSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.CrusherSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.diccrusher1_5);
            var my_colors = this.CrusherSpriteColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
            return ret;
        }

        public Bitmap GenerateCrusherSideSprite()
        {
            var orig = new Sprite(Sprite.bis5sprite);
            var orig_colors = orig.CrusherSpriteColorsArray();
            Bitmap b = new Bitmap(Properties.Resources.diccrusher2_5);
            var my_colors = this.CrusherSpriteColorsArray();
            var ret = PaletteHelper.PaletteSwap(b, orig_colors, my_colors);
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
                Color col = this.ColorFromSpriteColor(k);
                byte[] c = PaletteHelper.ColorToByte(col);
                foreach (int offset in colorsToMemOffsets[k])
                {
                    b[offset] = c[0];
                    b[offset + 1] = c[1];
                }
            }
            return b;
        }

        public Color ColorFromSpriteColor(SPRITE_COLORS label)
        {
            Type myType = GetType();
            var myFieldInfo = myType.GetField(label.ToString());
            return (Color)myFieldInfo.GetValue(this);
        }


        public void SetColorFromAttributeLabel(SPRITE_COLORS label, Color c)
        {
            Type myType = GetType();
            var myFieldInfo = myType.GetField(label.ToString());
            myFieldInfo.SetValue(this, c);
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
    */
}
