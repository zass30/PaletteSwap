using System;
using System.Collections.Generic;
using System.Drawing;

namespace PaletteSwap
{
    // new character config that contains
    // addresses in rom for sprites/portraits
    public struct ByteStreamPair
    {
        public byte[] spriteStream;
        public byte[] portraitStream;
    }

    public struct CharacterConfig
    {
        static public int spriteBeginOffset = 0x3FB1E;
        static public int spriteStep = 0x66c;
        static public int spriteColorLength = 0xA2;
        static public int portrait1BeginOffset = 0x31C48;
        static public int portrait2BeginOffset = 0x36CFE;
        static public int portraitStep = 0x500;
        static public int portraitColorLength = 0x80;
        static public List<int> bisonPunchesAddresses =
            new List<int>() { 0x6BD1A, 0x6BD1E, 0x6BFB6, 0x6C024, 0x6C038, 0x6C03C, 0x6C04C, 0x6C050 };
        static public int bisonPunchesValue = 0x14;

        public enum BUTTONS { lp, mp, hp, lk, mk, hk, start, hold, old1, old2 };
        public enum CHARACTERS
        {
            Ryu, Ehonda, Blanka, Guile, Ken, Chun, Zangief, Dhalsim,
            Dictator, Sagat, Boxer, Claw, Cammy, Thawk, Feilong, Deejay, Gouki
        };

        public static ByteStreamPair GetByteStreamPair(CHARACTERS c, BUTTONS b)
        {
            byte[] sprite_bytestream = new byte[0];
            byte[] portrait_bytestream = new byte[0];
            sprite_bytestream = GetSpriteResourceFromRom(c, b);
            portrait_bytestream = GetPortraitResourceFromRom(c, b);
            ByteStreamPair p = new ByteStreamPair();
            p.spriteStream = sprite_bytestream;
            p.portraitStream = portrait_bytestream;
            return p;       
        }

        public static string CodeFromCharacterEnum(CHARACTERS c)
        {
            switch (c)
            {
                case CHARACTERS.Ryu:
                    return "RYU";
                case CHARACTERS.Ehonda:
                    return "EHO";
                case CHARACTERS.Blanka:
                    return "BLA";
                case CHARACTERS.Guile:
                    return "GUI";
                case CHARACTERS.Ken:
                    return "KEN";
                case CHARACTERS.Chun:
                    return "CHU";
                case CHARACTERS.Zangief:
                    return "ZAN";
                case CHARACTERS.Dhalsim:
                    return "DHA";
                case CHARACTERS.Dictator:
                    return "DIC";
                case CHARACTERS.Sagat:
                    return "SAG";
                case CHARACTERS.Boxer:
                    return "BOX";
                case CHARACTERS.Claw:
                    return "CLA";
                case CHARACTERS.Cammy:
                    return "CAM";
                case CHARACTERS.Thawk:
                    return "THA";
                case CHARACTERS.Feilong:
                    return "FEI";
                case CHARACTERS.Deejay:
                    return "DEE";
                case CHARACTERS.Gouki:
                    return "GOU";
            }
            throw new ArgumentException("Invalid Character type");
        }

        public static CHARACTERS CharacterEnumFromCode(string s)
        {
            switch (s)
            {
                case "RYU":
                    return CHARACTERS.Ryu;
                case "EHO":
                    return CHARACTERS.Ehonda;
                case "BLA":
                    return CHARACTERS.Blanka;
                case "GUI":
                    return CHARACTERS.Guile;
                case "KEN":
                    return CHARACTERS.Ken;
                case "CHU":
                    return CHARACTERS.Chun;
                case "ZAN":
                    return CHARACTERS.Zangief;
                case "DHA":
                    return CHARACTERS.Dhalsim;
                case "DIC":
                    return CHARACTERS.Dictator;
                case "SAG":
                    return CHARACTERS.Sagat;
                case "BOX":
                    return CHARACTERS.Boxer;
                case "CLA":
                    return CHARACTERS.Claw;
                case "CAM":
                    return CHARACTERS.Cammy;
                case "THA":
                    return CHARACTERS.Thawk;
                case "FEI":
                    return CHARACTERS.Feilong;
                case "DEE":
                    return CHARACTERS.Deejay;
                case "GOU":
                    return CHARACTERS.Gouki;
            }
            throw new ArgumentException("Invalid Character type");
        }

        public static int GetButtonIdFromButton(BUTTONS b)
        {
            switch (b)
            {
                case BUTTONS.lp:
                    return 0;
                case BUTTONS.mp:
                    return 1;
                case BUTTONS.hp:
                    return 2;
                case BUTTONS.lk:
                    return 3;
                case BUTTONS.mk:
                    return 4;
                case BUTTONS.hk:
                    return 5;
                case BUTTONS.start:
                    return 6;
                case BUTTONS.hold:
                    return 7;
                case BUTTONS.old1:
                    return 8;
                case BUTTONS.old2:
                    return 9;
            }
            return 0;
        }

        public static int GetCharIdFromCharacter(CHARACTERS c)
        {
            switch (c)
            {
                case CHARACTERS.Ryu:
                    return 0;
                case CHARACTERS.Ehonda:
                    return 1;
                case CHARACTERS.Blanka:
                    return 2;
                case CHARACTERS.Guile:
                    return 3;
                case CHARACTERS.Ken:
                    return 4;
                case CHARACTERS.Chun:
                    return 5;
                case CHARACTERS.Zangief:
                    return 6;
                case CHARACTERS.Dhalsim:
                    return 7;
                case CHARACTERS.Dictator:
                    return 8;
                case CHARACTERS.Sagat:
                    return 9;
                case CHARACTERS.Boxer:
                    return 10;
                case CHARACTERS.Claw:
                    return 11;
                case CHARACTERS.Cammy:
                    return 12;
                case CHARACTERS.Thawk:
                    return 13;
                case CHARACTERS.Feilong:
                    return 14;
                case CHARACTERS.Deejay:
                    return 15;
                case CHARACTERS.Gouki:
                    return 16;
            }
            return 0;
        }

        public static byte[] GetPortraitResourceFromRom(CHARACTERS character, BUTTONS button)
        {
            int i = GetButtonIdFromButton(button);
            var portrait2_offset = CharacterConfig.GetPortrait2BeginOffset(character);
            var portrait_length = CharacterConfig.portraitColorLength;

            byte[] portraits = Properties.Resources.sfxe03c;
            byte[] portrait_bytes = new byte[portrait_length];

            Array.Copy(portraits, portrait2_offset + i * portrait_length, portrait_bytes, 0, portrait_length);
            return portrait_bytes;
        }

        public static byte[] GetSpriteResourceFromRom(CharacterConfig.CHARACTERS character, BUTTONS button)
        {
            int i = GetButtonIdFromButton(button);
            var sprite_offset = CharacterConfig.GetSpriteBeginOffset(character);
            if (character == CharacterConfig.CHARACTERS.Blanka && button == BUTTONS.old2)
                sprite_offset = sprite_offset + 0x02;
            var sprite_length = CharacterConfig.spriteColorLength;
            if (character == CharacterConfig.CHARACTERS.Blanka)
                sprite_length = sprite_length - 0x02;

            byte[] sprites = Properties.Resources.sfxe04a;

            byte[] sprite_bytes = new byte[sprite_length];

            Array.Copy(sprites, sprite_offset + i * sprite_length, sprite_bytes, 0, sprite_length);
            byte[] final = new byte[sprite_length - 2];
            Array.Copy(sprite_bytes, final, final.Length);
            return final;
        }


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

        public static int GetSpriteBeginOffset(CHARACTERS c)
        {
            if (c == CHARACTERS.Ryu)
                return 0x3FB2E;
            else if (c == CHARACTERS.Ehonda)
                return 0x4019A;
            else if (c == CHARACTERS.Blanka)
                return 0x40806;
            return spriteBeginOffset + GetCharIdFromCharacter(c) * spriteStep;
        }

        public static int GetPortrait1BeginOffset(CHARACTERS c)
        {
            return portrait1BeginOffset + GetCharIdFromCharacter(c) * portraitStep;
        }

        public static int GetPortrait2BeginOffset(CHARACTERS c)
        {
            return portrait2BeginOffset + GetCharIdFromCharacter(c) * portraitStep;
        }
    }

    public struct PaletteConfig
    {
        public Dictionary<string, List<int>> labelOffsets;
        public List<ColorOffset> defaultColorOffsets;
        public List<int> unusedOffsets;
        public int streamLength;
        static readonly int ROWLEN = 32;

        public static PaletteConfig GenerateSpriteConfig(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Dictator:
                    return DICTATOR.GenerateDictatorSpriteConfig();
                default:
                    return GenerateSpriteConfigGeneric(c);
            }
            throw new Exception("Invalid character");
        }

