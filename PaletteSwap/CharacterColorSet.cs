using PaletteSwap.Properties;
using System;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace PaletteSwap
{
    public class CharacterSet
    {
        public Character[] characterColors = new Character[10];
        public int sprite_offset;
        public int sprite_length;
        public int portrait_offset;
        public int portrait2_offset;
        public int portrait_length;
        public CharacterConfig.CHARACTERS character;

        public CharacterSet(CharacterConfig.CHARACTERS character)
        {
            this.character = character;
            this.sprite_offset = CharacterConfig.GetSpriteBeginOffset(character);
            this.sprite_length = CharacterConfig.spriteColorLength;
            if (character == CharacterConfig.CHARACTERS.Blanka)
                this.sprite_length = this.sprite_length - 0x02;
            this.portrait_offset = CharacterConfig.GetPortrait1BeginOffset(character);
            this.portrait2_offset = CharacterConfig.GetPortrait2BeginOffset(character);
            this.portrait_length = CharacterConfig.portraitColorLength;

            characterColors[0] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.lp);
            characterColors[1] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.mp);
            characterColors[2] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.hp);
            characterColors[3] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.lk);
            characterColors[4] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.mk);
            characterColors[5] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.hk);
            characterColors[6] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.start);
            characterColors[7] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.hold);
            characterColors[8] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.old1);
            characterColors[9] = Character.CreateDefaultCharacter(character, CharacterConfig.BUTTONS.old2);
        }

        public CharacterSet()
        {

        }

        public Bitmap GenerateSpriteKey()
        {
            if (character == CharacterConfig.CHARACTERS.Gouki)
            {
                int buffer = 10;
                var b0 = characterColors[0].GetBitmap("neutral");
                var b1 = characterColors[1].GetBitmap("neutral");
                Bitmap b = new Bitmap(b0.Width * 2 + buffer, b0.Height);
                Graphics gfb = Graphics.FromImage(b);
                gfb.DrawImage(b0, new Point(0, 0));
                gfb.DrawImage(b1, new Point(b0.Width + buffer, 0));
                return b;
            }
            else
                return GenerateKey("neutral");
        }

        public Bitmap GenerateKey(String s)
        {
            int buffer = 10;
            var sample = characterColors[0].GetBitmap(s);
            Bitmap b = new Bitmap(sample.Width * 5 + 4 * buffer, sample.Height * 2 + buffer);
            Graphics gfb = Graphics.FromImage(b);
            for (int i = 0; i < 3; i++)
            {
                gfb.DrawImage(characterColors[i].GetBitmap(s), new Point(sample.Width * i + i * buffer, 0));
                gfb.DrawImage(characterColors[i + 3].GetBitmap(s), new Point(sample.Width * i + i * buffer, sample.Height + buffer));
            }

            gfb.DrawImage(characterColors[6].GetBitmap(s), new Point(sample.Width * 3 + 3 * buffer, 0));
            gfb.DrawImage(characterColors[7].GetBitmap(s), new Point(sample.Width * 3 + 3 * buffer, sample.Height + buffer));
            gfb.DrawImage(characterColors[8].GetBitmap(s), new Point(sample.Width * 4 + 4 * buffer, 0));
            gfb.DrawImage(characterColors[9].GetBitmap(s), new Point(sample.Width * 4 + 4 * buffer, sample.Height + buffer));

            return b;
        }

        public Bitmap GenerateColorSetKey()
        {
            Bitmap portraits = GeneratePortraitKey();
            Bitmap sprites;
            if (character == CharacterConfig.CHARACTERS.Dictator)
                sprites = this.GenerateKey("psychopunch");
            else 
                sprites = GenerateSpriteKey();
            int buffer = 10;
            int w = Math.Max(portraits.Width, sprites.Width);
            int h = portraits.Height + sprites.Height + buffer;
            Bitmap b = new Bitmap(w, h);
            Graphics gfb = Graphics.FromImage(b);
            gfb.DrawImage(portraits, new Point(0, 0));
            gfb.DrawImage(sprites, new Point(0, portraits.Height + buffer));
            return b;
        }

        public Bitmap GenerateColorSetKeyAligned()
        {
            Bitmap portraits = GeneratePortraitKey();
            Bitmap sprites;
            if (character == CharacterConfig.CHARACTERS.Dictator)
                sprites = this.GenerateKey("psychopunch");
            else
                sprites = GenerateSpriteKey();
            int buffer = 10;
            int w = portraits.Width + buffer + sprites.Width;
            int h = Math.Max(portraits.Height, sprites.Height);
            Bitmap b = new Bitmap(w, h);
            Graphics gfb = Graphics.FromImage(b);
            gfb.DrawImage(portraits, new Point(0, 0));
            gfb.DrawImage(sprites, new Point(portraits.Width + buffer));
            return b;
        }

        public Bitmap GeneratePortraitKey()
        {
            if (character == CharacterConfig.CHARACTERS.Gouki)
                return characterColors[0].GetBitmap("victory");
            else
                return GenerateKey("victory");
        }

        public Bitmap GenerateLossKey()
        {
            return GenerateKey("loss");
        }

        public static CharacterSet GenerateDictatorCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Dictator);
        }

        public static CharacterSet GenerateGuileCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Guile);
        }
    
        public static CharacterSet GenerateClawCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Claw);
        }

        public static CharacterSet CharacterColorSetFromStreamsChar(byte[] sprites, byte[] portraits, CharacterConfig.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet(characterType);

            byte[] sprite_bytes = new byte[cs.sprite_length];
            byte[] portrait_bytes = new byte[cs.portrait_length];
            int blanka_offset = 0;
            for (int i = 0; i < 10; i++)
            {
                if (characterType == CharacterConfig.CHARACTERS.Blanka && i == 9)
                    blanka_offset = 0x02;
                Array.Copy(sprites, cs.sprite_offset + i * cs.sprite_length + blanka_offset, sprite_bytes, 0, cs.sprite_length);
                Array.Copy(portraits, cs.portrait_offset + i * cs.portrait_length, portrait_bytes, 0, cs.portrait_length);
                cs.characterColors[i].sprite.LoadStream(sprite_bytes);
                cs.characterColors[i].portrait.LoadStream(portrait_bytes);
            }
            return cs;
        }

        public static CharacterSet CharacterColorSetFromZipStreamChar(Stream fileStream, CharacterConfig.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet(characterType);

            byte[] sprites = new byte[cs.sprite_length];
            byte[] portraits = new byte[cs.portrait_length];

            var zip = new ZipArchive(fileStream, ZipArchiveMode.Read);
            foreach (var entry in zip.Entries)
            {
                using (var stream = entry.Open())
                {
                    if (entry.Name == "sfxe.03c" ||
                        entry.Name == "sfxjd.03c" ||
                        entry.Name == "sfxj.03c"
                        )
                    {
                        using (var memorySubStream = new MemoryStream())
                        {
                            stream.CopyTo(memorySubStream);
                            portraits = memorySubStream.ToArray();
                        }
                    }

                    if (entry.Name == "sfxe.04a" ||
                        entry.Name == "sfxjd.04a" ||
                        entry.Name == "sfxj.04a"
                        )
                    {
                        using (var memorySubStream = new MemoryStream())
                        {
                            stream.CopyTo(memorySubStream);
                            sprites = memorySubStream.ToArray();
                        }
                    }
                }
            }

            return CharacterColorSetFromStreamsChar(sprites, portraits, characterType);
        }

        public static CharacterSet CharacterColorSetFromZipColorSetChar(Stream fileStream, CharacterConfig.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet(characterType);

            var zip = new ZipArchive(fileStream, ZipArchiveMode.Read);
            foreach (var entry in zip.Entries)
            {
                using (var stream = entry.Open())
                {
                    for (int i = 0; i < 10; i++) {
                        var charCode = CharacterConfig.CodeFromCharacterEnum(characterType);
                        var fileName = charCode + @"/" + "0" + i.ToString() + ".col";
                        if (entry.FullName == fileName)
                        {
                            using (var textSubStream = new StreamReader(stream))
                            {
                                var colText = textSubStream.ReadToEnd();
                                cs.characterColors[i] = Character.CharacterFromColFormat(colText);
                            }
                        }
                    }
                }
            }
            return cs;
        }

        public byte[] patch_portraits_stream03(byte[] b)
        {
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i] == null)
                    continue;
                if (characterColors[i].portrait == null)
                    continue;
                var p = characterColors[i].portrait;
                byte[] color_bytes = p.ToByteStream();
                if (this.character == CharacterConfig.CHARACTERS.Gouki && i > 0)
                    continue;
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[portrait_offset + i * portrait_length + j] = color_bytes[j];
                    b[portrait2_offset + i * portrait_length + j] = color_bytes[j];
                }
            }
            return b;
        }

        public byte[] patch_sprites_stream04(byte[] b)
        {
            var blanka_offset = 0;
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i] == null)
                    continue;
                if (characterColors[i].sprite == null)
                    continue;
                var s = characterColors[i].sprite;
                byte[] color_bytes = s.ToByteStream();
                if (this.character == CharacterConfig.CHARACTERS.Gouki && i > 1)
                    continue;
                if (this.character == CharacterConfig.CHARACTERS.Blanka && i == 9)
                    blanka_offset = 0x02;
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[sprite_offset + i * sprite_length + j + blanka_offset] = color_bytes[j];
                }
            }
            PatchCammySuperStream04(b);
            PatchHondaSuperStream04(b);
            PatchBlankaSuperStream04(b);
            return b;
        }        

        public byte[] PatchCammySuperStream04(byte[] b)
        {
            PaletteHelper.patch_memory(b, 0x448B0, "6606");
            PaletteHelper.patch_memory(b, 0x44952, "6606");
            PaletteHelper.patch_memory(b, 0x449F4, "6606");
            PaletteHelper.patch_memory(b, 0x44A96, "6606");
            PaletteHelper.patch_memory(b, 0x44B38, "6606");
            PaletteHelper.patch_memory(b, 0x44BDA, "6606");
            PaletteHelper.patch_memory(b, 0x44C7C, "6606");
            PaletteHelper.patch_memory(b, 0x44BDA, "6606");
            PaletteHelper.patch_memory(b, 0x44D1E, "6606");
            PaletteHelper.patch_memory(b, 0x44BDA, "6606");
            PaletteHelper.patch_memory(b, 0x44DC0, "6606");
            PaletteHelper.patch_memory(b, 0x44E62, "6606");
            return b;
        }

        public byte[] PatchHondaSuperStream04(byte[] b)
        {
            string patch = "8B08 7B07";
            PaletteHelper.patch_memory(b, 0x401DC, patch);
            PaletteHelper.patch_memory(b, 0x4027E, patch);
            PaletteHelper.patch_memory(b, 0x40320, patch);
            PaletteHelper.patch_memory(b, 0x403C2, patch);
            PaletteHelper.patch_memory(b, 0x40464, patch);
            PaletteHelper.patch_memory(b, 0x40506, patch);
            PaletteHelper.patch_memory(b, 0x405A8, patch);
            PaletteHelper.patch_memory(b, 0x4064A, patch);
            PaletteHelper.patch_memory(b, 0x406EC, patch);
            PaletteHelper.patch_memory(b, 0x4078E, patch);
            return b;
        }

        public byte[] PatchBlankaSuperStream04(byte[] b)
        {
            string patch = "0000 1801 7807 6806 5805 4804 3803 2802 0800 5805 4804 2802 0800 0800 4804 2802 0000 1601 4604 4604 3603 2600 0600 0600 0600 3603 2602 0600 0600 0600 2602 0600";
            PaletteHelper.patch_memory(b, 0x40864, patch);
            PaletteHelper.patch_memory(b, 0x40904, patch);
            PaletteHelper.patch_memory(b, 0x409A4, patch);
            PaletteHelper.patch_memory(b, 0x40A44, patch);
            PaletteHelper.patch_memory(b, 0x40AE4, patch);
            PaletteHelper.patch_memory(b, 0x40B84, patch);
            PaletteHelper.patch_memory(b, 0x40C24, patch);
            PaletteHelper.patch_memory(b, 0x40CC4, patch);
            PaletteHelper.patch_memory(b, 0x40D64, patch);
            return b;
        }
    }

    public class GameSet
    {
        public Dictionary <CharacterConfig.CHARACTERS, CharacterSet> characterDictionary = new Dictionary<CharacterConfig.CHARACTERS, CharacterSet>();

        public static CharacterConfig.CHARACTERS[] supportedCharacters = new CharacterConfig.CHARACTERS[] { CharacterConfig.CHARACTERS.Dictator, CharacterConfig.CHARACTERS.Claw,
                CharacterConfig.CHARACTERS.Guile, CharacterConfig.CHARACTERS.Ryu, CharacterConfig.CHARACTERS.Chun, CharacterConfig.CHARACTERS.Boxer, CharacterConfig.CHARACTERS.Ken,
        CharacterConfig.CHARACTERS.Zangief, CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.CHARACTERS.Sagat, CharacterConfig.CHARACTERS.Feilong, CharacterConfig.CHARACTERS.Deejay,
        CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.CHARACTERS.Cammy, CharacterConfig.CHARACTERS.Thawk, CharacterConfig.CHARACTERS.Blanka, CharacterConfig.CHARACTERS.Gouki
        };


        public Bitmap GenerateColorSheet()
        {
            int buffer = 10;
            int h = 0;
            int w = 0;
            List<Bitmap> colorSetKey = new List<Bitmap>();

            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                Bitmap key = character.GenerateColorSetKey();
                colorSetKey.Add(key);
                w = Math.Max(w, key.Width);
                h = h + buffer + key.Height;
            }

            Bitmap b = new Bitmap(w, h );
            Graphics gfb = Graphics.FromImage(b);

            int x = 0;
            int y = 0;
            for (int i = 0; i < colorSetKey.Count; i++)
            {
                Bitmap key = colorSetKey[i];
                gfb.DrawImage(key, new Point(x, y));
                y = y + key.Height + buffer;
            }
            return b;
        }


        public Bitmap GenerateColorSheetAligned()
        {
            int buffer = 10;
            int h = 0;
            int w = 0;
            List<Bitmap> colorSetKey = new List<Bitmap>();

            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                Bitmap key = character.GenerateColorSetKeyAligned();
                colorSetKey.Add(key);
                w = Math.Max(w, key.Width);
                h = h + buffer + key.Height;
            }

            Bitmap b = new Bitmap(w, h);
            Graphics gfb = Graphics.FromImage(b);

            int x = 0;
            int y = 0;
            for (int i = 0; i < colorSetKey.Count; i++)
            {
                Bitmap key = colorSetKey[i];
                gfb.DrawImage(key, new Point(x, y));
                y = y + key.Height + buffer;
            }
            return b;
        }


        public byte[] patch_sprites_stream04(byte[] b)
        {
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] patch_portraits_stream03(byte[] b)
        {
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        private byte[] PatchOldBisonPunchesStream(byte[] b)
        {
            foreach (int offset in CharacterConfig.bisonPunchesAddresses)
            {
                b[offset] = (byte)CharacterConfig.bisonPunchesValue;
            }
            return b;
        }

        public static GameSet GameSetFromZipStream(Stream fileStream)
        {
            var gs = new GameSet();

            foreach (CharacterConfig.CHARACTERS character in supportedCharacters)
            {
                gs.characterDictionary[character] = CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, character);
            }
            return gs;
        }

        public static GameSet GameSetFromZipColorSet(Stream fileStream)
        {
            var gs = new GameSet();

            foreach (CharacterConfig.CHARACTERS character in supportedCharacters)
            {
                gs.characterDictionary[character] = CharacterSet.CharacterColorSetFromZipColorSetChar(fileStream, character);
            }
           return gs;
        }

        public void PatchZippedRom(Stream fileStream, bool fixOldDictatorPunches)
        {
            using (var zip = new ZipArchive(fileStream, ZipArchiveMode.Update))
            {
                foreach (var entry in zip.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        byte[] patched_b;

                        if (entry.Name == "sfxe.03c" ||
                            entry.Name == "sfxjd.03c" ||
                            entry.Name == "sfxj.03c" )
                        {
                            using (var memorySubStream = new MemoryStream())
                            {
                                stream.CopyTo(memorySubStream);
                                var b = memorySubStream.ToArray();
                                patched_b = patch_portraits_stream03(b);
                            }
                            var c = stream.CanSeek;
                            stream.Position = 0;
                            stream.Write(patched_b, 0, patched_b.Length);
                        }
                        else if (entry.Name == "sfxe.04a" ||
                            entry.Name == "sfxjd.04a" ||
                            entry.Name == "sfxj.04a")
                        {
                            using (var memorySubStream = new MemoryStream())
                            {
                                stream.CopyTo(memorySubStream);
                                var b = memorySubStream.ToArray();
                                patched_b = patch_sprites_stream04(b);
                            }
                            var c = stream.CanSeek;
                            stream.Position = 0;
                            stream.Write(patched_b, 0, patched_b.Length);
                        }
                        else if ((entry.Name == "sfxe.06a" ||
                            entry.Name == "sfxjd.06a" ||
                            entry.Name == "sfxj.06a") && fixOldDictatorPunches)
                        {
                            using (var memorySubStream = new MemoryStream())
                            {
                                stream.CopyTo(memorySubStream);
                                var b = memorySubStream.ToArray();
                                patched_b = PatchOldBisonPunchesStream(b);
                            }
                            var c = stream.CanSeek;
                            stream.Position = 0;
                            stream.Write(patched_b, 0, patched_b.Length);
                        }
                    }
                }
            }
        }
    }
}
