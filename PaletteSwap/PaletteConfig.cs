using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace PaletteSwap
{
    // new character config that contains
    // addresses in rom for sprites/portraits

    public struct CharacterConfig
    {
        static public int spriteBeginOffset = 0x3FB1E;
        static public int spriteStep = 0x66c;
        static public int spriteColorLength = 0xA2;
        static public int portrait1BeginOffset = 0x31C48;
        static public int portrait2BeginOffset = 0x36CFE;
        static public int portraitStep = 0x500;
        static public int portraitColorLength = 0x80;
        public enum BUTTONS { lp, mp, hp, lk, mk, hk, start, hold, old1, old2 };
        public enum CHARACTERS { Dictator, Claw, Guile };

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
            }
            throw new ArgumentException("Invalid Character type");
        }

        public static int GetCharIdFromCharacter(CHARACTERS c)
        {
            switch (c)
            {
                case CHARACTERS.Guile:
                    return 3;
                case CHARACTERS.Dictator:
                    return 8;
                case CHARACTERS.Claw:
                    return 11;
            }
            return 0;
        }

        public static int GetSpriteBeginOffset(CHARACTERS c)
        {
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
                //int ROWLEN = 32;
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
               // int ROWLEN = 32;
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
               // int ROWLEN = 32;
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
               // int ROWLEN = 32;
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
                //int ROWLEN = 32;
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
               // int ROWLEN = 32;
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
               // int ROWLEN = 32;
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

    public struct ImageConfig
    {
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

        public struct GUILE
        {
            public struct SPRITE {
                public static List<string> GuileStandNeutralLabels()
                {
                    return new List<string> { "darkcamo1", "darkcamo2",
                    "skin1", "skin2", "skin3", "skin4", "skin5",
                "costume1", "costume2", "costume3", "costume4", "costume5",
                    "flag1", "flag2", "hair" };
                }


                public static Bitmap GuileStandNeutralBaseImage()
                {
                    return new Bitmap(Properties.Resources.GUI_neutral2);
                }

                public static PaletteImage GenerateGuileStandingNeutralBasePaletteImage()
                {
                    return GenerateGuilePaletteImage(GuileStandNeutralBaseImage(), PaletteSwap.Properties.Resources.gui2sprite, GuileStandNeutralLabels());
                }

                public static PaletteImage GenerateGuilePaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.GUILE.GenerateGuileSpriteOffsets());
                }
            }

            public struct PORTRAIT
            {
                public static List<string> GuilePortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3", "skin4",
                        "skin5", "skin6", "skin7",
                        "chain1", "chain2", "chain3", "chain4", "chain5",
                        "shirt1","shirt2", "shirt3",
                        "hair1", "hair2", "hair3", "hair4", "hair5",
                        "bruise1", "bruise2", "bruise3", "bruise4", "bruise5",
                "blood1", "blood2", "blood3", };
                }                

                public static Bitmap GuileVictoryPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.GUI_portraitwin2);
                }

                public static Bitmap GuileLossPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.GUI_portraitloss2);
                }

                public static PaletteImage GenerateGuileVictoryBasePaletteImage()
                {
                    return GenerateGuilePortraitPaletteImage(GuileVictoryPortraitBaseImage(), PaletteSwap.Properties.Resources.gui2portrait, GuilePortraitLabels());
                }

                public static PaletteImage GenerateGuileLossBasePaletteImage()
                {
                    return GenerateGuilePortraitPaletteImage(GuileLossPortraitBaseImage(), PaletteSwap.Properties.Resources.gui2portrait, GuilePortraitLabels());
                }

                public static PaletteImage GenerateGuilePortraitPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.GUILE.GenerateGuilePortraitOffsets());
                }
            }
        }

        public struct CLAW
        {
            public struct SPRITE
            {
                public static List<string> ClawStandNeutralLabels()
                {
                    return new List<string> { "outline", "skin1", "skin2", "skin3", "skin4",
                "skin5", "skin6", "skin7", "stripe", 
                "costume1", "costume2", "costume3", "costume4", "sash1", "sash2" };
                }

                public static Bitmap ClawStandNeutralBaseImage()
                {
                    return new Bitmap(Properties.Resources.clawneutral7);
                }

                public static PaletteImage GenerateClawStandingNeutralBasePaletteImage()
                {
                    return GenerateClawPaletteImage(ClawStandNeutralBaseImage(), PaletteSwap.Properties.Resources.cla7sprite, ClawStandNeutralLabels());
                }

                public static PaletteImage GenerateClawPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.CLAW.GenerateClawSpriteOffsets());
                }
            }

            public struct PORTRAIT
            {
                public static List<string> ClawVictoryPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3",
                        "hair1", "hair2", "hair3", "hair4",
                        "metal1", "metal2", "metal3", "metal4", "metal5",
                "costume1", "costume2", "costume3", "iris", };
                }

                public static List<string> ClawLossPortraitLabels()
                {
                    return new List<string> { "skin1", "skin2", "skin3",
                        "hair1", "hair2", "hair3", "hair4",
                        "metal1", "metal2", "metal3", "metal4", "metal5",
                "costume1", "costume2", "costume3", 
                "blood1", "blood2", "blood3" };
                }


                public static Bitmap ClawVictoryPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.CLA_portraitwin7);
                }

                public static Bitmap ClawLossPortraitBaseImage()
                {
                    return new Bitmap(Properties.Resources.CLA_portraitloss7);
                }

                public static PaletteImage GenerateClawVictoryBasePaletteImage()
                {
                    return GenerateClawPortraitPaletteImage(ClawVictoryPortraitBaseImage(), PaletteSwap.Properties.Resources.cla7portrait, ClawVictoryPortraitLabels());
                }

                public static PaletteImage GenerateClawLossBasePaletteImage()
                {
                    return GenerateClawPortraitPaletteImage(ClawLossPortraitBaseImage(), PaletteSwap.Properties.Resources.cla7portrait, ClawLossPortraitLabels());
                }

                public static PaletteImage GenerateClawPortraitPaletteImage(Bitmap base_image, string resource, List<string> labels)
                {
                    return GeneratePaletteImage(base_image, resource, labels, PaletteConfig.CLAW.GenerateClawPortraitOffsets());
                }
            }
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
