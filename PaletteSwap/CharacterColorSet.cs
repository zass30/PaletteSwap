﻿using PaletteSwap.Properties;
using System;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

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

        public Bitmap GeneratePortraitKey()
        {
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
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[portrait_offset + i * portrait_length + j] = color_bytes[j];
                    b[portrait2_offset + i * portrait_length + j] = color_bytes[j];
                }
            }
            return b;
        }

        public byte[] portraits_stream03()
        {
            byte[] b = Resources.sfxe03c;

            return patch_portraits_stream03(b);
        }

        public byte[] patch_sprites_stream04(byte[] b)
        {
            for (int i = 0; i < 10; i++)
            {
                if (characterColors[i] == null)
                    continue;
                if (characterColors[i].sprite == null)
                    continue;
                var s = characterColors[i].sprite;
                byte[] color_bytes = s.ToByteStream();
                if (this.character == CharacterConfig.CHARACTERS.Blanka && i == 9)
                    sprite_offset += 0x02;
                for (int j = 0; j < color_bytes.Length; j++)
                {
                    b[sprite_offset + i * sprite_length + j] = color_bytes[j];
                }
            }
            return b;
        }        

            public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe04a;
            return patch_sprites_stream04(b);
        }
    }

    public class GameSet
    {
        public Dictionary <CharacterConfig.CHARACTERS, CharacterSet> characterDictionary = new Dictionary<CharacterConfig.CHARACTERS, CharacterSet>();

        public static CharacterConfig.CHARACTERS[] supportedCharacters = new CharacterConfig.CHARACTERS[] { CharacterConfig.CHARACTERS.Dictator, CharacterConfig.CHARACTERS.Claw,
                CharacterConfig.CHARACTERS.Guile, CharacterConfig.CHARACTERS.Ryu, CharacterConfig.CHARACTERS.Chun, CharacterConfig.CHARACTERS.Boxer, CharacterConfig.CHARACTERS.Ken,
        CharacterConfig.CHARACTERS.Zangief, CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.CHARACTERS.Sagat, CharacterConfig.CHARACTERS.Feilong, CharacterConfig.CHARACTERS.Deejay,
        CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.CHARACTERS.Cammy, CharacterConfig.CHARACTERS.Thawk, CharacterConfig.CHARACTERS.Blanka
        };


        public Bitmap GenerateNeutralKey()
        {
            int buffer = 10;
            int h = 0;
            int w = 0;
            List<Bitmap> spriteKeyList = new List<Bitmap>();
            foreach (var k in characterDictionary)
            {
                var spriteKey = k.Value.GenerateSpriteKey();
                h = h + spriteKey.Height + buffer;
                if (w < spriteKey.Width)
                    w = spriteKey.Width + buffer * 2;
                spriteKeyList.Add(spriteKey);
            }
            Bitmap b = new Bitmap(w * 2 + buffer, h );
            Graphics gfb = Graphics.FromImage(b);

            int x = 0;
            int y = 0;
            for (int i = 0; i < spriteKeyList.Count; i = i+2)
            {

                gfb.DrawImage(spriteKeyList[i], new Point(x, y));
                gfb.DrawImage(spriteKeyList[i+1], new Point(spriteKeyList[i].Width + buffer, y));
                y += Math.Max(spriteKeyList[i].Height, spriteKeyList[i+1].Height) + buffer;

            }
            return b;
        }

        public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe04a;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03()
        {
            byte[] b = Resources.sfxe03c;
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

        public byte[] PatchOldBisonPunches06()
        {
            byte[] b = Resources.sfxe06a;
            return PatchOldBisonPunchesStream(b);
        }

        public byte[] PatchOldBisonPunches06phoenix()
        {
            byte[] b = Resources.sfxjd06a;
            return PatchOldBisonPunchesStream(b);
        }

        public byte[] PatchOldBisonPunches06japanese()
        {
            byte[] b = Resources.sfxj06a;
            return PatchOldBisonPunchesStream(b);
        }

        public byte[] PatchOldBisonPunches06redggpo()
        {
            byte[] b = Resources.sfxo06a;
            return PatchOldBisonPunchesStream(b);
        }

        public byte[] sprites_stream04phoenix()
        {
            byte[] b = Resources.sfxjd04a;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;

        }

        public byte[] portraits_stream03phoenix()
        {
            byte[] b = Resources.sfxjd03c;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_portraits_stream03(b);
            }
            return b;

        }


        public byte[] sprites_stream04newlegacy()
        {
            byte[] b = Resources.sfxe04aNewLegacy;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;

        }

        public byte[] portraits_stream03newlegacy()
        {
            byte[] b = Resources.sfxe03cNewLegacy;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        public byte[] sprites_stream04japanese()
        {
            byte[] b = Resources.sfxj04a;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03japanese()
        {
            byte[] b = Resources.sfxj03c;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        public byte[] sprites_stream04redggpo()
        {
            byte[] b = Resources.sfxo04a;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03redggpo()
        {
            byte[] b = Resources.sfxo03c;
            foreach (var k in characterDictionary)
            {
                var character = k.Value;
                b = character.patch_portraits_stream03(b);
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
    }
}
