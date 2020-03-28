using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    // palette has label->color

    // stream: object that can print out stream and load stream
    // stream can go to string and back

    // image: can print image from palette

    public struct PaletteConfig
    {
        public Dictionary<string, List<int>> labelOffsets;
        public List<ColorOffset> defaultColorOffsets;
        public List<int> unusedOffsets;
        public int streamLength;
    }

    public class Character
    {
        public static int ROWLEN = 32;
        public static string defaults = "0007 2302 3403 5605 6706 7807 8A08 9B09";
        public static int defaultoffset = 32;
        public static List<ColorOffset> defaultentries = new List<ColorOffset>();
        public static Dictionary<string, List<int>> dictatorSpriteOffsets = new Dictionary<string, List<int>>
        {
            { "pads5", new List<int>() { 0, ROWLEN * 3 + 0, ROWLEN * 4 + 0 } },
            { "costume5", new List<int>() { 2, ROWLEN * 3 + 2, ROWLEN * 4 + 2 } },
            { "costume4", new List<int>() { 4, ROWLEN * 3 + 4, ROWLEN * 4 + 4 } },
            { "costume3", new List<int>() { 6, ROWLEN * 3 + 6, ROWLEN * 4 + 6 } },
            { "costume2", new List<int>() { 8, ROWLEN * 3 + 8, ROWLEN * 4 + 8 } },
            { "costume1", new List<int>() { 10, ROWLEN * 3 + 10, ROWLEN * 4 + 10 } },
            { "pads4", new List<int>() { 12 } },
            { "stripe", new List<int>() { 14, ROWLEN * 3 + 14 } },
            { "pads1", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
            { "pads2", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
            { "pads3", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
            { "skin1", new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22, ROWLEN * 4 + 22 } },
            { "skin2", new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24, ROWLEN * 4 + 24 } },
            { "skin3", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 3 + 26, ROWLEN * 4 + 26 } },
            { "skin4", new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 3 + 28, ROWLEN * 4 + 28 } },

            { "crusherpads5", new List<int>() { ROWLEN * 2 + 0 } },
            { "crushercostume4", new List<int>() { ROWLEN * 2 + 4 } },
            { "crushercostume3", new List<int>() { ROWLEN * 2 + 6 } },
            { "crushercostume2", new List<int>() { ROWLEN * 2 + 8 } },
            { "crushercostume1", new List<int>() { ROWLEN * 2 + 10 } },
            { "crusherpads4", new List<int>() { ROWLEN * 2 + 12 } },
            { "crusherflame1", new List<int>() { ROWLEN * 2 + 14 } },
            { "crusherpads1", new List<int>() { ROWLEN * 2 + 16 } },
            { "crusherpads2", new List<int>() { ROWLEN * 2 + 18 } },
            { "crusherpads3", new List<int>() { ROWLEN * 2 + 20 } },
            { "crusherflame2", new List<int>() { ROWLEN * 2 + 22 } },
            { "crusherhands1", new List<int>() { ROWLEN * 2 + 24 } },
            { "crusherhands2", new List<int>() { ROWLEN * 2 + 26 } },
            { "psychoglow", new List<int>() { ROWLEN * 3 + 12 } },
            { "psychopunch1", new List<int>() { ROWLEN * 4 + 12 } },
            { "psychopunch2", new List<int>() { ROWLEN * 4 + 14 } },
            { "psychopunch3", new List<int>() { ROWLEN * 4 + 16 } },
            { "psychopunch4", new List<int>() { ROWLEN * 4 + 18 } },
            { "psychopunch5", new List<int>() { ROWLEN * 4 + 20 } },
        };
        // put in dictator unused offsets
        // and dictator default values

        public Palette sprite;
        public Palette portrait;
        public enum CHARACTERS { Dictator, Claw };
        public enum BUTTONS { lp, mp, hp, lk, mk, hk, start, hold, old1, old2 };

        public static Character createDefaultCharacter(CHARACTERS characater, BUTTONS button)
        {
            var c = new Character();
            if (characater == CHARACTERS.Dictator)
            {
                Palette s = new Palette();
                Palette p = new Palette();
                c.sprite = s;
                s.setAllOffSets(dictatorSpriteOffsets);
                byte[] b = new byte[0];                
                switch (button)
                {
                    case BUTTONS.lp:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis0sprite);
                        break;
                    case BUTTONS.mp:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis1sprite);
                        break;
                    case BUTTONS.hp:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis2sprite);
                        break;
                    case BUTTONS.lk:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis3sprite);
                        break;
                    case BUTTONS.mk:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis4sprite);
                        break;
                    case BUTTONS.hk:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis5sprite);
                        break;
                    case BUTTONS.start:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis6sprite);
                        break;
                    case BUTTONS.hold:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis7sprite);
                        break;
                    case BUTTONS.old1:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis8sprite);
                        break;
                    case BUTTONS.old2:
                        b = PaletteHelper.StringToByteStream(Properties.Resources.bis9sprite);
                        break;
                }
                s.loadStream(b);
                s.streamLength = b.Length;
                byte[] colordefaults = PaletteHelper.StringToByteStream(defaults);
                byte[] col_byte = new byte[2];
                for (int i = 0; i < colordefaults.Length; i = i + 2)
                {
                    col_byte[0] = colordefaults[i];
                    col_byte[1] = colordefaults[i + 1];
                    Color defaultcolor = PaletteHelper.ByteToColor(col_byte);
                    ColorOffset cp = new ColorOffset();
                    cp.c = defaultcolor;
                    cp.position = defaultoffset + i;
                    s.defaultColorOffsets.Add(cp);
                }
                s.unusedOffsets = new List<int>() { 66, 67, 92, 93 };
                c.portrait = p;                
            }
            return c;
        }
    }

    public struct ColorOffset
    {
        public Color c;
        public int position;
    }


    public class Palette
    {
        public int streamLength { get; set; } // this doens't belong here
        public List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
        public List<int> unusedOffsets = new List<int>();
        
        private Dictionary<string, Color> labelsToColors = new Dictionary<string, Color>
        {
        };

        public static Dictionary<string, List<int>> labelsToMemOffsets = new Dictionary<string, List<int>>
        {
        };

        public static Palette PaletteFromConfig(PaletteConfig pc)
        {
            Palette p = new Palette();
            p.setAllOffSets(pc.labelOffsets);
            p.defaultColorOffsets = new List<ColorOffset>(pc.defaultColorOffsets);
            p.streamLength = pc.streamLength;
            p.unusedOffsets = new List<int>(pc.unusedOffsets);
            return p;
        }

        public Palette()
        {

        }

        public void loadStream(byte[] b)
        {
            byte[] col_byte = new byte[2];
            foreach (var k in labelsToMemOffsets.Keys)
            {
                int offset = labelsToMemOffsets[k][0];
                col_byte[0] = b[offset];
                col_byte[1] = b[offset+1];
                Color col = PaletteHelper.ByteToColor(col_byte);
                labelsToColors[k] = col;
            }
        }

        public void setAllOffSets(Dictionary<string, List<int>> offsets)
        {
            foreach (var k in offsets.Keys)
            {
                var l = new List<int>(offsets[k]);
                setOffsets(k, l);
            }
        }

        public void setOffsets(string s, List<int> l)
        {
            labelsToMemOffsets[s] = l;
        }

        public List<int> getOffsets(string s)
        {
            return labelsToMemOffsets[s];
        }


        public Color getColor(string s)
        {
            if (labelsToColors.ContainsKey(s))
                return labelsToColors[s];
            else
                return Color.Black;
        }

        public void SetColor(string s, Color c)
        {
            labelsToColors[s] = c;
        }

        
        public byte[] ToByteStream()
        {
            byte[] b = new byte[streamLength];

            foreach (var k in defaultColorOffsets)
            {
                byte[] c = PaletteHelper.ColorToByte(k.c);
                b[k.position] = c[0];
                b[k.position + 1] = c[1];
            }

            foreach (var k in labelsToMemOffsets.Keys)
            {
                Color col = this.getColor(k);
                byte[] c = PaletteHelper.ColorToByte(col);
                foreach (int offset in labelsToMemOffsets[k])
                {
                    b[offset] = c[0];
                    b[offset + 1] = c[1];
                }
            }

            return b;
        }

    }
    
    /*
        public new enum PALETTE_COLORS
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

        public static Dictionary<PALETTE_COLORS, List<int>> colorsToMemOffsets = new Dictionary<PALETTE_COLORS, List<int>>
        {
            { PALETTE_COLORS.pads5, new List<int>() { 0, ROWLEN * 3 + 0, ROWLEN * 4 + 0 } },
            { PALETTE_COLORS.costume5, new List<int>() { 2, ROWLEN * 3 + 2, ROWLEN * 4 + 2 } },
            { PALETTE_COLORS.costume4, new List<int>() { 4, ROWLEN * 3 + 4, ROWLEN * 4 + 4 } },
            { PALETTE_COLORS.costume3, new List<int>() { 6, ROWLEN * 3 + 6, ROWLEN * 4 + 6 } },
            { PALETTE_COLORS.costume2, new List<int>() { 8, ROWLEN * 3 + 8, ROWLEN * 4 + 8 } },
            { PALETTE_COLORS.costume1, new List<int>() { 10, ROWLEN * 3 + 10, ROWLEN * 4 + 10 } },
            { PALETTE_COLORS.pads4, new List<int>() { 12 } },
            { PALETTE_COLORS.stripe, new List<int>() { 14, ROWLEN * 3 + 14 } },
            { PALETTE_COLORS.pads1, new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
            { PALETTE_COLORS.pads2, new List<int>() { 28, ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
            { PALETTE_COLORS.pads3, new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
            { PALETTE_COLORS.skin1, new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22, ROWLEN * 4 + 22 } },
            { PALETTE_COLORS.skin2, new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24, ROWLEN * 4 + 24 } },
            { PALETTE_COLORS.skin3, new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 3 + 26, ROWLEN * 4 + 26 } },
            { PALETTE_COLORS.skin4, new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 3 + 28, ROWLEN * 4 + 28 } },

            { PALETTE_COLORS.crusherpads5, new List<int>() { ROWLEN * 2 + 0 } },
            { PALETTE_COLORS.crushercostume4, new List<int>() { ROWLEN * 2 + 4 } },
            { PALETTE_COLORS.crushercostume3, new List<int>() { ROWLEN * 2 + 6 } },
            { PALETTE_COLORS.crushercostume2, new List<int>() { ROWLEN * 2 + 9 } },
            { PALETTE_COLORS.crushercostume1, new List<int>() { ROWLEN * 2 + 10 } },
            { PALETTE_COLORS.crusherpads4, new List<int>() { ROWLEN * 2 + 12 } },
            { PALETTE_COLORS.crusherflame1, new List<int>() { ROWLEN * 2 + 14 } },
            { PALETTE_COLORS.crusherpads1, new List<int>() { ROWLEN * 2 + 16 } },
            { PALETTE_COLORS.crusherpads2, new List<int>() { ROWLEN * 2 + 18 } },
            { PALETTE_COLORS.crusherpads3, new List<int>() { ROWLEN * 2 + 20 } },
            { PALETTE_COLORS.crusherflame2, new List<int>() { ROWLEN * 2 + 22 } },
            { PALETTE_COLORS.crusherhands1, new List<int>() { ROWLEN * 2 + 24 } },
            { PALETTE_COLORS.crusherhands2, new List<int>() { ROWLEN * 2 + 26 } },
            { PALETTE_COLORS.psychoglow, new List<int>() { ROWLEN * 3 + 12 } },
            { PALETTE_COLORS.psychopunch1, new List<int>() { ROWLEN * 4 + 12 } },
            { PALETTE_COLORS.psychopunch2, new List<int>() { ROWLEN * 4 + 14 } },
            { PALETTE_COLORS.psychopunch3, new List<int>() { ROWLEN * 4 + 16 } },
            { PALETTE_COLORS.psychopunch4, new List<int>() { ROWLEN * 4 + 18 } },
            { PALETTE_COLORS.psychopunch5, new List<int>() { ROWLEN * 4 + 20 } },
        };
    }*/
}
