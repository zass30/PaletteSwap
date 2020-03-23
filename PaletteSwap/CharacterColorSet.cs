using PaletteSwap.Properties;
using System;

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
            var cs = new CharacterColorSet();
            byte[] sprite_bytes = new byte[sprite_length];
            byte[] portrait_bytes = new byte[portrait_length];
            for (int i = 0; i < 10; i++)
            {
                Array.Copy(sprites, sprite_offset + i* sprite_length, sprite_bytes, 0, sprite_length);
                Array.Copy(portraits, portrait_offset + i * portrait_length, portrait_bytes, 0, portrait_length);
                Portrait p = Portrait.LoadFromStream(portrait_bytes);
                Sprite s = Sprite.LoadFromStream(sprite_bytes);
                cs.characterColors[i].p = p;
                cs.characterColors[i].s = s;
            }
            return cs;
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
