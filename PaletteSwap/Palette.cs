using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public abstract class Palette
    {
        public enum PALETTE_COLORS { foo, bar };
        public static int ROWLEN = 32;

        public string printFirstColor<TEnum>() where TEnum : struct
            {
            if (!typeof(TEnum).IsEnum)
            {
                return "not an enum";
            }
            else
            {
                foreach (var label in Enum.GetNames(typeof(TEnum)))
                {
                    return label;
                }
            }
            return "";

        }
    }

    public class DictatorSprite : Palette
    {
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
    }

    public class DictatorPortrait : Palette
    {
        public new enum PALETTE_COLORS { brown, cyan, pink }
    }
}
