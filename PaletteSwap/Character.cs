using System;
using System.Collections.Generic;
using System.Drawing;
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

        public string ToColFormat()
        {
            StringBuilder s = new StringBuilder();
            s.Append(CharacterConfig.CodeFromCharacterEnum(characterType));
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
            var characterType = CharacterConfig.CharacterEnumFromCode(first);
            Character character = CreateDefaultCharacter(characterType, CharacterConfig.BUTTONS.lp);
            var sprite = sr.ReadLine(); // SPRITE
            var rest = sr.ReadToEnd();
            var v = rest.Split(new string[] { "PORTRAIT" + System.Environment.NewLine, "PORTRAIT" + "\r", "PORTRAIT" + "\n", "PORTRAIT" + "\r\n" }, StringSplitOptions.None);
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
            if (character != CharacterConfig.CHARACTERS.Gouki)
            AssignImage(p, ImageConfig.GenerateLossBasePalette(character), "loss");

            if (character == CharacterConfig.CHARACTERS.Dictator)
                AssignDicatatorSpriteImages(s);
            if (character == CharacterConfig.CHARACTERS.Gouki)
                AssignGoukiSpriteImages(s);

            return c;
        }

        private static void AssignDicatatorSpriteImages(Palette s)
        {
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPunchBasePaletteImage(), "psychopunch");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorPsychoPrepBasePaletteImage(), "psychoprep");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherTopBasePaletteImage(), "crushertop");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherBottomBasePaletteImage(), "crusherbottom");
            AssignImage(s, ImageConfig.Dictator.SPRITE.GenerateDictatorCrusherBackBasePaletteImage(), "crusherback");
        }

        private static void AssignGoukiSpriteImages(Palette s)
        {
            AssignImage(s, ImageConfig.Gouki.SPRITE.GenerateGoukiTeleport1BasePaletteImage(), "teleport1");
            AssignImage(s, ImageConfig.Gouki.SPRITE.GenerateGoukiTeleport2BasePaletteImage(), "teleport2");
            AssignImage(s, ImageConfig.Gouki.SPRITE.GenerateGoukiTeleport3BasePaletteImage(), "teleport3");
        }

        public Bitmap GetBitmap(string s)
        {
            switch (s)
            {
                case "neutral":
                    return sprite.GetBitmap("neutral");
                case "victory":
                    return portrait.GetBitmap("victory");
                case "loss":
                    return portrait.GetBitmap("loss");
                case "psychopunch":
                    return sprite.GetBitmap("psychopunch");
            }
            throw new Exception("invalid bitmap");
        }

        public Bitmap GetPreviewBitmap()
        {
            Bitmap n;
            if (this.characterType != CharacterConfig.CHARACTERS.Dictator)
                n = this.GetBitmap("neutral");
            else
                n = this.GetBitmap("psychopunch");
            var v = this.GetBitmap("victory");
            Bitmap l;
            if (this.characterType != CharacterConfig.CHARACTERS.Gouki)
                l = this.GetBitmap("loss");
            else
                l = new Bitmap(1, 1);
            int buffer = 10;
            var w = n.Width + buffer + v.Width + buffer + l.Width;
            var h = Math.Max(n.Height, Math.Max(v.Height, l.Height));
            Bitmap b = new Bitmap(w, h);
            Graphics gfb = Graphics.FromImage(b);
            gfb.DrawImage(n, new Point(0, h - n.Height));
            gfb.DrawImage(v, new Point(n.Width + buffer, 0));
            gfb.DrawImage(l, new Point(n.Width + buffer + v.Width + buffer, 0));
            return b;
        }

        private static void AssignImage(Palette palette, PaletteImage paletteImage, string label)
        {
            paletteImage.palette = palette;
            palette.SetImage(label, paletteImage);
        }
    } 
}
