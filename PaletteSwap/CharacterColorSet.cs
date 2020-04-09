﻿using PaletteSwap.Properties;
using System;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;

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

        public CharacterSet(CharacterConfig.CHARACTERS character)
        {
            this.sprite_offset = CharacterConfig.GetSpriteBeginOffset(character);
            this.sprite_length = CharacterConfig.spriteColorLength;
            this.portrait_offset = CharacterConfig.GetPortrait1BeginOffset(character);
            this.portrait2_offset = CharacterConfig.GetPortrait2BeginOffset(character);
            this.portrait_length = CharacterConfig.portraitColorLength;

            characterColors[0] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.lp);
            characterColors[1] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.mp);
            characterColors[2] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.hp);
            characterColors[3] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.lk);
            characterColors[4] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.mk);
            characterColors[5] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.hk);
            characterColors[6] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.start);
            characterColors[7] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.hold);
            characterColors[8] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.old1);
            characterColors[9] = Character.createDefaultCharacter(character, CharacterConfig.BUTTONS.old2);
        }

        public CharacterSet()
        {

        }

        public static CharacterSet GenerateDictatorCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Dictator);
        }

        public static CharacterSet GenerateGuileCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Guile);
        }
        // sprite block length
        // GUI - 40E60
        // KEN - 414CC
        // CHU - 41B38
        // ZAN - 421A4
        // DHA - 42810
        // DIC - 42E7C
        // SAG = 0x434E8
        // BOX - 0x43B54
        // CLW00 = 0x441C0 - step of 66C
        // CAM00 = 0x4482C
        /*36CFE + (Char_ID * 0x500) + (Palette_ID * 0x80)formula to get the address of the 
         * losing portrait additional paletes
         31C48 + (Char_ID * 0x500) + (Palette_ID * 0x80) and this is for the normal portrait, 
         just in case
         // ryu 0
         // eho 1
         // bla 2
         // gui 3
         // ken 4
         // chu 5
         // zan 6
         // dha 7
         // dic 8
         // sag 9
         // box A
         // cla B
         // cam c
         // tha d
         // fei e
         // dee f
*/
        public static CharacterSet GenerateClawCharacterSet()
        {
            return new CharacterSet(CharacterConfig.CHARACTERS.Claw);
        }

        public static CharacterSet CharacterColorSetFromStreams(byte[] sprites, byte[] portraits)
        {
            var cs = GenerateDictatorCharacterSet();
            byte[] sprite_bytes = new byte[cs.sprite_length];
            byte[] portrait_bytes = new byte[cs.portrait_length];
            for (int i = 0; i < 10; i++)
            {
                Array.Copy(sprites, cs.sprite_offset + i * cs.sprite_length, sprite_bytes, 0, cs.sprite_length);
                Array.Copy(portraits, cs.portrait_offset + i * cs.portrait_length, portrait_bytes, 0, cs.portrait_length);
                cs.characterColors[i].sprite.LoadStream(sprite_bytes);
                cs.characterColors[i].portrait.LoadStream(portrait_bytes);
            }
            return cs;
        }

        public static CharacterSet CharacterColorSetFromStreamsChar(byte[] sprites, byte[] portraits, CharacterConfig.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet(characterType);

            byte[] sprite_bytes = new byte[cs.sprite_length];
            byte[] portrait_bytes = new byte[cs.portrait_length];
            for (int i = 0; i < 10; i++)
            {
                Array.Copy(sprites, cs.sprite_offset + i * cs.sprite_length, sprite_bytes, 0, cs.sprite_length);
                Array.Copy(portraits, cs.portrait_offset + i * cs.portrait_length, portrait_bytes, 0, cs.portrait_length);
                cs.characterColors[i].sprite.LoadStream(sprite_bytes);
                cs.characterColors[i].portrait.LoadStream(portrait_bytes);
            }
            return cs;
        }

        // todo, delete this. is it old?
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

            return CharacterColorSetFromStreamsChar(sprites, portraits, characterType);
        }

        public static CharacterSet CharacterColorSetFromZipStream(Stream fileStream)
        {
            var cs = GenerateDictatorCharacterSet();
            byte[] sprites = new byte[cs.sprite_length];
            byte[] portraits = new byte[cs.portrait_length];

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

        public static GameSet GameSetFromZipStream(Stream fileStream)
        {
            var gs = new GameSet();
            gs.characterDictionary[CharacterConfig.CHARACTERS.Dictator] = CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, CharacterConfig.CHARACTERS.Dictator);
            gs.characterDictionary[CharacterConfig.CHARACTERS.Claw] = CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, CharacterConfig.CHARACTERS.Claw);
            gs.characterDictionary[CharacterConfig.CHARACTERS.Guile] = CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, CharacterConfig.CHARACTERS.Guile);
            return gs;
        }
    }
}
