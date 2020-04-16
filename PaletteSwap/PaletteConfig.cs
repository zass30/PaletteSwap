using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

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
        public enum CHARACTERS { Dictator, Claw, Guile, Ryu, Chun };

        public static ByteStreamPair GetByteStreamPair(CHARACTERS c, BUTTONS b)
        {
            byte[] sprite_bytestream = new byte[0];
            byte[] portrait_bytestream = new byte[0];
            switch (c) {
                case CHARACTERS.Claw:
                    switch (b)
            {
                case CharacterConfig.BUTTONS.lp:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla0sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla0portrait);
                    break;
                case CharacterConfig.BUTTONS.mp:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla1sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla1portrait);
                    break;
                case CharacterConfig.BUTTONS.hp:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla2sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla2portrait);
                    break;
                case CharacterConfig.BUTTONS.lk:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla3sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla3portrait);
                    break;
                case CharacterConfig.BUTTONS.mk:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla4sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla4portrait);
                    break;
                case CharacterConfig.BUTTONS.hk:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla5sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla5portrait);
                    break;
                case CharacterConfig.BUTTONS.start:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla6sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla6portrait);
                    break;
                case CharacterConfig.BUTTONS.hold:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla7sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla7portrait);
                    break;
                case CharacterConfig.BUTTONS.old1:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla8sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla8portrait);
                    break;
                case CharacterConfig.BUTTONS.old2:
                    sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla9sprite);
                    portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.cla9portrait);
                    break;
            }
                    break;
                case CHARACTERS.Dictator:
                    switch (b)
                    {
                        case CharacterConfig.BUTTONS.lp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis0sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis0portrait);
                            break;
                        case CharacterConfig.BUTTONS.mp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis1sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis1portrait);
                            break;
                        case CharacterConfig.BUTTONS.hp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis2sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis2portrait);
                            break;
                        case CharacterConfig.BUTTONS.lk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis3sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis3portrait);
                            break;
                        case CharacterConfig.BUTTONS.mk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis4sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis4portrait);
                            break;
                        case CharacterConfig.BUTTONS.hk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis5sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis5portrait);
                            break;
                        case CharacterConfig.BUTTONS.start:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis6sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis6portrait);
                            break;
                        case CharacterConfig.BUTTONS.hold:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis7sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis7portrait);
                            break;
                        case CharacterConfig.BUTTONS.old1:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis8sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis8portrait);
                            break;
                        case CharacterConfig.BUTTONS.old2:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis9sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.bis9portrait);
                            break;
                    }
                    break;
                case CHARACTERS.Guile:
                    switch (b)
                    {
                        case CharacterConfig.BUTTONS.lp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui0sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui0portrait);
                            break;
                        case CharacterConfig.BUTTONS.mp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui1sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui1portrait);
                            break;
                        case CharacterConfig.BUTTONS.hp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui2sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui2portrait);
                            break;
                        case CharacterConfig.BUTTONS.lk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui3sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui3portrait);
                            break;
                        case CharacterConfig.BUTTONS.mk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui4sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui4portrait);
                            break;
                        case CharacterConfig.BUTTONS.hk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui5sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui5portrait);
                            break;
                        case CharacterConfig.BUTTONS.start:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui6sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui6portrait);
                            break;
                        case CharacterConfig.BUTTONS.hold:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui7sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui7portrait);
                            break;
                        case CharacterConfig.BUTTONS.old1:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui8sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui8portrait);
                            break;
                        case CharacterConfig.BUTTONS.old2:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui9sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.gui9portrait);
                            break;
                    }
                    break;
                case CHARACTERS.Ryu:
                    switch (b)
                    {
                        case CharacterConfig.BUTTONS.lp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu0sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu0portrait);
                            break;
                        case CharacterConfig.BUTTONS.mp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu1sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu1portrait);
                            break;
                        case CharacterConfig.BUTTONS.hp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu2sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu2portrait);
                            break;
                        case CharacterConfig.BUTTONS.lk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu3sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu3portrait);
                            break;
                        case CharacterConfig.BUTTONS.mk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu4sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu4portrait);
                            break;
                        case CharacterConfig.BUTTONS.hk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu5sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu5portrait);
                            break;
                        case CharacterConfig.BUTTONS.start:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu6sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu6portrait);
                            break;
                        case CharacterConfig.BUTTONS.hold:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu7sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu7portrait);
                            break;
                        case CharacterConfig.BUTTONS.old1:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu8sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu8portrait);
                            break;
                        case CharacterConfig.BUTTONS.old2:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu9sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.ryu9portrait);
                            break;
                    }
                    break;
                case CHARACTERS.Chun:
                    switch (b)
                    {
                        case CharacterConfig.BUTTONS.lp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu0sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu0portrait);
                            break;
                        case CharacterConfig.BUTTONS.mp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu1sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu1portrait);
                            break;
                        case CharacterConfig.BUTTONS.hp:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu2sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu2portrait);
                            break;
                        case CharacterConfig.BUTTONS.lk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu3sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu3portrait);
                            break;
                        case CharacterConfig.BUTTONS.mk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu4sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu4portrait);
                            break;
                        case CharacterConfig.BUTTONS.hk:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu5sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu5portrait);
                            break;
                        case CharacterConfig.BUTTONS.start:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu6sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu6portrait);
                            break;
                        case CharacterConfig.BUTTONS.hold:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu7sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu7portrait);
                            break;
                        case CharacterConfig.BUTTONS.old1:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu8sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu8portrait);
                            break;
                        case CharacterConfig.BUTTONS.old2:
                            sprite_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu9sprite);
                            portrait_bytestream = PaletteHelper.StringToByteStream(Properties.Resources.chu9portrait);
                            break;
                    }
                    break;
            }

            ByteStreamPair p = new ByteStreamPair();
            p.spriteStream = sprite_bytestream;
            p.portraitStream = portrait_bytestream;
            return p;
        }
        
        public static string CodeFromCharacterEnum(CHARACTERS c)
        {
            switch (c)
            {
                case CHARACTERS.Dictator:
                    return "DIC";
                case CHARACTERS.Claw:
                    return "CLA";
                case CHARACTERS.Guile:
                    return "GUI";
                case CHARACTERS.Ryu:
                    return "RYU";
                case CHARACTERS.Chun:
                    return "CHU";
            }
            throw new ArgumentException("Invalid Character type");
        }
        
        public static CHARACTERS CharacterEnumFromCode(string s)
        {
            switch (s)
            {
                case "CLA":
                    return CHARACTERS.Claw;
                case "DIC":
                    return CHARACTERS.Dictator;
                case "GUI":
                    return CHARACTERS.Guile;
                case "RYU":
                    return CHARACTERS.Ryu;
                case "CHU":
                    return CHARACTERS.Chun;
            }
            throw new ArgumentException("Invalid Character type");
        }

        public static int GetCharIdFromCharacter(CHARACTERS c)
        {
            switch (c)
            {
                case CHARACTERS.Ryu:
                    return 0;
                case CHARACTERS.Guile:
                    return 3;
                case CHARACTERS.Chun:
                    return 5;
                case CHARACTERS.Dictator:
                    return 8;
                case CHARACTERS.Claw:
                    return 11;
            }
            return 0;
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
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU.GenerateRyuSpriteConfig();
                case CharacterConfig.CHARACTERS.Chun:
                    return CHUN.GenerateChunSpriteConfig();
                case CharacterConfig.CHARACTERS.Guile:
                    return GUILE.GenerateGuileSpriteConfig();
                case CharacterConfig.CHARACTERS.Claw:
                    return CLAW.GenerateClawSpriteConfig();
                case CharacterConfig.CHARACTERS.Dictator:
                    return DICTATOR.GenerateDictatorSpriteConfig();

            }
            throw new Exception("Invalid character");
        }

        public static PaletteConfig GeneratePortraitConfig(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU.GenerateRyuPortraitConfig();
                case CharacterConfig.CHARACTERS.Chun:
                    return CHUN.GenerateRyuPortraitConfig();
                case CharacterConfig.CHARACTERS.Guile:
                    return GUILE.GenerateGuilePortraitConfig();
                case CharacterConfig.CHARACTERS.Claw:
                    return CLAW.GenerateClawPortraitConfig();
                case CharacterConfig.CHARACTERS.Dictator:
                    return DICTATOR.GenerateDictatorPortraitConfig();

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

            public static PaletteConfig GenerateChunSpriteConfig()
            {
                int MEMLEN = 30;
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = GenerateChunSpriteOffsets();
                pc.unusedOffsets = new List<int>();
                pc.defaultColorOffsets = new List<ColorOffset>();
                pc.streamLength = MEMLEN;
                return pc;
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
        { "costume2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
        { "costume3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
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

            public static PaletteConfig GenerateRyuPortraitConfig()
            {
                int MEMLEN = ROWLEN * 4;
                List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
                for (int i = 0; i < 4; i++)
                {
                    ColorOffset dco = new ColorOffset();
                    dco.c = PaletteHelper.MemFormatToColor("0A00");
                    dco.position = 30 + ROWLEN * i;
                    defaultColorOffsets.Add(dco);
                }

                Dictionary<string, List<int>> guilePortraitOffsets = GenerateRyuPortraitOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = guilePortraitOffsets;
                string defaults = "F00F F00F";//just a test to flush out unknown spot
                int defaultsOffset = 20;
                pc.createColorOffsets(defaults, defaultsOffset);
                //                pc.defaultColorOffsets = defaultColorOffsets;
                pc.unusedOffsets = new List<int>() { };
                pc.streamLength = MEMLEN;
                return pc;
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

            public static PaletteConfig GenerateRyuSpriteConfig()
            {
                int MEMLEN = 30;
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = GenerateRyuSpriteOffsets();
                pc.unusedOffsets = new List<int>();
                pc.defaultColorOffsets = new List<ColorOffset>();
                pc.streamLength = MEMLEN;
                return pc;
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
        { "costume2", new List<int>() { ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
        { "costume3", new List<int>() { ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
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

            public static PaletteConfig GenerateRyuPortraitConfig()
            {
                int MEMLEN = ROWLEN * 4;
                List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
                for (int i = 0; i < 4; i++)
                {
                    ColorOffset dco = new ColorOffset();
                    dco.c = PaletteHelper.MemFormatToColor("0A00");
                    dco.position = 30 + ROWLEN * i;
                    defaultColorOffsets.Add(dco);
                }

                Dictionary<string, List<int>> guilePortraitOffsets = GenerateRyuPortraitOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = guilePortraitOffsets;
                string defaults = "F00F F00F";//just a test to flush out unknown spot
                int defaultsOffset = 20;
                pc.createColorOffsets(defaults, defaultsOffset);
//                pc.defaultColorOffsets = defaultColorOffsets;
                pc.unusedOffsets = new List<int>() { };
                pc.streamLength = MEMLEN;
                return pc;
            }
        }

        public struct GUILE
        {
            public static PaletteConfig GenerateGuileSpriteConfig()
            {
                int MEMLEN = 30;
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = GenerateGuileSpriteOffsets();
                pc.unusedOffsets = new List<int>();
                pc.defaultColorOffsets = new List<ColorOffset>();
                pc.streamLength = MEMLEN;
                return pc;
            }

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

            public static PaletteConfig GenerateGuilePortraitConfig()
            {
                int MEMLEN = ROWLEN * 4;
                List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
                for (int i = 0; i < 4; i++)
                {
                    ColorOffset dco = new ColorOffset();
                    dco.c = PaletteHelper.MemFormatToColor("9900");
                    dco.position = 30 + ROWLEN * i;
                    defaultColorOffsets.Add(dco);
                }

                Dictionary<string, List<int>> guilePortraitOffsets = GenerateGuilePortraitOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = guilePortraitOffsets;
                pc.defaultColorOffsets = defaultColorOffsets;
                pc.unusedOffsets = new List<int>() { };
                pc.streamLength = MEMLEN;
                return pc;
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
            public static PaletteConfig GenerateClawSpriteConfig()
            {
                int MEMLEN = 30;
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = GenerateClawSpriteOffsets();
                pc.unusedOffsets = new List<int>();
                pc.defaultColorOffsets = new List<ColorOffset>();
                pc.streamLength = MEMLEN;
                return pc;
            }

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

            public static PaletteConfig GenerateClawPortraitConfig()
            {
                int MEMLEN = ROWLEN * 4;
                List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
                for (int i = 0; i < 4; i++)
                {
                    ColorOffset dco = new ColorOffset();
                    dco.c = PaletteHelper.MemFormatToColor("0A00");
                    dco.position = 30 + ROWLEN * i;
                    defaultColorOffsets.Add(dco);
                }

                Dictionary<string, List<int>> clawPortraitOffsets = GenerateClawPortraitOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = clawPortraitOffsets;
                pc.defaultColorOffsets = defaultColorOffsets;
                pc.unusedOffsets = new List<int>() { };
                pc.streamLength = MEMLEN;
                return pc;
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

            public static PaletteConfig GenerateDictatorPortraitConfig()
            {
                int MEMLEN = ROWLEN * 4;
                List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
                for (int i = 0; i < 4; i++)
                {
                    ColorOffset dco = new ColorOffset();
                    dco.c = PaletteHelper.MemFormatToColor("0A00");
                    dco.position = 30 + ROWLEN * i;
                    defaultColorOffsets.Add(dco);
                }
                ColorOffset co = new ColorOffset();
                co.c = PaletteHelper.MemFormatToColor("0008");
                co.position = 20 + ROWLEN * 2;
                defaultColorOffsets.Add(co);
                Dictionary<string, List<int>> dictatorPortraitOffsets = GenerateDictatorPortraitOffsets();
                PaletteConfig pc = new PaletteConfig();
                pc.labelOffsets = dictatorPortraitOffsets;
                pc.defaultColorOffsets = defaultColorOffsets;
                pc.unusedOffsets = new List<int>() { };
                pc.streamLength = MEMLEN;
                return pc;
            }
        }
    }

    // a character config has
    // neutral sprite base image Properties.Resources.RYU_neutral2
    // neutral sprite colors Properties.Resources.ryu2sprite
    // list of keys from sprite
    // victory base image
    // victory sprite colors
    // loss base image
    // loss sprite colors
    // list of keys from portrait

    public struct ImageConfig
    {
        public static PaletteImage GenerateNeutralBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.GUI_neutral2),
PaletteSwap.Properties.Resources.gui2sprite, PaletteConfig.GUILE.GenerateGuileSpriteOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.clawneutral7),
PaletteSwap.Properties.Resources.cla7sprite, PaletteConfig.CLAW.GenerateClawSpriteOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.SPRITE.GenerateDictatorStandingNeutralBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.RYU_neutral2),
    PaletteSwap.Properties.Resources.ryu2sprite, PaletteConfig.RYU.GenerateRyuSpriteOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.CHU_neutral1),
    PaletteSwap.Properties.Resources.chu1sprite, PaletteConfig.CHUN.GenerateChunSpriteOffsets());
            }
            throw new Exception("Invalid character");
        }

        public static PaletteImage GenerateVictoryBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.GUI_portraitwin2),
PaletteSwap.Properties.Resources.gui2portrait, PaletteConfig.GUILE.GenerateGuilePortraitOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.CLA_portraitwin7),
PaletteSwap.Properties.Resources.cla7portrait, PaletteConfig.CLAW.GenerateClawPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.PORTRAIT.GenerateDictatorVictoryBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.RYU_portraitwin2),
    PaletteSwap.Properties.Resources.ryu2portrait, PaletteConfig.RYU.GenerateRyuPortraitOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.CHU_portraitwin1),
    PaletteSwap.Properties.Resources.chu1portrait, PaletteConfig.RYU.GenerateRyuPortraitOffsets());
            }
            throw new Exception("Invalid character");
        }

        public static PaletteImage GenerateLossBasePalette(CharacterConfig.CHARACTERS c)
        {
            switch (c)
            {
                case CharacterConfig.CHARACTERS.Guile:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.GUI_portraitloss2),
PaletteSwap.Properties.Resources.gui2portrait, PaletteConfig.GUILE.GenerateGuilePortraitOffsets());
                case CharacterConfig.CHARACTERS.Claw:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.CLA_portraitloss7),
