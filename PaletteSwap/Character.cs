using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public class Character
    {
        public Palette sprite;
        public Palette portrait;
  //      public enum CHARACTERS { Dictator, Claw, Guile };
        public CharacterConfig.CHARACTERS characterType;
        //public enum BUTTONS { lp, mp, hp, lk, mk, hk, start, hold, old1, old2 };

        public static string CodeFromCharacterEnum(CharacterConfig.CHARACTERS c)
        {
            switch (c) {
                case CharacterConfig.CHARACTERS.Dictator:
                    return "DIC";
                case CharacterConfig.CHARACTERS.Claw:
                    return "CLA";
                case CharacterConfig.CHARACTERS.Guile:
                    return "GUI";                    
            }
            throw new ArgumentException("Invalid Character type");
        }

        public static CharacterConfig.CHARACTERS CharacterEnumFromCode(string s)
        {
            switch (s)
            {
                case "CLA":
                    return CharacterConfig.CHARACTERS.Claw;
                case "DIC":
                    return CharacterConfig.CHARACTERS.Dictator;
                case "GUI":
                    return CharacterConfig.CHARACTERS.Guile;
            }
            throw new ArgumentException("Invalid Character type");
        }

        public string ToColFormat()
        {
            StringBuilder s = new StringBuilder();
            s.Append(CodeFromCharacterEnum(characterType));
            s.Append(System.Environment.NewLine);
            s.Append("SPRITE" + System.Environment.NewLine);
            s.Append(this.sprite.ToColFormat() + System.Environment.NewLine);
            s.Append("PORTRAIT" + System.Environment.NewLine);
            s.Append(this.portrait.ToColFormat());
            return s.ToString();
        }

        public static Character CharacterFromColFormat(string s)
        {
            var sr = new StringReader(s);
            string first = sr.ReadLine();
            var characterType = CharacterEnumFromCode(first);
            Character character = createDefaultCharacter(characterType, CharacterConfig.BUTTONS.lp);
            var sprite = sr.ReadLine(); // SPRITE
            var rest = sr.ReadToEnd();
            var v = rest.Split(new string[] { "PORTRAIT" + System.Environment.NewLine }, StringSplitOptions.None);
            var sprites = v[0];
            var portraits = v[1];
            character.sprite.LoadColFormat(sprites);
            character.portrait.LoadColFormat(portraits);

            return character;
        }

        // legacy function to delete eventually after old cols are convereted
        public void loadFromColFormat(string colstr)
        {
            var v = colstr.Split(':');
            loadSpriteColFormat(v[0]);
            loadPortraitColFormat(v[1]);
        }

        private void loadPortraitColFormat(string s)
        {
            var p = this.portrait;
            var v = s.Split('\n');
            p.SetColor("skin1", PaletteHelper.RGBFormatToColor(v[0]));
            p.SetColor("skin2", PaletteHelper.RGBFormatToColor(v[1]));
            p.SetColor("skin3", PaletteHelper.RGBFormatToColor(v[2]));
            p.SetColor("skin4", PaletteHelper.RGBFormatToColor(v[3]));
            p.SetColor("skin5", PaletteHelper.RGBFormatToColor(v[4]));
            p.SetColor("skin6", PaletteHelper.RGBFormatToColor(v[5]));
            p.SetColor("skin7", PaletteHelper.RGBFormatToColor(v[6]));
            p.SetColor("costume1", PaletteHelper.RGBFormatToColor(v[7]));
            p.SetColor("costume2", PaletteHelper.RGBFormatToColor(v[8]));
            p.SetColor("costume3", PaletteHelper.RGBFormatToColor(v[9]));
            p.SetColor("costume4", PaletteHelper.RGBFormatToColor(v[10]));
            p.SetColor("teeth1", PaletteHelper.RGBFormatToColor(v[11]));
            p.SetColor("teeth2", PaletteHelper.RGBFormatToColor(v[12]));
            p.SetColor("teeth3", PaletteHelper.RGBFormatToColor(v[13]));
            p.SetColor("teeth4", PaletteHelper.RGBFormatToColor(v[14]));
            p.SetColor("piping1", PaletteHelper.RGBFormatToColor(v[15]));
            p.SetColor("piping2", PaletteHelper.RGBFormatToColor(v[16]));
            p.SetColor("piping3", PaletteHelper.RGBFormatToColor(v[17]));
            p.SetColor("piping4", PaletteHelper.RGBFormatToColor(v[18]));
            p.SetColor("pipingloss1", PaletteHelper.RGBFormatToColor(v[19]));
            p.SetColor("pipingloss2", PaletteHelper.RGBFormatToColor(v[20]));
            p.SetColor("pipingloss3", PaletteHelper.RGBFormatToColor(v[21]));
            p.SetColor("pipingloss4", PaletteHelper.RGBFormatToColor(v[22]));
            p.SetColor("costumeloss1", PaletteHelper.RGBFormatToColor(v[23]));
            p.SetColor("costumeloss2", PaletteHelper.RGBFormatToColor(v[24]));
            p.SetColor("costumeloss3", PaletteHelper.RGBFormatToColor(v[25]));
            p.SetColor("costumeloss4", PaletteHelper.RGBFormatToColor(v[26]));
            p.SetColor("blood1", PaletteHelper.RGBFormatToColor(v[27]));
            p.SetColor("blood2", PaletteHelper.RGBFormatToColor(v[28]));
            p.SetColor("blood3", PaletteHelper.RGBFormatToColor(v[29]));
        }

        private void loadSpriteColFormat(string s)
        {
            var sp = this.sprite;
            var v = s.Split('\n');
            sp.SetColor("skin1", PaletteHelper.RGBFormatToColor(v[0]));
            sp.SetColor("skin2", PaletteHelper.RGBFormatToColor(v[1]));
            sp.SetColor("skin3", PaletteHelper.RGBFormatToColor(v[2]));
            sp.SetColor("skin4", PaletteHelper.RGBFormatToColor(v[3]));
            sp.SetColor("costume1", PaletteHelper.RGBFormatToColor(v[4]));
            sp.SetColor("costume2", PaletteHelper.RGBFormatToColor(v[5]));
            sp.SetColor("costume3", PaletteHelper.RGBFormatToColor(v[6]));
            sp.SetColor("costume4", PaletteHelper.RGBFormatToColor(v[7]));
            sp.SetColor("costume5", PaletteHelper.RGBFormatToColor(v[8]));
            sp.SetColor("pads1", PaletteHelper.RGBFormatToColor(v[9]));
            sp.SetColor("pads2", PaletteHelper.RGBFormatToColor(v[10]));
            sp.SetColor("pads3", PaletteHelper.RGBFormatToColor(v[11]));
            sp.SetColor("pads4", PaletteHelper.RGBFormatToColor(v[12]));
            sp.SetColor("pads5", PaletteHelper.RGBFormatToColor(v[13]));
            sp.SetColor("stripe", PaletteHelper.RGBFormatToColor(v[14]));
            sp.SetColor("psychoglow", PaletteHelper.RGBFormatToColor(v[15]));
            sp.SetColor("psychopunch1", PaletteHelper.RGBFormatToColor(v[16]));
            sp.SetColor("psychopunch2", PaletteHelper.RGBFormatToColor(v[17]));
            sp.SetColor("psychopunch3", PaletteHelper.RGBFormatToColor(v[18]));
            sp.SetColor("psychopunch4", PaletteHelper.RGBFormatToColor(v[19]));
            sp.SetColor("psychopunch5", PaletteHelper.RGBFormatToColor(v[20]));
            sp.SetColor("crushercostume1", PaletteHelper.RGBFormatToColor(v[21]));
            sp.SetColor("crushercostume2", PaletteHelper.RGBFormatToColor(v[22]));
            sp.SetColor("crushercostume3", PaletteHelper.RGBFormatToColor(v[23]));
            sp.SetColor("crushercostume4", PaletteHelper.RGBFormatToColor(v[24]));
            sp.SetColor("crusherpads1", PaletteHelper.RGBFormatToColor(v[25]));
            sp.SetColor("crusherpads2", PaletteHelper.RGBFormatToColor(v[26]));
            sp.SetColor("crusherpads3", PaletteHelper.RGBFormatToColor(v[27]));
            sp.SetColor("crusherpads4", PaletteHelper.RGBFormatToColor(v[28]));
            sp.SetColor("crusherpads5", PaletteHelper.RGBFormatToColor(v[29]));
            sp.SetColor("crusherhands1", PaletteHelper.RGBFormatToColor(v[30]));
            sp.SetColor("crusherhands2", PaletteHelper.RGBFormatToColor(v[31]));
            sp.SetColor("crusherflame1", PaletteHelper.RGBFormatToColor(v[32]));
            sp.SetColor("crusherflame2", PaletteHelper.RGBFormatToColor(v[33]));

        }
        // end legacy function block

        public static Character createDefaultCharacter(CharacterConfig.CHARACTERS character, CharacterConfig.BUTTONS button)
        {
            var c = new Character();
            c.characterType = character;
            ByteStreamPair bsp = CharacterConfig.GetByteStreamPair(character, button);
            PaletteConfig spriteConfig = PaletteConfig.GenerateSpriteConfig(character);
            PaletteConfig portraitConfig = PaletteConfig.GeneratePortraitConfig(character);
            Palette s = Palette.PaletteFromConfig(spriteConfig);
            Palette p = Palette.PaletteFromConfig(portraitConfig);
            c.sprite = s;
            c.portrait = p;

            s.LoadStream(bsp.spriteStream);
            p.LoadStream(bsp.portraitStream);
            AssignImage(s, ImageConfig.GenerateNeutralBasePalette(character), "neutral");

            if (character == CharacterConfig.CHARACTERS.Dictator)
            {
                AssignDicatatorSpriteImages(s);
                AssignDicatatorPortraitImages(p);
            }
            else if (character == CharacterConfig.CHARACTERS.Claw) {
                AssignImage(p, ImageConfig.CLAW.PORTRAIT.GenerateClawVictoryBasePaletteImage(), "victory");
                AssignImage(p, ImageConfig.CLAW.PORTRAIT.GenerateClawLossBasePaletteImage(), "loss");
            }
            else if (character == CharacterConfig.CHARACTERS.Guile)
            {
                AssignImage(p, ImageConfig.GUILE.PORTRAIT.GenerateGuileVictoryBasePaletteImage(), "victory");
                AssignImage(p, ImageConfig.GUILE.PORTRAIT.GenerateGuileLossBasePaletteImage(), "loss");
            }
            return c;
        }

        private static void AssignDicatatorPortraitImages(Palette p)
        {
            AssignImage(p, ImageConfig.Dictator.PORTRAIT.GenerateDictatorVictoryBasePaletteImage(), "victory");
            AssignImage(p, ImageConfig.Dictator.PORTRAIT.GenerateDictatorLossBasePaletteImage(), "loss");
        }

        private static void AssignDicatatorSpriteImages(Palette s)
        {
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPunchBasePaletteImage(), "psychopunch");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPrepBasePaletteImage(), "psychoprep");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherTopBasePaletteImage(), "crushertop");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherBottomBasePaletteImage(), "crusherbottom");
        }

        private static void AssignImage(Palette palette, PaletteImage paletteImage, string label)
        {
            paletteImage.palette = palette;
            palette.SetImage(label, paletteImage);
        }
    } 
}