        public static PaletteConfig GeneratePortraitConfig(CharacterConfig.CHARACTERS c)
        {
            Dictionary<string, List<int>> labeloffsets;
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    labeloffsets = RYU.GenerateRyuPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Ehonda:
                    labeloffsets = HONDA.GenerateHondaPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Blanka:
                    labeloffsets = BLANKA.GenerateBlankaPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Guile:
                    labeloffsets = GUILE.GenerateGuilePortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Ken:
                    labeloffsets = KEN.GenerateKenPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Chun:
                    labeloffsets = CHUN.GenerateChunPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Zangief:
                    labeloffsets = ZANGIEF.GenerateZangiefPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Dhalsim:
                    labeloffsets = DHALSIM.GenerateDhalsimPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Dictator:
                    labeloffsets = DICTATOR.GenerateDictatorPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Sagat:
                    labeloffsets = SAGAT.GenerateSagatPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Boxer:
                    labeloffsets = BOXER.GenerateBoxerPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Claw:
                    labeloffsets = CLAW.GenerateClawPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Cammy:
                    labeloffsets = CAMMY.GenerateCammyPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Thawk:
                    labeloffsets = HAWK.GenerateHawkPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Feilong:
                    labeloffsets = FEI.GenerateFeiPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Deejay:
                    labeloffsets = DEEJAY.GenerateDeejayPortraitOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Gouki:
                    labeloffsets = GOUKI.GenerateGoukiPortraitOffsets();
                    break;
                default:
                    throw new Exception("Invalid character");
            }
            int MEMLEN = ROWLEN * 4;
            List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
            for (int i = 0; i < 4; i++)
            {
                ColorOffset dco = new ColorOffset();
                dco.c = PaletteHelper.MemFormatToColor("9A00");
                dco.position = 30 + ROWLEN * i;
                if (c == CharacterConfig.CHARACTERS.Gouki && i > 1)
                {
                    // do nothing
                }
                else
                {
                    defaultColorOffsets.Add(dco);

                }
            }

            PaletteConfig pc = new PaletteConfig();
            pc.labelOffsets = labeloffsets;
            pc.defaultColorOffsets = defaultColorOffsets;
            pc.unusedOffsets = new List<int>() { };
            pc.streamLength = MEMLEN;
            if (c == CharacterConfig.CHARACTERS.Gouki) // New Legacy puts data in 2nd half of gouki portrait
                pc.streamLength = MEMLEN / 2;

