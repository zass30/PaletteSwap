using PaletteSwap.Properties;
using System;
using System.IO.Compression;
using System.IO;

namespace PaletteSwap
{
    public struct CharacterColor
    {
        public Sprite s;
        public Portrait p;
    }

    public class CharacterSet
    {
        public Character[] characterColors = new Character[10];
        public int sprite_offset;
        public int sprite_length;
        public int portrait_offset;
        public int portrait2_offset;
        public int portrait_length;

        public static CharacterSet GenerateDictatorCharacterSet()
        {
            CharacterSet cs = new CharacterSet();
            cs.sprite_offset = 0x00042E7E;
            cs.sprite_length = 0xA2;
            cs.portrait_offset = 0x34448;
            cs.portrait2_offset = 0x394FE;
            cs.portrait_length = 0x80;
            return cs;
        }

        public byte[] portraits_stream03()
        {
            byte[] b = Resources.sfxe03c;
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i] == null)
                    continue;
                if (characterColors[i].portrait == null)
                    continue;
                var p = characterColors[i].portrait;
                byte[] color_bytes = p.ToByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[portrait_offset + i * portrait_length + j] = color_bytes[j];
                }
            }
            return b;
        }

        public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe04a;
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i] == null)
                    continue;
                if (characterColors[i].sprite == null)
                    continue;
                var s = characterColors[i].sprite;
                byte[] color_bytes = s.ToByteStream();
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[sprite_offset + i * sprite_length + j] = color_bytes[j];
                }
            }
            return b;
        }
    }

    public class CharacterColorSet
    {
        public CharacterColor[] characterColors;

        public static int sprite_offset = 0x00042E7C;
        public static int sprite_length = 0xA2;
        public static int portrait_offset = 0x34448;
        public static int portrait_length = 0x80;
        public static int portrait2_offset = 0x394FE;
        public static int[] punch_glitch = new int[] { 0x6BD1A, 0x6BD1E, 0x6BFB6, 0x6C024,
        0x6C038, 0x6C03C, 0x6C04C, 0x6C050};
        public static byte punch_glitch_replacement_value = 0x14;

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


        public static CharacterColorSet CharacterColorSetFromZipStream(Stream fileStream)
        {
            byte[] sprites = new byte[sprite_length];
            byte[] portraits = new byte[portrait_length];

            var zip = new ZipArchive(fileStream, ZipArchiveMode.Read);
            foreach (var entry in zip.Entries)
            {
                using (var stream = entry.Open())
                {
                    if (entry.Name == "sfxe.03c" ||
                        entry.Name == "sfxjd.03c")
                    {
                        using (var memorySubStream = new MemoryStream())
                        {
                            stream.CopyTo(memorySubStream);
                            portraits = memorySubStream.ToArray();
                        }
                    }

                    if (entry.Name == "sfxe.04a" ||
                        entry.Name == "sfxjd.04a")
                    {
                        using (var memorySubStream = new MemoryStream())
                        {
                            stream.CopyTo(memorySubStream);
                            sprites = memorySubStream.ToArray();
                        }
                    }
                }
            }

            return CharacterColorSetFromStreams(sprites, portraits);
        }

        public ZipArchive ZipArchive()
        {
            var memoryStream = new MemoryStream();
            byte[] p_stream = portraits_stream03();
            byte[] s_stream = sprites_stream04();
            var archive = new ZipArchive(memoryStream, ZipArchiveMode.Update, true);
            var _03file = archive.CreateEntry("sfxe.03c");
            using (var entryStream = _03file.Open())
            using (var streamWriter = new StreamWriter(entryStream))
            {
                var c = entryStream.CanSeek;
                entryStream.Write(p_stream, 0, p_stream.Length);
            }

            var _04file = archive.CreateEntry("sfxe.04a");
            using (var entryStream = _04file.Open())
            using (var streamWriter = new StreamWriter(entryStream))
            {
                var c = entryStream.CanSeek;
                entryStream.Write(s_stream, 0, s_stream.Length);
            }
            return archive;
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
                    b[portrait2_offset + i * portrait_length + j] = color_bytes[j];
                }

            }
            return b;
        }

        private byte[] old_bison_punches_stream(byte[] b)
        {
            foreach (int offset in punch_glitch)
            {
                b[offset] = punch_glitch_replacement_value;
            }
            return b;
        }

        public byte[] old_bison_punches_stream06()
        {
            return old_bison_punches_stream(Resources.sfxe06a);
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

        public byte[] old_bison_punches_stream06phoenix()
        {
            return old_bison_punches_stream(Resources.sfxjd06a);
        }

        public byte[] portraits_stream03japanese()
        {
            return portraits_stream(Resources.sfxj03c);
        }

        public byte[] sprites_stream04japanese()
        {
            return sprites_stream(Resources.sfxj04a);
        }

        public byte[] old_bison_punches_stream06japanese()
        {
            return old_bison_punches_stream(Resources.sfxj06a);
        }
    }
}
