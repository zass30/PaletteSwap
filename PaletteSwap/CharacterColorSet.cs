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

        public static CharacterSet GenerateDictatorCharacterSet()
        {
            CharacterSet cs = new CharacterSet();
            cs.sprite_offset = 0x00042E7E;
            cs.sprite_length = 0xA2;
            cs.portrait_offset = 0x34448;  // character id = 8
            cs.portrait2_offset = 0x394FE;
            cs.portrait_length = 0x80;
            cs.characterColors[0] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            cs.characterColors[1] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mp);
            cs.characterColors[2] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hp);
            cs.characterColors[3] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lk);
            cs.characterColors[4] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mk);
            cs.characterColors[5] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hk);
            cs.characterColors[6] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.start);
            cs.characterColors[7] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hold);
            cs.characterColors[8] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.old1);
            cs.characterColors[9] = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.old2);
            return cs;
        }
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
            CharacterSet cs = new CharacterSet();
            cs.sprite_offset = 0x000441C2;
            cs.sprite_length = 0xA2;
            cs.portrait_offset = 0x35348;//character id B
            cs.portrait2_offset = 0x3A3FE;
            cs.portrait_length = 0x80;
            cs.characterColors[0] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.lp);
            cs.characterColors[1] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.mp);
            cs.characterColors[2] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.hp);
            cs.characterColors[3] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.lk);
            cs.characterColors[4] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.mk);
            cs.characterColors[5] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.hk);
            cs.characterColors[6] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.start);
            cs.characterColors[7] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.hold);
            cs.characterColors[8] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.old1);
            cs.characterColors[9] = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.old2);
            return cs;
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

        public static CharacterSet CharacterColorSetFromStreamsChar(byte[] sprites, byte[] portraits, Character.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet();
            switch (characterType)
            {
                case Character.CHARACTERS.Dictator:
                    cs = GenerateDictatorCharacterSet();
                    break;
                case Character.CHARACTERS.Claw:
                    cs = GenerateClawCharacterSet();
                    break;
            }
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

        public static CharacterSet CharacterColorSetFromZipStreamChar(Stream fileStream, Character.CHARACTERS characterType)
        {
            CharacterSet cs = new CharacterSet();
            switch (characterType) {
                case Character.CHARACTERS.Dictator:
                    cs = GenerateDictatorCharacterSet();
                    break;
                case Character.CHARACTERS.Claw:
                    cs = GenerateClawCharacterSet();
                    break;
            }

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

    // test this
    public class GameSet
    {
        public List<CharacterSet> characters = new List<CharacterSet>();

        public byte[] sprites_stream04()
        {
            byte[] b = Resources.sfxe04a;
            foreach (var character in characters)
            {
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03()
        {
            byte[] b = Resources.sfxe03c;
            foreach (var character in characters)
            {
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        public byte[] sprites_stream04phoenix()
        {
            byte[] b = Resources.sfxjd04a;
            foreach (var character in characters)
            {
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03phoenix()
        {
            byte[] b = Resources.sfxjd03c;
            foreach (var character in characters)
            {
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        public byte[] sprites_stream04japanese()
        {
            byte[] b = Resources.sfxj04a;
            foreach (var character in characters)
            {
                b = character.patch_sprites_stream04(b);
            }
            return b;
        }

        public byte[] portraits_stream03japanese()
        {
            byte[] b = Resources.sfxj03c;
            foreach (var character in characters)
            {
                b = character.patch_portraits_stream03(b);
            }
            return b;
        }

        public static GameSet GameSetFromZipStream(Stream fileStream)
        {
            var gs = new GameSet();
            gs.characters.Add(CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, Character.CHARACTERS.Dictator));
            gs.characters.Add(CharacterSet.CharacterColorSetFromZipStreamChar(fileStream, Character.CHARACTERS.Claw));
            return gs;
        }
    }
}