            return pc;
        }
        // todo make this a general function to avoid duplication
        public static Dictionary<string, List<int>> GetSpriteOffsets(CharacterConfig.CHARACTERS c)
        {
                       switch (c)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU.GenerateRyuSpriteOffsets();
                case CharacterConfig.CHARACTERS.Ken:
                    return KEN.GenerateKenSpriteOffsets();
                case CharacterConfig.CHARACTERS.Chun:
                    return CHUN.GenerateChunSpriteOffsets();
                case CharacterConfig.CHARACTERS.Guile:
                    return GUILE.GenerateGuileSpriteOffsets();
                case CharacterConfig.CHARACTERS.Claw:
                    return CLAW.GenerateClawSpriteOffsets();
                case CharacterConfig.CHARACTERS.Dictator:
                    return DICTATOR.GenerateDictatorSpriteOffsets();
                case CharacterConfig.CHARACTERS.Boxer:
                    return BOXER.GenerateBoxerSpriteOffsets();
                case CharacterConfig.CHARACTERS.Zangief:
                    return ZANGIEF.GenerateZangiefSpriteOffsets();
                case CharacterConfig.CHARACTERS.Ehonda:
                    return HONDA.GenerateHondaSpriteOffsets();
                case CharacterConfig.CHARACTERS.Sagat:
                    return SAGAT.GenerateSagatSpriteOffsets();
                case CharacterConfig.CHARACTERS.Feilong:
                    return FEI.GenerateFeiSpriteOffsets();
                case CharacterConfig.CHARACTERS.Deejay:
                    return DEEJAY.GenerateDeejaySpriteOffsets();
                case CharacterConfig.CHARACTERS.Cammy:
                    return CAMMY.GenerateCammySpriteOffsets();
                case CharacterConfig.CHARACTERS.Thawk:
                    return HAWK.GenerateHawkSpriteOffsets();
                case CharacterConfig.CHARACTERS.Dhalsim:
                    return DHALSIM.GenerateDhalsimSpriteOffsets();
                case CharacterConfig.CHARACTERS.Blanka:
                    return BLANKA.GenerateBlankaSpriteOffsets();
                case CharacterConfig.CHARACTERS.Gouki:
                    return GOUKI.GenerateGoukiSpriteOffsets();
            }
            throw new Exception("Invalid character");
        }
       
        public void createColorOffsets(string defaults, int offset)
        {
            byte[] colordefaults = PaletteHelper.StringToByteStream(defaults);
            byte[] col_byte = new byte[2];
            this.defaultColorOffsets = new List<ColorOffset>();
            for (int i = 0; i < colordefaults.Length; i = i + 2)
            {
                col_byte[0] = colordefaults[i];
                col_byte[1] = colordefaults[i + 1];
                Color defaultcolor = PaletteHelper.ByteToColor(col_byte);
                ColorOffset cp = new ColorOffset();
                cp.c = defaultcolor;
                cp.position = offset + i;
                this.defaultColorOffsets.Add(cp);
            }
        }

        public static PaletteConfig GenerateSpriteConfigGeneric(CharacterConfig.CHARACTERS c)
        {
            Dictionary<string, List<int>> labeloffsets;
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    labeloffsets = RYU.GenerateRyuSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Ehonda:
                    labeloffsets = HONDA.GenerateHondaSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Blanka:
                    labeloffsets = BLANKA.GenerateBlankaSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Guile:
                    labeloffsets = GUILE.GenerateGuileSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Ken:
                    labeloffsets = KEN.GenerateKenSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Chun:
                    labeloffsets = CHUN.GenerateChunSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Zangief:
                    labeloffsets = ZANGIEF.GenerateZangiefSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Dhalsim:
                    labeloffsets = DHALSIM.GenerateDhalsimSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Sagat:
                    labeloffsets = SAGAT.GenerateSagatSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Boxer:
                    labeloffsets = BOXER.GenerateBoxerSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Claw:
                    labeloffsets = CLAW.GenerateClawSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Cammy:
                    labeloffsets = CAMMY.GenerateCammySpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Thawk:
                    labeloffsets = HAWK.GenerateHawkSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Feilong:
                    labeloffsets = FEI.GenerateFeiSpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Deejay:
                    labeloffsets = DEEJAY.GenerateDeejaySpriteOffsets();
                    break;
                case CharacterConfig.CHARACTERS.Gouki:
                    labeloffsets = GOUKI.GenerateGoukiSpriteOffsets();
                    break;
                default:
                    throw new Exception("Invalid character");
            }


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
            int MEMLEN = 30;
            PaletteConfig pc = new PaletteConfig();
            pc.labelOffsets = labeloffsets;
            pc.unusedOffsets = new List<int>();
            pc.defaultColorOffsets = new List<ColorOffset>();
            pc.streamLength = MEMLEN;
            return pc;
        }

        public struct GOUKI
        {
            public static Dictionary<string, List<int>> GenerateGoukiSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "belt", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "hair2", new List<int>() { 14 } },
            { "costume1", new List<int>() { 16 } },
            { "costume2", new List<int>() { 18 } },
            { "costume3", new List<int>() { 20 } },
            { "costume4", new List<int>() { 22 } },
            { "costume5", new List<int>() { 24 } },
            { "costume6", new List<int>() { 26 } },
            { "hair1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }
            /*
             *                             16   18   20   22   24   26   28
 * A101 A202 A303 A404 A505 A606 A707 A808 1502 3704 4805 6907 9B0A CD0C 0F0F 000F 
 * 110A 220A 330A 440A 550A 660A 770A 880A 990A AA0A BB0A CC0A DD0A EE0A FF0A FB00 
 * 1A01 2A02 3A03 4A04 5A05 6A06 7A07 8A08 9A09 AC0A BA0B CA0C DA0D EA0D 5842 5C00 
 * 5101 5201 5301 5401 5501 5601 5701 5801 5901 5A01 5B01 5C01 5D01 5E01 CC0C 1501 
 * E359 AD33 6A2C 62D9 6786 FC7A 5122 EA84 FC8D 4C88 2F68 8B5A 7011 A64A CCD9 4F71
 */
            public static Dictionary<string, List<int>> GenerateGoukiPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "shadow", new List<int>() { 0 } },
        { "skin1", new List<int>() { 28 } },
        { "skin2", new List<int>() { 26 } },
        { "skin3", new List<int>() { 24 } },
        { "skin4", new List<int>() { 22 } },
        { "skin5", new List<int>() { 20 } },
        { "skin6", new List<int>() { 18 } },
        { "skin7", new List<int>() { 16 } },
                };
                return portraitOffsets;
            }
        }

        public struct BLANKA
        {
            public static Dictionary<string, List<int>> GenerateBlankaSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "outline", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "skin7", new List<int>() { 14 } },
            { "costume1", new List<int>() { 16 } },
            { "costume2", new List<int>() { 18 } },
            { "costume3", new List<int>() { 20 } },
            { "costume4", new List<int>() { 22 } },
            { "costume5", new List<int>() { 24 } },
            { "lining1", new List<int>() { 26 } },
            { "lining2", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateBlankaPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6 } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8 } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10 } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12 } },

        { "hair1", new List<int>() { 14, ROWLEN * 2 + 14 } },
        { "hair2", new List<int>() { 16, ROWLEN * 2 + 16 } },
        { "hair3", new List<int>() { 18, ROWLEN * 2 + 18 } },
        { "hair4", new List<int>() { 20, ROWLEN * 2 + 20 } },
        { "hair5", new List<int>() { 22, ROWLEN * 2 + 22 } },
            
        { "teeth1", new List<int>() { 24, ROWLEN * 1 + 24 } },
        { "teeth2", new List<int>() { 26, ROWLEN * 1 + 26 } },
        { "teeth3", new List<int>() { 28, ROWLEN * 1 + 28 } },

        { "blood1", new List<int>() { ROWLEN * 1 + 14} },
        { "blood2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 2 + 24 } },
        { "blood3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 2 + 26 } },
        { "blood4", new List<int>() { ROWLEN * 1 + 20, ROWLEN * 2 + 28 } },
        { "blood5", new List<int>() { ROWLEN * 1 + 22 } },
                };
                return portraitOffsets;
            }
        }

        public struct HAWK
        {
            public static Dictionary<string, List<int>> GenerateHawkSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "hair", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "skin7", new List<int>() { 14 } },
            { "costume6", new List<int>() { 16 } },
            { "costume5", new List<int>() { 18 } },
            { "costume4", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume1", new List<int>() { 26 } },
            { "feathers", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }        

            public static Dictionary<string, List<int>> GenerateHawkPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6 } },

        { "hair1", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8 } },
        { "hair2", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10 } },
        { "hair3", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12 } },

        { "costume1", new List<int>() { 14, ROWLEN * 1 + 14, ROWLEN * 2 + 14, ROWLEN * 3 + 14 } },
        { "costume2", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 2 + 16, ROWLEN * 3 + 16 } },
        { "costume3", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 2 + 18, ROWLEN * 3 + 18 } },
        { "costume4", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 2 + 20, ROWLEN * 3 + 20 } },
        { "costume5", new List<int>() { 22, ROWLEN * 1 + 22 } },
        { "costume6", new List<int>() { 24, ROWLEN * 1 + 24 } },

        { "teeth1", new List<int>() { ROWLEN * 2 + 22, ROWLEN * 3 + 22 } },
        { "teeth2", new List<int>() { ROWLEN * 2 + 24, ROWLEN * 3 + 24 } },

        { "bruise1", new List<int>() { 26, ROWLEN * 2 + 26 } },
        { "bruise2", new List<int>() { 28, ROWLEN * 2 + 28 } },

        { "blood1", new List<int>() { ROWLEN * 1 + 26, ROWLEN * 3 + 26 } },
        { "blood2", new List<int>() { ROWLEN * 1 + 28, ROWLEN * 3 + 28 } },
                };
                return portraitOffsets;
            }
        }

        public struct CAMMY
        {
            public static Dictionary<string, List<int>> GenerateCammySpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "costume6", new List<int>() { 0 } },
            { "skin2", new List<int>() { 2 } },
            { "skin3", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin5", new List<int>() { 8 } },
            { "skin6", new List<int>() { 10 } },
            { "skin7", new List<int>() { 12 } },
            { "costume5", new List<int>() { 14 } },
            { "costume4", new List<int>() { 16 } },
            { "costume3", new List<int>() { 18 } },
            { "costume2", new List<int>() { 20 } },
            { "costume1", new List<int>() { 22 } },
            { "beret", new List<int>() { 24 } },
            { "hair", new List<int>() { 26 } },
            { "skin1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateCammyPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6 } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8 } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10 } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12 } },

        { "hair1", new List<int>() { 14, ROWLEN * 1 + 14, ROWLEN * 2 + 14 } },
        { "hair2", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 2 + 16 } },
        { "hair3", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 2 + 18 } },
        { "hair4", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 2 + 20 } },

        { "beret1", new List<int>() { 22 } },
        { "beret2", new List<int>() { 24 } },
        { "beret3", new List<int>() { 26 } },
        { "beret4", new List<int>() { 28 } },

        { "costume1", new List<int>() { ROWLEN * 1 + 22 } },
        { "costume2", new List<int>() { ROWLEN * 1 + 24 } },
        { "costume3", new List<int>() { ROWLEN * 1 + 26 } },
        { "costume4", new List<int>() { ROWLEN * 1 + 28 } },

        { "eyes1", new List<int>() { ROWLEN * 2 + 22 } },
        { "eyes2", new List<int>() { ROWLEN * 2 + 24 } },
        { "eyes3", new List<int>() { ROWLEN * 2 + 26 } },
        { "eyes4", new List<int>() { ROWLEN * 2 + 28 } },
                };

                return portraitOffsets;
            }
        }

        public struct DHALSIM
        {
            public static Dictionary<string, List<int>> GenerateDhalsimSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "necklace3", new List<int>() { 0 } },
            { "skin6", new List<int>() { 2 } },
            { "skin5", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin3", new List<int>() { 8 } },
            { "skin2", new List<int>() { 10 } },
            { "skin1", new List<int>() { 12 } },
            { "necklace1", new List<int>() { 14 } },
            { "necklace2", new List<int>() { 16 } },
            { "paint", new List<int>() { 18 } },
            { "costume5", new List<int>() { 20 } },
            { "costume4", new List<int>() { 22 } },
            { "costume3", new List<int>() { 24 } },
            { "costume2", new List<int>() { 26 } },
            { "costume1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateDhalsimPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6 } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8 } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10 } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12 } },

        { "necklace1", new List<int>() { 14, ROWLEN * 1 + 14, ROWLEN * 3 + 14 } },
        { "necklace2", new List<int>() { ROWLEN * 1 + 16 } },
        { "necklace3", new List<int>() { 16, ROWLEN * 1 + 18, ROWLEN * 3 + 16 } },
        { "necklace4", new List<int>() { 18, ROWLEN * 1 + 20, ROWLEN * 2 + 18, ROWLEN * 3 + 18 } },
        { "necklace5", new List<int>() { ROWLEN * 1 + 22 } },

        { "costume1", new List<int>() { ROWLEN * 1 + 24 } },
        { "costume2", new List<int>() { ROWLEN * 1 + 26 } },
        { "costume3", new List<int>() { ROWLEN * 1 + 28 } },

        { "paint1", new List<int>() { 24, ROWLEN * 2 + 24, ROWLEN * 3 + 24 } },
        { "paint2", new List<int>() { 26, ROWLEN * 2 + 14 } },
        { "paint3", new List<int>() { 28, ROWLEN * 2 + 16 } },


        { "bruise1", new List<int>() { 20, ROWLEN * 2 + 20, ROWLEN * 3 + 20 } },
        { "bruise2", new List<int>() { 22, ROWLEN * 2 + 22, ROWLEN * 3 + 22 } },

        { "blood1", new List<int>() { ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
        { "blood2", new List<int>() { ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },
                };

                return portraitOffsets;
            }
        }

        public struct DEEJAY
        {
            public static Dictionary<string, List<int>> GenerateDeejaySpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "skin9", new List<int>() { 0 } },
            { "skin2", new List<int>() { 2 } },
            { "skin3", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin5", new List<int>() { 8 } },
            { "skin6", new List<int>() { 10 } },
            { "skin7", new List<int>() { 12 } },
            { "skin8", new List<int>() { 14 } },
            { "costume6", new List<int>() { 16 } },
            { "costume5", new List<int>() { 18 } },
            { "costume4", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume1", new List<int>() { 26 } },
            { "skin1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateDeejayPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6 } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8 } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10 } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12 } },

        { "teeth1", new List<int>() { 14, ROWLEN * 1 + 14 } },
        { "teeth2", new List<int>() { 16, ROWLEN * 1 + 16 } },
        { "teeth3", new List<int>() { 18, ROWLEN * 1 + 18 } },
        { "teeth4", new List<int>() { 20, ROWLEN * 1 + 20 } },

        { "necklace1", new List<int>() { 22 } },
        { "necklace2", new List<int>() { 24 } },
        { "necklace3", new List<int>() { 26 } },
        { "necklace4", new List<int>() { 28 } },

        { "blood1", new List<int>() { ROWLEN * 1 + 22 } },
        { "blood2", new List<int>() { ROWLEN * 1 + 24 } },
        { "blood3", new List<int>() { ROWLEN * 1 + 26 } },
        { "blood4", new List<int>() { ROWLEN * 1 + 28 } },
                };

                return portraitOffsets;
            }
        }
        
        public struct SAGAT
        {
            public static Dictionary<string, List<int>> GenerateSagatSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "stripe1", new List<int>() { 0 } },
            { "stripe2", new List<int>() { 2 } },
            { "skin1", new List<int>() { 4 } },
            { "skin2", new List<int>() { 6 } },
            { "skin3", new List<int>() { 8 } },
            { "skin4", new List<int>() { 10 } },
            { "skin5", new List<int>() { 12 } },
            { "skin6", new List<int>() { 14 } },
            { "wraps1", new List<int>() { 16 } },
            { "wraps2", new List<int>() { 18 } },
            { "wraps3", new List<int>() { 20 } },
            { "shorts1", new List<int>() { 22 } },
            { "shorts2", new List<int>() { 24 } },
            { "shorts3", new List<int>() { 26 } },
            { "shorts4", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateSagatPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6 } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8 } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10 } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12 } },

        { "wraps1", new List<int>() { 14, ROWLEN * 1 + 14 } },
        { "wraps2", new List<int>() { 16, ROWLEN * 1 + 16 } },
        { "wraps3", new List<int>() { 18, ROWLEN * 1 + 18 } },

        { "teeth1", new List<int>() { ROWLEN * 2 + 14 } },
        { "teeth2", new List<int>() { ROWLEN * 2 + 16 } },
        { "teeth3", new List<int>() { ROWLEN * 2 + 18 } },

        { "blood1", new List<int>() { 20 } },
        { "blood2", new List<int>() { 22 } },
        { "blood4", new List<int>() { ROWLEN * 2 + 20, ROWLEN * 3 + 16 } },
        { "blood6", new List<int>() { ROWLEN * 2 + 22, ROWLEN * 3 + 18 } },
        { "blood3", new List<int>() { ROWLEN * 1 + 26, ROWLEN * 3 + 14 } },
        { "blood5", new List<int>() { ROWLEN * 1 + 28 } },

        { "bruise1", new List<int>() { ROWLEN * 1 + 20 } },
        { "bruise2", new List<int>() { ROWLEN * 1 + 22 } },
        { "bruise3", new List<int>() { ROWLEN * 1 + 24 } },

        { "scars1", new List<int>() { 24, ROWLEN * 2 + 24, ROWLEN * 3 + 24 } },
        { "scars2", new List<int>() { 26, ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
        { "scars3", new List<int>() { 28, ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },
                };

                return portraitOffsets;
            }
        }

        public struct FEI
        {
            public static Dictionary<string, List<int>> GenerateFeiSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "shoes", new List<int>() { 0 } },
            { "skin2", new List<int>() { 2 } },
            { "skin3", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin5", new List<int>() { 8 } },
            { "skin6", new List<int>() { 10 } },
            { "skin7", new List<int>() { 12 } },
            { "skin8", new List<int>() { 14 } },
            { "costume6", new List<int>() { 16 } },
            { "costume5", new List<int>() { 18 } },
            { "costume4", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume1", new List<int>() { 26 } },
            { "skin1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateFeiPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6 } },

        { "hair1", new List<int>() { 8, ROWLEN * 1 + 8 } },
        { "hair2", new List<int>() { 10, ROWLEN * 1 + 10 } },
        { "hair3", new List<int>() { 12, ROWLEN * 1 + 12 } },

        { "costume1", new List<int>() { 14 } },
        { "costume2", new List<int>() { 16 } },
        { "costume3", new List<int>() { 18 } },
        { "costume4", new List<int>() { 20 } },
        { "costume5", new List<int>() { 22, ROWLEN * 1 + 22 } },

        { "teeth1", new List<int>() { 24, ROWLEN * 1 + 24 } },
        { "teeth2", new List<int>() { 26, ROWLEN * 1 + 26 } },
        { "teeth3", new List<int>() { 28, ROWLEN * 1 + 28 } },

        { "blood1", new List<int>() { ROWLEN * 1 + 14 } },
        { "blood2", new List<int>() { ROWLEN * 1 + 16 } },
        { "blood3", new List<int>() { ROWLEN * 1 + 18 } },
                };

                return portraitOffsets;
            }
        }

        public struct HONDA
        {
            public static Dictionary<string, List<int>> GenerateHondaSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "hair2", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "skin7", new List<int>() { 14 } },
            { "hair1", new List<int>() { 16 } },
            { "costume1", new List<int>() { 18 } },
            { "costume2", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume4", new List<int>() { 24 } },
            { "costume5", new List<int>() { 26 } },
            { "facepaint", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateHondaPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, ROWLEN * 2 + 24, } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6 } },

        { "hair1", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8 } },
        { "hair2", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10 } },
        { "hair3", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12 } },

        { "facepaint1", new List<int>() { 14, ROWLEN * 1 + 14, ROWLEN * 2 + 14, ROWLEN * 3 + 14 } },
        { "facepaint2", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 2 + 16, ROWLEN * 3 + 16, ROWLEN * 3 + 22 } },
        { "facepaint3", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 2 + 18, ROWLEN * 3 + 18 } },

        { "mouth1", new List<int>() { 20, ROWLEN * 3 + 20 } },
        { "mouth2", new List<int>() { 22 } },

        { "facepaintloss1", new List<int>() { ROWLEN * 1 + 20, ROWLEN * 2 + 20 } },
        { "facepaintloss2", new List<int>() { ROWLEN * 1 + 22, ROWLEN * 2 + 22 } },

        { "teeth1", new List<int>() { 24, ROWLEN * 1 + 24,  ROWLEN * 3 + 24 } },
        { "teeth2", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
        { "teeth3", new List<int>() { 28, ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },

        { "facepaintloss3", new List<int>() { ROWLEN * 1 + 28 } },

                };

                return portraitOffsets;
            }
        }

        public struct ZANGIEF
        {
            public static Dictionary<string, List<int>> GenerateZangiefSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "hair3", new List<int>() { 0 } },
            { "hair2", new List<int>() { 2 } },
            { "skin5", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin2", new List<int>() { 8 } },
            { "skin1", new List<int>() { 10 } },
            { "skin3", new List<int>() { 12 } },
            { "costume3", new List<int>() { 14 } },
            { "costume2", new List<int>() { 16 } },
            { "costume1", new List<int>() { 18 } },
            { "belt2", new List<int>() { 20 } },
            { "belt1", new List<int>() { 22 } },
            { "costume4", new List<int>() { 24 } },
            { "hair1", new List<int>() { 26 } },
            { "belt3", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateZangiefPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0 } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2 } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4 } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6 } },

        { "hair1", new List<int>() { 8, ROWLEN * 1 + 8 } },
        { "hair2", new List<int>() { 10, ROWLEN * 1 + 10 } },
        { "hair3", new List<int>() { ROWLEN * 1 + 12, 12 } }, // jab gief has diff values here, use strong as baseline and 

        { "costume1", new List<int>() { 14 } },
        { "costume2", new List<int>() { 16 } },
        { "costume3", new List<int>() { 18 } },
        { "costume4", new List<int>() { 20 } },
        { "costume5", new List<int>() { 22 } },

        { "blood3", new List<int>() { 28, ROWLEN * 1 + 28 } },
        { "blood1", new List<int>() { 24, ROWLEN * 1 + 24 } },
        { "blood2", new List<int>() { 26, ROWLEN * 1 + 26 } },

        { "eyes1", new List<int>() { ROWLEN * 1 + 14 } },
        { "eyes2", new List<int>() { ROWLEN * 1 + 16 } },
        { "eyes3", new List<int>() { ROWLEN * 1 + 18 } },
        { "eyes4", new List<int>() { ROWLEN * 1 + 20 } },
        { "eyes5", new List<int>() { ROWLEN * 1 + 22 } },

                };

                return portraitOffsets;
            }
        }

        public struct BOXER
        {
            public static Dictionary<string, List<int>> GenerateBoxerSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "gloves3", new List<int>() { 0 } },
            { "skin6", new List<int>() { 2 } },
            { "skin5", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin3", new List<int>() { 8 } },
            { "skin2", new List<int>() { 10 } },
            { "skin1", new List<int>() { 12 } },
            { "gloves2", new List<int>() { 14 } },
            { "gloves1", new List<int>() { 16 } },
            { "costume5", new List<int>() { 18 } },
            { "costume4", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume1", new List<int>() { 26 } },
            { "shine", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateBoxerPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "teeth1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, 14 } },
        { "skin1", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 3 + 2 } },
        { "skin2", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 3 + 4 } },
        { "skin3", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 3 + 6 } },
        { "skin4", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 3 + 8 } },
        { "skin5", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 3 + 10 } },
        { "skin6", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 3 + 12 } },

        { "teeth2", new List<int>() { 16 } },
        { "teeth3", new List<int>() { 18 } },
        { "teeth4", new List<int>() { 20, ROWLEN * 3 + 22 } },

        { "bruise1", new List<int>() { 22 } },
        { "bruise2", new List<int>() { 24 } },
        { "bruise3", new List<int>() { 26 } },
        { "bruise4", new List<int>() { 28 } },

        { "costume1", new List<int>() { ROWLEN * 1 + 14, ROWLEN * 2 + 14, ROWLEN * 3 + 14 } },
        { "costume2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 2 + 16, ROWLEN * 3 + 16 } },
        { "costume3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 2 + 18, ROWLEN * 3 + 18 } },
        { "costume4", new List<int>() { ROWLEN * 1 + 20, ROWLEN * 2 + 20, ROWLEN * 3 + 20 } },

        { "gloves7", new List<int>() { ROWLEN * 1 + 22, ROWLEN * 2 + 22 } },
        { "gloves8", new List<int>() { ROWLEN * 1 + 24, ROWLEN * 2 + 24 } },
        { "gloves9", new List<int>() { ROWLEN * 1 + 26, ROWLEN * 2 + 26 } },
        { "gloves10", new List<int>() { ROWLEN * 1 + 28, ROWLEN * 2 + 28 } },

        { "blood1", new List<int>() { ROWLEN * 3 + 24 } },
        { "blood2", new List<int>() { ROWLEN * 3 + 26 } },
        { "blood3", new List<int>() { ROWLEN * 3 + 28 } },

        { "gloves1", new List<int>() { ROWLEN * 2 + 2 } },
        { "gloves2", new List<int>() { ROWLEN * 2 + 4 } },
        { "gloves3", new List<int>() { ROWLEN * 2 + 6 } },
        { "gloves4", new List<int>() { ROWLEN * 2 + 8 } },
        { "gloves5", new List<int>() { ROWLEN * 2 + 10 } },
        { "gloves6", new List<int>() { ROWLEN * 2 + 12 } },
                };

                return portraitOffsets;
            }
        }

        public struct CHUN
        {
            public static Dictionary<string, List<int>> GenerateChunSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "hair5", new List<int>() { 0 } },
            { "skin2", new List<int>() { 2 } },
            { "skin3", new List<int>() { 4 } },
            { "skin4", new List<int>() { 6 } },
            { "skin5", new List<int>() { 8 } },
            { "hair1", new List<int>() { 10 } },
            { "hair2", new List<int>() { 12 } },
            { "hair3", new List<int>() { 14 } },
            { "hair4", new List<int>() { 16 } },
            { "costume5", new List<int>() { 18 } },
            { "costume4", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume1", new List<int>() { 26 } },
            { "skin1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateChunPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "lips1", new List<int>() { 0, ROWLEN * 1 + 0 } },
        { "skin1", new List<int>() { 2, ROWLEN * 1 + 2 } },
        { "skin2", new List<int>() { 4, ROWLEN * 1 + 4 } },
        { "hair1", new List<int>() { 6, ROWLEN * 1 + 6 } },
        { "hair2", new List<int>() { 8, ROWLEN * 1 + 8 } },
        { "hair3", new List<int>() { 10, ROWLEN * 1 + 10 } },
        { "hair4", new List<int>() { 12, ROWLEN * 1 + 12 } },
        { "costume1", new List<int>() { 14 } },
        { "costume2", new List<int>() { 16 } },
        { "costume3", new List<int>() { 18 } },
        { "costume4", new List<int>() { 20 } },
        { "costume5", new List<int>() { 22 } },
        { "lips4", new List<int>() { 24, ROWLEN * 1 + 24 } },
        { "lips3", new List<int>() { 26, ROWLEN * 1 + 26 } },
        { "lips2", new List<int>() { 28, ROWLEN * 1 + 28 } },
        { "bruise1", new List<int>() { ROWLEN * 1 + 14 } },
        { "bruise2", new List<int>() { ROWLEN * 1 + 16 } },                };

                return portraitOffsets;
            }
        }

        public struct RYU
        {
            public static Dictionary<string, List<int>> GenerateRyuSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "belt", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "hair1", new List<int>() { 10 } },
            { "hair2", new List<int>() { 12 } },
            { "headband2", new List<int>() { 14} },
            { "costume1", new List<int>() { 16 } },
            { "costume2", new List<int>() { 18 } },
            { "costume3", new List<int>() { 20 } },
            { "costume4", new List<int>() { 22 } },
            { "costume5", new List<int>() { 24 } },
            { "costume6", new List<int>() { 26 } },
            { "headband1", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateRyuPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2, } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4, } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6, } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8, } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10, } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12, } },
        { "eyes1", new List<int>() { 14 } },
        { "eyes2", new List<int>() { 16 } },
        { "eyes3", new List<int>() { 18 } },
        { "headband1", new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 2 + 18 } },
        { "headband2", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 2 + 20 } },
        { "headband3", new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 2 + 22 } },
        { "costume1", new List<int>() { ROWLEN * 1 + 14, ROWLEN * 3 + 14 } },
        { "costume2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 3 + 16, 20 } }, // fix jab color encoding
        { "costume3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 3 + 18, 22 } },
        { "costume4", new List<int>() { ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
        { "costume5", new List<int>() { ROWLEN * 1 + 22, ROWLEN * 3 + 22 } },
        { "blood1", new List<int>() { ROWLEN * 2 + 24, ROWLEN * 3 + 24 } },
        { "blood2", new List<int>() { ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
        { "blood3", new List<int>() { ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },
        { "teeth1", new List<int>() { ROWLEN * 2 + 14 } },
        { "teeth2", new List<int>() { ROWLEN * 2 + 16 } },
                };

                return portraitOffsets;
            }
        }

        public struct KEN
        {
            public static Dictionary<string, List<int>> GenerateKenSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "belt", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "hair1", new List<int>() { 14} },
            { "costume1", new List<int>() { 16 } },
            { "costume2", new List<int>() { 18 } },
            { "costume3", new List<int>() { 20 } },
            { "costume4", new List<int>() { 22 } },
            { "costume5", new List<int>() { 24 } },
            { "costume6", new List<int>() { 26 } },
            { "hair2", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateKenPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
                {
        { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, } },
        { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2, } },
        { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4, } },
        { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6, } },
        { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8, } },
        { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10, } },
        { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12, } },

        { "costume1", new List<int>() { 14 } },
        { "costume2", new List<int>() { 16 } },
        { "costume3", new List<int>() { 18 } },
        { "costume4", new List<int>() { 20 } },

        { "hair1", new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22 } },
        { "hair2", new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24 } },
        { "hair3", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
        { "hair4", new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },

        { "teeth1", new List<int>() { ROWLEN * 1 + 14, ROWLEN * 2 + 14 } },
        { "teeth2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 2 + 16 } },
        { "teeth3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 2 + 18 } },

        { "blood1", new List<int>() { ROWLEN * 2 + 20, ROWLEN * 3 + 14 } },
        { "blood2", new List<int>() { ROWLEN * 2 + 22, ROWLEN * 3 + 16 } },
        { "blood3", new List<int>() { ROWLEN * 2 + 24, ROWLEN * 3 + 18 } },

                };

                return portraitOffsets;
            }
        }

        public struct GUILE
        {
            public static Dictionary<string, List<int>> GenerateGuileSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "darkcamo2", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "costume1", new List<int>() { 12 } },
            { "costume2", new List<int>() { 14} },
            { "costume3", new List<int>() { 16 } },
            { "costume4", new List<int>() { 18 } },
            { "flag1", new List<int>() { 20 } },
            { "flag2", new List<int>() { 22 } },
            { "hair", new List<int>() { 24 } },
            { "darkcamo1", new List<int>() { 26 } },
            { "costume5", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateGuilePortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
        {
            { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, } },
            { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2, } },
            { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4, } },
            { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6, } },
            { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8, } },
            { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10, } },
            { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12, } },
            { "chain1", new List<int>() { 14, ROWLEN * 3 + 14, } },
            { "chain2", new List<int>() { 16, ROWLEN * 3 + 16, } },
            { "chain3", new List<int>() { 18, ROWLEN * 3 + 18, } },
            { "chain4", new List<int>() { 20, ROWLEN * 3 + 20, } },
            { "chain5", new List<int>() { 22, ROWLEN * 3 + 22, } },
            { "shirt1", new List<int>() { 24 } },
            { "shirt2", new List<int>() { 26 } },
            { "shirt3", new List<int>() { 28 } },
            { "hair1", new List<int>() { ROWLEN * 1 + 14 } },
            { "hair2", new List<int>() { ROWLEN * 1 + 16 } },
            { "hair3", new List<int>() { ROWLEN * 1 + 18 } },
            { "hair4", new List<int>() { ROWLEN * 1 + 20 } },
            { "hair5", new List<int>() { ROWLEN * 1 + 22 } },
            { "bruise1", new List<int>() { ROWLEN * 2 + 14 } },
            { "bruise2", new List<int>() { ROWLEN * 2 + 16 } },
            { "bruise3", new List<int>() { ROWLEN * 2 + 18 } },
            { "bruise4", new List<int>() { ROWLEN * 2 + 20 } },
            { "bruise5", new List<int>() { ROWLEN * 2 + 22 } },
            { "blood1", new List<int>() { ROWLEN * 1 + 24, ROWLEN * 2 + 24, ROWLEN * 3 + 24 } },
            { "blood2", new List<int>() { ROWLEN * 1 + 26, ROWLEN * 2 + 26, ROWLEN * 3 + 26 } },
            { "blood3", new List<int>() { ROWLEN * 1 + 28, ROWLEN * 2 + 28, ROWLEN * 3 + 28 } },
        };
                return portraitOffsets;
            }
        }

        public struct CLAW
        {
            public static Dictionary<string, List<int>> GenerateClawSpriteOffsets()
            {
                Dictionary<string, List<int>> spriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "outline", new List<int>() { 0 } },
            { "skin1", new List<int>() { 2 } },
            { "skin2", new List<int>() { 4 } },
            { "skin3", new List<int>() { 6 } },
            { "skin4", new List<int>() { 8 } },
            { "skin5", new List<int>() { 10 } },
            { "skin6", new List<int>() { 12 } },
            { "skin7", new List<int>() { 14} },
            { "stripe", new List<int>() { 16 } },
            { "costume1", new List<int>() { 18 } },
            { "costume2", new List<int>() { 20 } },
            { "costume3", new List<int>() { 22 } },
            { "costume4", new List<int>() { 24 } },
            { "sash1", new List<int>() { 26 } },
            { "sash2", new List<int>() { 28 } },
           };
                return spriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateClawPortraitOffsets()
            {
                Dictionary<string, List<int>> portraitOffsets = new Dictionary<string, List<int>>
        {
            { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, } },
            { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2, } },
            { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4, } },
            { "hair1", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6, } },
            { "hair2", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8, } },
            { "hair3", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10, } },
            { "hair4", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12, } },
            { "metal1", new List<int>() { 14, ROWLEN * 1 + 14, ROWLEN * 2 + 14, ROWLEN * 3 + 14, } },
            { "metal2", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 2 + 16, ROWLEN * 3 + 16, } },
            { "metal3", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 2 + 18, ROWLEN * 3 + 18, } },
            { "metal4", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 2 + 20, ROWLEN * 3 + 20, } },
            { "metal5", new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 2 + 22, ROWLEN * 3 + 22, } },
            { "costume1", new List<int>() { 24 } },
            { "costume2", new List<int>() { 26 } },
            { "costume3", new List<int>() { 28 } },
            { "iris", new List<int>() { ROWLEN * 1 + 28 } },
            { "blood1", new List<int>() { ROWLEN * 3 + 24 } },
            { "blood2", new List<int>() { ROWLEN * 3 + 26 } },
            { "blood3", new List<int>() { ROWLEN * 3 + 28 } },
        };
                return portraitOffsets;
            }
        }

        public struct DICTATOR
        {
            public static PaletteConfig GenerateDictatorSpriteConfig()
            {
                int MEMLEN = 160;
                var dictatorSpriteOffsets = GenerateDictatorSpriteOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = dictatorSpriteOffsets;
                pc.unusedOffsets = new List<int>() { 66, 67, 92, 93 };
                string defaults = "0007 2302 3403 5605 6706 7807 8A08 9B09";
                int defaultsOffset = 32;
                pc.createColorOffsets(defaults, defaultsOffset);
                pc.streamLength = MEMLEN;
                return pc;
            }

            public static Dictionary<string, List<int>> GenerateDictatorSpriteOffsets()
            {
                Dictionary<string, List<int>> dictatorSpriteOffsets =
                   new Dictionary<string, List<int>>
           {
            { "pads5", new List<int>() { 0, ROWLEN * 3 + 0, ROWLEN * 4 + 0 } },
            { "costume5", new List<int>() { 2, ROWLEN * 3 + 2, ROWLEN * 4 + 2 } },
            { "costume4", new List<int>() { 4, ROWLEN * 3 + 4, ROWLEN * 4 + 4 } },
            { "costume3", new List<int>() { 6, ROWLEN * 3 + 6, ROWLEN * 4 + 6 } },
            { "costume2", new List<int>() { 8, ROWLEN * 3 + 8, ROWLEN * 4 + 8 } },
            { "costume1", new List<int>() { 10, ROWLEN * 3 + 10, ROWLEN * 4 + 10 } },
            { "pads4", new List<int>() { 12 } },
            { "stripe", new List<int>() { 14, ROWLEN * 3 + 14 } },
            { "pads1", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
            { "pads2", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
            { "pads3", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
            { "skin1", new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22, ROWLEN * 4 + 22 } },
            { "skin2", new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24, ROWLEN * 4 + 24 } },
            { "skin3", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 3 + 26, ROWLEN * 4 + 26 } },
            { "skin4", new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 3 + 28, ROWLEN * 4 + 28 } },

            { "crusherpads5", new List<int>() { ROWLEN * 2 + 0 } },
            { "crushercostume4", new List<int>() { ROWLEN * 2 + 4 } },
            { "crushercostume3", new List<int>() { ROWLEN * 2 + 6 } },
            { "crushercostume2", new List<int>() { ROWLEN * 2 + 8 } },
            { "crushercostume1", new List<int>() { ROWLEN * 2 + 10 } },
            { "crusherpads4", new List<int>() { ROWLEN * 2 + 12 } },
            { "crusherflame1", new List<int>() { ROWLEN * 2 + 14 } },
            { "crusherpads1", new List<int>() { ROWLEN * 2 + 16 } },
            { "crusherpads2", new List<int>() { ROWLEN * 2 + 18 } },
            { "crusherpads3", new List<int>() { ROWLEN * 2 + 20 } },
            { "crusherflame2", new List<int>() { ROWLEN * 2 + 22 } },
            { "crusherhands1", new List<int>() { ROWLEN * 2 + 24 } },
            { "crusherhands2", new List<int>() { ROWLEN * 2 + 26 } },
            { "psychoglow", new List<int>() { ROWLEN * 3 + 12 } },
            { "psychopunch1", new List<int>() { ROWLEN * 4 + 12 } },
            { "psychopunch2", new List<int>() { ROWLEN * 4 + 14 } },
            { "psychopunch3", new List<int>() { ROWLEN * 4 + 16 } },
            { "psychopunch4", new List<int>() { ROWLEN * 4 + 18 } },
            { "psychopunch5", new List<int>() { ROWLEN * 4 + 20 } },
           };
                return dictatorSpriteOffsets;
            }

            public static Dictionary<string, List<int>> GenerateDictatorPortraitOffsets()
            {
                Dictionary<string, List<int>> dictatorPortraitOffsets = new Dictionary<string, List<int>>
        {
            { "skin1", new List<int>() { 0, ROWLEN * 1 + 0, ROWLEN * 2 + 0, ROWLEN * 3 + 0, } },
            { "skin2", new List<int>() { 2, ROWLEN * 1 + 2, ROWLEN * 2 + 2, ROWLEN * 3 + 2, } },
            { "skin3", new List<int>() { 4, ROWLEN * 1 + 4, ROWLEN * 2 + 4, ROWLEN * 3 + 4, } },
            { "skin4", new List<int>() { 6, ROWLEN * 1 + 6, ROWLEN * 2 + 6, ROWLEN * 3 + 6, } },
            { "skin5", new List<int>() { 8, ROWLEN * 1 + 8, ROWLEN * 2 + 8, ROWLEN * 3 + 8, } },
            { "skin6", new List<int>() { 10, ROWLEN * 1 + 10, ROWLEN * 2 + 10, ROWLEN * 3 + 10, } },
            { "skin7", new List<int>() { 12, ROWLEN * 1 + 12, ROWLEN * 2 + 12, ROWLEN * 3 + 12, } },
            { "piping1", new List<int>() { 14, ROWLEN * 1 + 14 } },
            { "piping2", new List<int>() { 16, ROWLEN * 1 + 16 } },
            { "piping3", new List<int>() { 18, ROWLEN * 1 + 18 } },
            { "piping4", new List<int>() { 20, ROWLEN * 1 + 20 } },
            { "costume1", new List<int>() { 22 } },
            { "costume2", new List<int>() { 24 } },
            { "costume3", new List<int>() { 26 } },
            { "costume4", new List<int>() { 28 } },
            { "teeth1", new List<int>() { ROWLEN * 1 + 22, ROWLEN * 2 + 22 } },
            { "teeth2", new List<int>() { ROWLEN * 1 + 24, ROWLEN * 2 + 24 } },
            { "teeth3", new List<int>() { ROWLEN * 1 + 26, ROWLEN * 2 + 26 } },
            { "teeth4", new List<int>() { ROWLEN * 1 + 28, ROWLEN * 2 + 28 } },
            { "blood1", new List<int>() { ROWLEN * 2 + 14 } },
            { "blood2", new List<int>() { ROWLEN * 2 + 16 } },
            { "blood3", new List<int>() { ROWLEN * 2 + 18 } },
            { "pipingloss1", new List<int>() { ROWLEN * 3 + 14 } },
            { "pipingloss2", new List<int>() { ROWLEN * 3 + 16 } },
            { "pipingloss3", new List<int>() { ROWLEN * 3 + 18 } },
            { "pipingloss4", new List<int>() { ROWLEN * 3 + 20 } },
            { "costumeloss1", new List<int>() { ROWLEN * 3 + 22 } },
            { "costumeloss2", new List<int>() { ROWLEN * 3 + 24 } },
            { "costumeloss3", new List<int>() { ROWLEN * 3 + 26 } },
            { "costumeloss4", new List<int>() { ROWLEN * 3 + 28 } },
        };
                return dictatorPortraitOffsets;
            }
        }
    }

    public struct ImageConfig
    {
        public static PaletteImage GenerateNeutralBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.GUI_neutral2),
PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Guile, CharacterConfig.BUTTONS.hp)),
PaletteConfig.GUILE.GenerateGuileSpriteOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CLA_neutral7),
PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold)),

PaletteConfig.CLAW.GenerateClawSpriteOffsets());
                case CharacterConfig.CHARACTERS.Boxer:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BOX_neutral0),
PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Boxer, CharacterConfig.BUTTONS.lp)),
PaletteConfig.BOXER.GenerateBoxerSpriteOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.SPRITE.GenerateDictatorStandingNeutralBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.RYU_neutral2),
    PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Ryu, CharacterConfig.BUTTONS.hp)),
PaletteConfig.RYU.GenerateRyuSpriteOffsets());
                case CharacterConfig.CHARACTERS.Ken:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.KEN_neutral0),
    PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Ken, CharacterConfig.BUTTONS.lp)),
PaletteConfig.KEN.GenerateKenSpriteOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CHU_neutral1),
PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Chun, CharacterConfig.BUTTONS.mp)),
    PaletteConfig.CHUN.GenerateChunSpriteOffsets());
                case CharacterConfig.CHARACTERS.Zangief:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.ZAN_neutral1), 
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Zangief, CharacterConfig.BUTTONS.mp)), 
                        PaletteConfig.ZANGIEF.GenerateZangiefSpriteOffsets());
                case CharacterConfig.CHARACTERS.Ehonda:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.EHO_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.HONDA.GenerateHondaSpriteOffsets());
                case CharacterConfig.CHARACTERS.Sagat:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.SAG_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Sagat, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.SAGAT.GenerateSagatSpriteOffsets());
                case CharacterConfig.CHARACTERS.Feilong:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.FEI_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Feilong, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.FEI.GenerateFeiSpriteOffsets());
                case CharacterConfig.CHARACTERS.Deejay:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DEE_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Deejay, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DEEJAY.GenerateDeejaySpriteOffsets());
                case CharacterConfig.CHARACTERS.Dhalsim:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DHA_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DHALSIM.GenerateDhalsimSpriteOffsets());
                case CharacterConfig.CHARACTERS.Cammy:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CAM_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Cammy, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.CAMMY.GenerateCammySpriteOffsets());
                case CharacterConfig.CHARACTERS.Thawk:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.THA_neutral2),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Thawk, CharacterConfig.BUTTONS.hp)),
                        PaletteConfig.HAWK.GenerateHawkSpriteOffsets());
                case CharacterConfig.CHARACTERS.Blanka:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BLA_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Blanka, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.BLANKA.GenerateBlankaSpriteOffsets());
                case CharacterConfig.CHARACTERS.Gouki:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.GOU_neutral0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Gouki, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.GOUKI.GenerateGoukiSpriteOffsets());
            }
            throw new Exception("Invalid character");
        }

        public static PaletteImage GenerateVictoryBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.GUI_portraitwin2),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Guile, CharacterConfig.BUTTONS.hp)),
