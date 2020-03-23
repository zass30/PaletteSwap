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

        public static int sprite_offset = 0x00042E7C;
        public static int sprite_length = 0xA2;
        public static int portrait_offset = 0x34448;
        public static int portrait_length = 0x80;

        public CharacterColorSet()
        {
            characterColors = new CharacterColor[10];
        }

        public static CharacterColorSet CharacterColorSetFromStreams(byte[] sprites, byte[] portraits)
        {
            return new CharacterColorSet();
        }

        private byte[] sprites_stream(byte[] b)
        {
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i].s == null)
                    continue;
                var s = characterColors[i].s;
                byte[] color_bytes = s.ByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[sprite_offset + i * sprite_length + j] = color_bytes[j];
                }

            }
            return b;
        }

        private byte[] portraits_stream(byte[] b)
        {
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i].p == null)
                    continue;
                var p = characterColors[i].p;
                byte[] color_bytes = p.ByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[portrait_offset + i * portrait_length + j] = color_bytes[j];
                }

            }
            return b;
        }

        public byte[] portraits_stream03()
        {
            return portraits_stream(Resources.sfxe03c);
        }

        public byte[] sprites_stream04()
        {
            return sprites_stream(Resources.sfxe04a);
        }

        public byte[] portraits_stream03phoenix()
        {
            return portraits_stream(Resources.sfxjd03c);
        }

        public byte[] sprites_stream04phoenix()
        {
            return sprites_stream(Resources.sfxjd04a);          
        }
    }
}
