using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public class Character
    {
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