PaletteConfig.GUILE.GenerateGuilePortraitOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CLA_portraitwin7),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold)),
PaletteConfig.CLAW.GenerateClawPortraitOffsets());
                case CharacterConfig.CHARACTERS.Boxer:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BOX_portraitwin0),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Boxer, CharacterConfig.BUTTONS.lp)),
PaletteConfig.BOXER.GenerateBoxerPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.PORTRAIT.GenerateDictatorVictoryBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.RYU_portraitwin2),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ryu, CharacterConfig.BUTTONS.hp)),
    PaletteConfig.RYU.GenerateRyuPortraitOffsets());
                case CharacterConfig.CHARACTERS.Ken:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.KEN_portraitwin0),
    PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ken, CharacterConfig.BUTTONS.lp)),
PaletteConfig.KEN.GenerateKenPortraitOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CHU_portraitwin1),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Chun, CharacterConfig.BUTTONS.mp)),
    PaletteConfig.CHUN.GenerateChunPortraitOffsets());
                case CharacterConfig.CHARACTERS.Zangief:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.ZAN_portraitwin1),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Zangief, CharacterConfig.BUTTONS.mp)),
                        PaletteConfig.ZANGIEF.GenerateZangiefPortraitOffsets());
                case CharacterConfig.CHARACTERS.Ehonda:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.EHO_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.HONDA.GenerateHondaPortraitOffsets());
                case CharacterConfig.CHARACTERS.Sagat:
                    return Sagat.PORTRAIT.GenerateSagatVictoryBasePaletteImage();
                case CharacterConfig.CHARACTERS.Feilong:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.FEI_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Feilong, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.FEI.GenerateFeiPortraitOffsets());
                case CharacterConfig.CHARACTERS.Deejay:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DEE_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Deejay, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DEEJAY.GenerateDeejayPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dhalsim:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DHA_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DHALSIM.GenerateDhalsimPortraitOffsets());
                case CharacterConfig.CHARACTERS.Cammy:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CAM_portraitwinX),
                       Cammy.PORTRAIT.GenerateCammyCustomXColor(),
                       PaletteConfig.CAMMY.GenerateCammyPortraitOffsets());
                case CharacterConfig.CHARACTERS.Thawk:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.THA_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Thawk, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.HAWK.GenerateHawkPortraitOffsets());
                case CharacterConfig.CHARACTERS.Blanka:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BLA_portraitwin0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Blanka, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.BLANKA.GenerateBlankaPortraitOffsets());
                case CharacterConfig.CHARACTERS.Gouki:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.GOU_portraitwinX),
                        GOUKI.PORTRAIT.GenerateGoukiCustomXColor(),
                        PaletteConfig.GOUKI.GenerateGoukiPortraitOffsets());
            }
            throw new Exception("Invalid character");
        }

        public static PaletteImage GenerateLossBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.GUI_portraitloss2),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Guile, CharacterConfig.BUTTONS.hp)),
