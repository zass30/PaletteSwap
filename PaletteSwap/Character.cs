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
                PaletteConfig dicSpriteConfig = PaletteConfig.GenerateDictatorSpriteConfig();
                PaletteConfig dicPortraitConfig = PaletteConfig.GenerateDictatorPortraitConfig();
                Palette s = Palette.PaletteFromConfig(dicSpriteConfig);
                Palette p = Palette.PaletteFromConfig(dicPortraitConfig);
                c.sprite = s;
                c.portrait = p;
                byte[] sprite_bytestream = new byte[0];
                byte[] portrait_bytestream = new byte[0];
                switch (button)
                {
                    case BUTTONS.lp:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis0sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis0portrait);
                        break;
                    case BUTTONS.mp:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis1sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis1portrait);
                        break;
                    case BUTTONS.hp:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis2sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis2portrait);
                        break;
                    case BUTTONS.lk:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis3sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis3portrait);
                        break;
                    case BUTTONS.mk:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis4sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis4portrait);
                        break;
                    case BUTTONS.hk:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis5sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis5portrait);
                        break;
                    case BUTTONS.start:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis6sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis6portrait);
                        break;
                    case BUTTONS.hold:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis7sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis7portrait);
                        break;
                    case BUTTONS.old1:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis8sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis8portrait);
                        break;
                    case BUTTONS.old2:
                        sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis9sprite);
                        portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis9portrait);
                        break;
                }
                s.loadStream(sprite_bytestream);
                AssignDicatatorSpriteImages(s);

                p.loadStream(portrait_bytestream);
            }
            return c;
        }        

        private static void AssignDicatatorSpriteImages(Palette s)
        {
            // neutral sprite image
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorStandingNeutralBasePaletteImage(), "neutral");

            // psychopunch image
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPunchBasePaletteImage(), "psychopunch");

            // psychoprep image
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPrepBasePaletteImage(), "psychoprep");

            // crushertop image
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherTopBasePaletteImage(), "crushertop");

            // crusherbottom image
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherBottomBasePaletteImage(), "crusherbottom");

        }

        private static void AssignImage(Palette sprite, PaletteImage p, string label)
        {
            p.palette = sprite;
            sprite.SetImage(label, p);
        }
    }
}
