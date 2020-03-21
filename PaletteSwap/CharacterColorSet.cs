using PaletteSwap.Properties;

namespace PaletteSwap
{
    public struct CharacterColor
    {
        public Sprite s;
        public Portrait p;
    }

    public class CharacterColorSet
    {
        public CharacterColor[] characterColors;

        public static int offset = 0x00042E7C;
        public static int sprite_length = 0xa2;

        public CharacterColorSet()
        {
            characterColors = new CharacterColor[10];
        }

        public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe1;
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i].s == null)
                    continue;
                var s = characterColors[i].s;
                byte[] color_bytes = s.ByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[offset + i * sprite_length + j] = color_bytes[j];
                }

            }
            return b;
        }
    }
}