PaletteConfig.GUILE.GenerateGuilePortraitOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CLA_portraitloss7),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold)),
PaletteConfig.CLAW.GenerateClawPortraitOffsets());
                case CharacterConfig.CHARACTERS.Boxer:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BOX_portraitloss0),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Boxer, CharacterConfig.BUTTONS.lp)),
PaletteConfig.BOXER.GenerateBoxerPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.PORTRAIT.GenerateDictatorLossBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.RYU_portraitloss2),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ryu, CharacterConfig.BUTTONS.hp)),
PaletteConfig.RYU.GenerateRyuPortraitOffsets());
                case CharacterConfig.CHARACTERS.Ken:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.KEN_portraitloss0),
    PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ken, CharacterConfig.BUTTONS.lp)),
PaletteConfig.KEN.GenerateKenPortraitOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CHU_portraitloss1),
PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Chun, CharacterConfig.BUTTONS.mp)),
    PaletteConfig.CHUN.GenerateChunPortraitOffsets());
                case CharacterConfig.CHARACTERS.Zangief:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.ZAN_portraitloss1),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Zangief, CharacterConfig.BUTTONS.mp)),
                        PaletteConfig.ZANGIEF.GenerateZangiefPortraitOffsets());
                case CharacterConfig.CHARACTERS.Ehonda:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.EHO_portraitloss0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.HONDA.GenerateHondaPortraitOffsets());
                case CharacterConfig.CHARACTERS.Sagat:
                    return Sagat.PORTRAIT.GenerateSagatLossBasePaletteImage();
                case CharacterConfig.CHARACTERS.Feilong:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.FEI_portraitloss0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Feilong, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.FEI.GenerateFeiPortraitOffsets());
                case CharacterConfig.CHARACTERS.Deejay:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DEE_portraitloss0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Deejay, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DEEJAY.GenerateDeejayPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dhalsim:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.DHA_portraitloss0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.DHALSIM.GenerateDhalsimPortraitOffsets());
                case CharacterConfig.CHARACTERS.Cammy:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.CAM_portraitlossX),
                       Cammy.PORTRAIT.GenerateCammyCustomXColor(),
                        PaletteConfig.CAMMY.GenerateCammyPortraitOffsets());
                case CharacterConfig.CHARACTERS.Thawk:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.THA_portraitlossX),
                       Hawk.PORTRAIT.GenerateHawkCustomXColor(),
                        PaletteConfig.HAWK.GenerateHawkPortraitOffsets());
                case CharacterConfig.CHARACTERS.Blanka:
                    return GeneratePaletteImageFromOffsets(new Bitmap(Properties.Resources.BLA_portraitloss0),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Blanka, CharacterConfig.BUTTONS.lp)),
                        PaletteConfig.BLANKA.GenerateBlankaPortraitOffsets());
            }
            throw new Exception("Invalid character");
        }

        public static PaletteImage GeneratePaletteImage(Bitmap base_image, string resource, List<string> labels, Dictionary<string, List<int>> offsets)
        {
            byte[] byte_stream = PaletteHelper.StringToByteStream(resource);
            Color[] c = PaletteHelper.ColorsFromLabelsAndStream(byte_stream,
                offsets,
                labels);
            PaletteImage p = new PaletteImage(base_image, c);
            p.labels = labels;
            return p;
        }

        public static PaletteImage GeneratePaletteImageFromOffsets(Bitmap base_image, string resource, Dictionary<string, List<int>> offsets)
        {
            List<string> labels = new List<string>();
            foreach (var k in offsets)
            {
                labels.Add(k.Key);
            }

            return GeneratePaletteImage(base_image, resource, labels, offsets);
        }

        public struct Dictator
        {
            public struct SPRITE
            {
                public static List<string> DictatorStandNeutralLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4",
                "stripe", "pads1", "pads2", "pads3", "pads4", "pads5",
                "costume1", "costume2", "costume3", "costume4", "costume5"};
                }

                public static List<string> DictatorPsychoPunchLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4",
                "stripe", "pads1", "pads2", "pads3", "pads4", "pads5",
                "costume1", "costume2", "costume3", "costume4", "costume5",
                "psychopunch1", "psychopunch2", "psychopunch3", "psychopunch4", "psychopunch5"};
                }

                public static List<string> DictatorPsychoPrepLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4",
                "stripe", "pads1", "pads2", "pads3", "pads4", "pads5",
                "costume1", "costume2", "costume3", "costume4", "costume5",
                "psychoglow"};
                }

                public static List<string> DictatorPsychoCrusherLabels()
                {
                    return new List<string> { "crushercostume1", "crushercostume2", "crushercostume3", "crushercostume4",
                "crusherflame1", "crusherflame2", "crusherhands1", "crusherhands2",
                "crusherpads1", "crusherpads2", "crusherpads3", "crusherpads4", "crusherpads5" };
                }

                public static Bitmap DictatorStandNeutralBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_neutral1);
                }

                public static Bitmap DictatorPsychoPunchBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_psychopunch1);
                }

                public static Bitmap DictatorPsychoPrepBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_psychoprep5);
                }

                public static Bitmap DictatorCrusherTopBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_crushertop5);
                }

                public static Bitmap DictatorCrusherBottomBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_crusherbottom5);
                }

                public static PaletteImage GenerateDictatorStandingNeutralBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorStandNeutralBaseImage(), 
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.mp)), 
                        DictatorStandNeutralLabels());
                }

                public static PaletteImage GenerateDictatorPsychoPunchBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorPsychoPunchBaseImage(),
PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.mp)),
                                                DictatorPsychoPunchLabels());
                }

                public static PaletteImage GenerateDictatorPsychoPrepBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorPsychoPrepBaseImage(),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk)),
                        DictatorPsychoPrepLabels());
                }

                public static PaletteImage GenerateDictatorCrusherTopBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorCrusherTopBaseImage(),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk)),
                        DictatorPsychoCrusherLabels());
                }

                public static PaletteImage GenerateDictatorCrusherBottomBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorCrusherBottomBaseImage(),
                        PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk)),
                        DictatorPsychoCrusherLabels());
                }

                public static PaletteImage GenerateDicatatorPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.DICTATOR.GenerateDictatorSpriteOffsets());
                }
            }

            public struct PORTRAIT
            {
                public static List<string> DictatorVictoryPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4", "skin5", "skin6", "skin7",
                "costume1", "costume2", "costume3", "costume4",
                "teeth1", "teeth2", "teeth3", "teeth4",
                "piping1", "piping2", "piping3", "piping4" };
                }

                public static List<string> DictatorLossPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4", "skin5", "skin6", "skin7",
                "teeth1", "teeth2", "teeth3", "teeth4",
                "blood1", "blood2", "blood3",
                "costumeloss1", "costumeloss2", "costumeloss3", "costumeloss4",
                "pipingloss1", "pipingloss2", "pipingloss3", "pipingloss4",
                    "costume1", "costume2", "costume3", "costume4",
                "piping1", "piping2", "piping3", "piping4",};
                }

                public static Bitmap DictatorVictoryPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_portraitwin5);
                }

                public static Bitmap DictatorLossPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_portraitloss0);
                }

                public static PaletteImage GenerateDictatorVictoryBasePaletteImage()
                {
                    return GenerateDicatatorPortraitPaletteImage(DictatorVictoryPortraitBaseImage(),
                                                PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk)),
