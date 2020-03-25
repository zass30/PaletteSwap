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
        public static Dictionary<PALETTE_COLORS, List<int>> colorsToMemOffsets = new Dictionary<PALETTE_COLORS, List<int>>
        {       
            { PALETTE_COLORS.foo, new List<int>() { 1 } },
        };

        public string first_enum()
        {
            foreach (var label in Enum.GetNames(typeof(PALETTE_COLORS)))
            {
                return label;
            }
            return "";
        }
    }

    public class SpriteTestClass : Palette
    {
        public new enum PALETTE_COLORS { sp1, sp2 }
    }

    public class PortraitTestClass : Palette
    {
        public new enum PALETTE_COLORS { po1, po2 }
    }
}
