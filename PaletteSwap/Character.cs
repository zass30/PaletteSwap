using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public class Character
    {
        public static int ROWLEN = 32;
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
        public Palette sprite;
        public Palette portrait;
        public enum CHARACTERS { Dictator, Claw };
        public enum BUTTONS { lp, mp, hp, lk, mk, hk, start, hold, old1, old2 };

        public static Character createDefaultCharacter(CHARACTERS characater, BUTTONS button)
        {
            var c = new Character();
            if (characater == CHARACTERS.Dictator)
            {
                PaletteConfig pc = PaletteConfig.GenerateDictatorSpriteConfig();
                Palette s = Palette.PaletteFromConfig(pc);
                Palette p = new Palette();
                c.sprite = s;
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
                c.portrait = p;
            }
            return c;
        }
    }
}