PaletteSwap.Properties.Resources.cla7portrait, PaletteConfig.CLAW.GenerateClawPortraitOffsets());
                case CharacterConfig.CHARACTERS.Dictator:
                    return Dictator.PORTRAIT.GenerateDictatorLossBasePaletteImage();
                case CharacterConfig.CHARACTERS.Ryu:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.RYU_portraitloss2),
PaletteSwap.Properties.Resources.ryu2portrait, PaletteConfig.RYU.GenerateRyuPortraitOffsets());
                case CharacterConfig.CHARACTERS.Chun:
                    return GeneratePaletteImage2(new Bitmap(Properties.Resources.CHU_portraitloss1),
    PaletteSwap.Properties.Resources.chu1portrait, PaletteConfig.RYU.GenerateRyuPortraitOffsets());
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

        public static PaletteImage GeneratePaletteImage2(Bitmap base_image, string resource, Dictionary<string, List<int>> offsets)
        {
            List<string> labels = new List<string>();
            foreach (var k in offsets)
            {
                labels.Add(k.Key);
            }
            byte[] byte_stream = PaletteHelper.StringToByteStream(resource);
            Color[] c = PaletteHelper.ColorsFromLabelsAndStream(byte_stream,
                offsets,
                labels);
            PaletteImage p = new PaletteImage(base_image, c);
            p.labels = labels;
            return p;
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
                    return new Bitmap(Properties.Resources.dicstand1);
                }

                public static Bitmap DictatorPsychoPunchBaseImage()
                {
                    return new Bitmap(Properties.Resources.dicmp1);
                }

                public static Bitmap DictatorPsychoPrepBaseImage()
                {
                    return new Bitmap(Properties.Resources.dicpsychoprep5);
                }

                public static Bitmap DictatorCrusherTopBaseImage()
                {
                    return new Bitmap(Properties.Resources.diccrusher1_5);
                }

                public static Bitmap DictatorCrusherBottomBaseImage()
                {
                    return new Bitmap(Properties.Resources.diccrusher2_5);
                }

                public static PaletteImage GenerateDictatorStandingNeutralBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorStandNeutralBaseImage(), PaletteSwap.Properties.Resources.bis1sprite, DictatorStandNeutralLabels());
                }

                public static PaletteImage GenerateDictatorPsychoPunchBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorPsychoPunchBaseImage(), PaletteSwap.Properties.Resources.bis1sprite, DictatorPsychoPunchLabels());
                }

                public static PaletteImage GenerateDictatorPsychoPrepBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorPsychoPrepBaseImage(), PaletteSwap.Properties.Resources.bis5sprite, DictatorPsychoPrepLabels());
                }

                public static PaletteImage GenerateDictatorCrusherTopBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorCrusherTopBaseImage(), PaletteSwap.Properties.Resources.bis5sprite, DictatorPsychoCrusherLabels());
                }

                public static PaletteImage GenerateDictatorCrusherBottomBasePaletteImage()
                {
                    return GenerateDicatatorPaletteImage(DictatorCrusherBottomBaseImage(), PaletteSwap.Properties.Resources.bis5sprite, DictatorPsychoCrusherLabels());
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
                    return new Bitmap(Properties.Resources.dicportraitwin5);
                }

                public static Bitmap DictatorLossPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.DIC_portraitloss0);
                }

                public static PaletteImage GenerateDictatorVictoryBasePaletteImage()
                {
                    return GenerateDicatatorPortraitPaletteImage(DictatorVictoryPortraitBaseImage(), PaletteSwap.Properties.Resources.bis5portrait, DictatorVictoryPortraitLabels());
                }

                public static PaletteImage GenerateDictatorLossBasePaletteImage()
                {
                    return GenerateDicatatorPortraitPaletteImage(DictatorLossPortraitBaseImage(), PaletteSwap.Properties.Resources.bis0portrait, DictatorLossPortraitLabels());
                }

                public static PaletteImage GenerateDicatatorPortraitPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.DICTATOR.GenerateDictatorPortraitOffsets());
                }
            }
        }
    }
}