DictatorVictoryPortraitLabels());
                }

                public static PaletteImage GenerateDictatorLossBasePaletteImage()
                {
                    return GenerateDicatatorPortraitPaletteImage(DictatorLossPortraitBaseImage(),
                                                PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.lp)),
                        DictatorLossPortraitLabels());
                }

                public static PaletteImage GenerateDicatatorPortraitPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.DICTATOR.GenerateDictatorPortraitOffsets());
                }
            }
        }

        public struct Sagat
        {
            public struct PORTRAIT
            {
                public static List<string> SagatVictoryPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4", "skin5", "skin6", "skin7",
                "wraps1", "wraps2", "wraps3",
                "blood1", "blood2", "blood3", "blood4", "blood5", "blood6",
                "bruise1", "bruise2", "bruise3",
                "scars1", "scars2", "scars3",
                };
                }

                public static List<string> SagatLossPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4", "skin5", "skin6", "skin7",
                "teeth1", "teeth2", "teeth3",
                "blood1", "blood2", "blood3", "blood4", "blood5", "blood6",
                "bruise1", "bruise2", "bruise3",
                "scars1", "scars2", "scars3",
                };
                }

                public static PaletteImage GenerateSagatVictoryBasePaletteImage()
                {
                    return GenerateSagatPortraitPaletteImage(Properties.Resources.SAG_portraitwin0,
                                                PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Sagat, CharacterConfig.BUTTONS.lp)),
    SagatVictoryPortraitLabels());
                }

                public static PaletteImage GenerateSagatLossBasePaletteImage()
                {
                    return GenerateSagatPortraitPaletteImage(Properties.Resources.SAG_portraitloss0,
                                                PaletteHelper.ByteStreamToString(CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Sagat, CharacterConfig.BUTTONS.lp)),
    SagatLossPortraitLabels());
                }

                public static PaletteImage GenerateSagatPortraitPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.SAGAT.GenerateSagatPortraitOffsets());
                }
            }
        }

        public struct Cammy
        {
            public struct PORTRAIT
            {
                // Cammy's portrait has two dinstinct whites: her hair and her eyes.
                // We create a custom portrait from her jab where the eyes are FF00FF
                public static string GenerateCammyCustomXColor()
                {
                    byte[] stream = CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Cammy, CharacterConfig.BUTTONS.lp);
                    PaletteConfig portraitConfig = PaletteConfig.GeneratePortraitConfig(CharacterConfig.CHARACTERS.Cammy);
                    Palette p = Palette.PaletteFromConfig(portraitConfig);
                    p.LoadStream(stream);
                    p.SetColor("eyes1", Color.FromArgb(255, 255, 0, 255));
                    var stream2 = p.ToByteStream();
                    return PaletteHelper.ByteStreamToString(stream2);
                }
            }
        }

        public struct Hawk
        {
            public struct PORTRAIT
            {
                // Hawk's portrait has two dinstinct whites: his paint and his teeth.
                // We create a custom portrait from her jab where the teeth are FF00FF
                public static string GenerateHawkCustomXColor()
                {
                    byte[] stream = CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Thawk, CharacterConfig.BUTTONS.lp);
                    PaletteConfig portraitConfig = PaletteConfig.GeneratePortraitConfig(CharacterConfig.CHARACTERS.Thawk);
                    Palette p = Palette.PaletteFromConfig(portraitConfig);
                    p.LoadStream(stream);
                    p.SetColor("teeth1", Color.FromArgb(255, 255, 0, 255));
                    var stream2 = p.ToByteStream();
                    return PaletteHelper.ByteStreamToString(stream2);
                }
            }
        }

        public struct GOUKI
        {
            public struct PORTRAIT
            {
                // Gouki's portrait in rom is just all 0's. We need to create a default.
                public static string GenerateGoukiCustomXColor()
                {
                    //                    byte[] stream = CharacterConfig.GetPortraitResourceFromRom(CharacterConfig.CHARACTERS.Gouki, CharacterConfig.BUTTONS.lp);
                    string s = "A101 A202 A303 A404 A505 A606 A707 A808 1502 3704 4805 6907 9B0A CD0C 0F0F 000F " +
                               "110A 220A 330A 440A 550A 660A 770A 880A 990A AA0A BB0A CC0A DD0A EE0A FF0A FB00 " +
                               "1A01 2A02 3A03 4A04 5A05 6A06 7A07 8A08 9A09 AC0A BA0B CA0C DA0D EA0D 5842 5C00 " +
                               "5101 5201 5301 5401 5501 5601 5701 5801 5901 5A01 5B01 5C01 5D01 5E01 CC0C 1501 " +
                               "E359 AD33 6A2C 62D9 6786 FC7A 5122 EA84 FC8D 4C88 2F68 8B5A 7011 A64A CCD9 4F71";
                    byte[] stream = PaletteHelper.StringToByteStream(s);
                    PaletteConfig portraitConfig = PaletteConfig.GeneratePortraitConfig(CharacterConfig.CHARACTERS.Gouki);
                    Palette p = Palette.PaletteFromConfig(portraitConfig);
                    p.LoadStream(stream);
                    var stream2 = p.ToByteStream();
                    return PaletteHelper.ByteStreamToString(stream2);
                }

            }
        }
    }
}
