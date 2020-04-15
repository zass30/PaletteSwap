using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public partial class Character
    {
        public Palette sprite;
        public Palette portrait;
        public CharacterConfig.CHARACTERS characterType;

        public static string CodeFromCharacterEnum(CharacterConfig.CHARACTERS c)
        {
            switch (c) {
                case CharacterConfig.CHARACTERS.Dictator:
                    return "DIC";
                case CharacterConfig.CHARACTERS.Claw:
                    return "CLA";
                case CharacterConfig.CHARACTERS.Guile:
                    return "GUI";
                case CharacterConfig.CHARACTERS.Ryu:
                    return "RYU";
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
                case "RYU":
                    return CharacterConfig.CHARACTERS.Ryu;
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
            Character character = CreateDefaultCharacter(characterType, CharacterConfig.BUTTONS.lp);
            var sprite = sr.ReadLine(); // SPRITE
            var rest = sr.ReadToEnd();
            var v = rest.Split(new string[] { "PORTRAIT" + System.Environment.NewLine }, StringSplitOptions.None);
            var sprites = v[0];
            var portraits = v[1];
            character.sprite.LoadColFormat(sprites);
            character.portrait.LoadColFormat(portraits);

            return character;
        }

        public static Character CreateDefaultCharacter(CharacterConfig.CHARACTERS character, CharacterConfig.BUTTONS button)
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
            AssignImage(p, ImageConfig.GenerateVictoryBasePalette(character), "victory");
            AssignImage(p, ImageConfig.GenerateLossBasePalette(character), "loss");

            if (character == CharacterConfig.CHARACTERS.Dictator)
                AssignDicatatorSpriteImages(s);
            
            return c;
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
