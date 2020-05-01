using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;

namespace PaletteSwap
{
    public partial class MainForm : Form
    {
        bool DISABLE_PATCHING = false;
        ZoomForm z;
        ColorSetForm c;
        public string currentlyZoomedLabel;
        public CharacterConfig.CHARACTERS currentCharacterType;
        PictureBox previouslySelectedSquare;
        PictureBox currentlySelectedColor;
        public GameSet gameSet = new GameSet();
        public CharacterSet characterSet;
        public Character currentCharacter;
        bool skip_image_recolors = false;
        int DEFAULT_DROPDOWN_INDEX = 0;
        enum ROMSTYLE { us, japanese, phoenix };
        Regex rx = new Regex(@"[^_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        CharacterConfig.CHARACTERS[] supportedCharacters = new CharacterConfig.CHARACTERS[] { CharacterConfig.CHARACTERS.Dictator, CharacterConfig.CHARACTERS.Claw,
                CharacterConfig.CHARACTERS.Guile, CharacterConfig.CHARACTERS.Ryu, CharacterConfig.CHARACTERS.Chun, CharacterConfig.CHARACTERS.Boxer, CharacterConfig.CHARACTERS.Ken,
                CharacterConfig.CHARACTERS.Zangief, CharacterConfig.CHARACTERS.Ehonda};

        public MainForm()
        {
            InitializeComponent();
            CreateCharacterSet();
            EnableDragAndDrop();
            SetDefaultDropDown();
            loadSpritesAndPalettesFromDropDown();
            CreateZoomAndColorForms();
        }

        public void CreateCharacterSet()
        {
            foreach (CharacterConfig.CHARACTERS character in supportedCharacters)
            {
                gameSet.characterDictionary[character] = new CharacterSet(character);
            }

            currentCharacterType = CharacterConfig.CHARACTERS.Dictator;
            characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Dictator];
        }


        public void SetDefaultDropDown()
        {
            colorSelectorBox.SelectedIndex = DEFAULT_DROPDOWN_INDEX;
        }

        public void CreateZoomAndColorForms()
        {
            z = new ZoomForm(this);
            c = new ColorSetForm(this);
        }

        private void EnableDragAndDrop()
        {
            // drag and drop functionality
            label1.DragDrop += new DragEventHandler(label1_DragDrop);
            label1.DragEnter += new DragEventHandler(label1_DragEnter);
            COLlabel.DragDrop += new DragEventHandler(COLlabel_DragDrop);
            COLlabel.DragEnter += new DragEventHandler(COLlabel_DragEnter);
        }

        private void loadSpritesAndPalettesFromDropDown()
        {
            resetCurrentCharacterColorFromDropDown();
            reload_everything();
        }

        private void reload_everything()
        {
            skip_image_recolors = true;
            load_portrait_buttons();
            load_sprite_buttons();
            load_sprite_neutralstand();
            load_portrait_victory();
            load_portrait_loss();
            if (currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                load_sprite_load_sprite_psychopunch();
                load_sprite_load_sprite_psychoprep();
                load_sprite_load_sprite_crushertop();
                load_sprite_load_sprite_crusherbottom();
            }

            skip_image_recolors = false;
        }

        private void load_sprite_neutralstand()
        {
            var n = currentCharacter.GetBitmap("neutral");
            var p = GetSpriteNeutralBox();
            p.BackgroundImage = n;
        }

        private void load_sprite_load_sprite_psychopunch()
        {
            psychopunchBox.BackgroundImage = currentCharacter.sprite.GetBitmap("psychopunch");
        }

        private void load_sprite_load_sprite_psychoprep()
        {
            psychoprepBox.BackgroundImage = currentCharacter.sprite.GetBitmap("psychoprep");
        }

        private void load_sprite_load_sprite_crushertop()
        {
            crushertopBox.BackgroundImage = currentCharacter.sprite.GetBitmap("crushertop");
        }

        private void load_sprite_load_sprite_crusherbottom()
        {
            crusherbottomBox.BackgroundImage = currentCharacter.sprite.GetBitmap("crusherbottom");
        }

        private void load_portrait_victory()
        {
            var v = currentCharacter.GetBitmap("victory");
            var p = GetPortraitVictoryBox();
            p.BackgroundImage = v;
        }

        private void load_portrait_loss()
        {
            var l = currentCharacter.GetBitmap("loss");
            var p = GetPortraitLossBox();
            p.BackgroundImage = l;
        }

        private PictureBox GetSpriteNeutralBox()
        {
            // todo dynamic generation
            switch (currentCharacterType)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU_neutralStandBox;
                case CharacterConfig.CHARACTERS.Ken:
                    return KEN_neutralStandBox;
                case CharacterConfig.CHARACTERS.Ehonda:
                    return EHO_neutralStandBox;
                case CharacterConfig.CHARACTERS.Zangief:
                    return ZAN_neutralStandBox;
                case CharacterConfig.CHARACTERS.Chun:
                    return CHU_neutralStandBox;
                case CharacterConfig.CHARACTERS.Guile:
                    return GUI_neutralStandBox;
                case CharacterConfig.CHARACTERS.Boxer:
                    return BOX_neutralStandBox;
                case CharacterConfig.CHARACTERS.Claw:
                    return CLA_neutralStandBox;
                case CharacterConfig.CHARACTERS.Dictator:
                    return DIC_neutralStandBox;
            }
            throw new Exception("Invalid character");
        }

        private PictureBox GetPortraitLossBox()
        {
            switch (currentCharacterType)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU_portraitLossBox;
                case CharacterConfig.CHARACTERS.Ken:
                    return KEN_portraitLossBox;
                case CharacterConfig.CHARACTERS.Ehonda:
                    return EHO_portraitLossBox;
                case CharacterConfig.CHARACTERS.Zangief:
                    return ZAN_portraitLossBox;
                case CharacterConfig.CHARACTERS.Chun:
                    return CHU_portraitLossBox;
                case CharacterConfig.CHARACTERS.Guile:
                    return GUI_portraitLossBox;
                case CharacterConfig.CHARACTERS.Boxer:
                    return BOX_portraitLossBox;
                case CharacterConfig.CHARACTERS.Claw:
                    return CLA_portraitLossBox;
                case CharacterConfig.CHARACTERS.Dictator:
                    return DIC_portraitLossBox;
            }
            throw new Exception("Invalid character");
        }

        private PictureBox GetPortraitVictoryBox()
        {
            switch (currentCharacterType)
            {
                case CharacterConfig.CHARACTERS.Ryu:
                    return RYU_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Ken:
                    return KEN_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Ehonda:
                    return EHO_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Zangief:
                    return ZAN_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Chun:
                    return CHU_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Guile:
                    return GUI_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Boxer:
                    return BOX_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Claw:
                    return CLA_portraitVictoryBox;
                case CharacterConfig.CHARACTERS.Dictator:
                    return DIC_portraitVictoryBox;
            }
            throw new Exception("Invalid character");
        }

        private void load_sprite_buttons()
        {
            switch (currentCharacterType)
            {
                case CharacterConfig.CHARACTERS.Dictator:
                    DIC_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    DIC_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    DIC_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    DIC_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");

                    pal_sprite_stripe.BackColor = currentCharacter.sprite.GetColor("stripe");

                    DIC_sprite_pads1.BackColor = currentCharacter.sprite.GetColor("pads1");
                    pal_sprite_pads2.BackColor = currentCharacter.sprite.GetColor("pads2");
                    pal_sprite_pads3.BackColor = currentCharacter.sprite.GetColor("pads3");
                    pal_sprite_pads4.BackColor = currentCharacter.sprite.GetColor("pads4");
                    pal_sprite_pads5.BackColor = currentCharacter.sprite.GetColor("pads5");

                    pal_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    pal_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    pal_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    pal_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    pal_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");

                    pal_sprite_psychoglow.BackColor = currentCharacter.sprite.GetColor("psychoglow");

                    pal_sprite_psychopunch1.BackColor = currentCharacter.sprite.GetColor("psychopunch1");
                    pal_sprite_psychopunch2.BackColor = currentCharacter.sprite.GetColor("psychopunch2");
                    pal_sprite_psychopunch3.BackColor = currentCharacter.sprite.GetColor("psychopunch3");
                    pal_sprite_psychopunch4.BackColor = currentCharacter.sprite.GetColor("psychopunch4");
                    pal_sprite_psychopunch5.BackColor = currentCharacter.sprite.GetColor("psychopunch5");

                    pal_sprite_crusherpads1.BackColor = currentCharacter.sprite.GetColor("crusherpads1");
                    pal_sprite_crusherpads2.BackColor = currentCharacter.sprite.GetColor("crusherpads2");
                    pal_sprite_crusherpads3.BackColor = currentCharacter.sprite.GetColor("crusherpads3");
                    pal_sprite_crusherpads4.BackColor = currentCharacter.sprite.GetColor("crusherpads4");
                    pal_sprite_crusherpads5.BackColor = currentCharacter.sprite.GetColor("crusherpads5");

                    pal_sprite_crushercostume1.BackColor = currentCharacter.sprite.GetColor("crushercostume1");
                    pal_sprite_crushercostume2.BackColor = currentCharacter.sprite.GetColor("crushercostume2");
                    pal_sprite_crushercostume3.BackColor = currentCharacter.sprite.GetColor("crushercostume3");
                    pal_sprite_crushercostume4.BackColor = currentCharacter.sprite.GetColor("crushercostume4");

                    pal_sprite_crusherflame1.BackColor = currentCharacter.sprite.GetColor("crusherflame1");
                    pal_sprite_crusherflame2.BackColor = currentCharacter.sprite.GetColor("crusherflame2");

                    pal_sprite_crusherhands1.BackColor = currentCharacter.sprite.GetColor("crusherhands1");
                    pal_sprite_crusherhands2.BackColor = currentCharacter.sprite.GetColor("crusherhands2");

                    break;
                case CharacterConfig.CHARACTERS.Claw:
                    sprite_claw_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    sprite_claw_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    sprite_claw_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    sprite_claw_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    sprite_claw_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");
                    sprite_claw_skin6.BackColor = currentCharacter.sprite.GetColor("skin6");
                    sprite_claw_skin7.BackColor = currentCharacter.sprite.GetColor("skin7");

                    sprite_claw_sash1.BackColor = currentCharacter.sprite.GetColor("sash1");
                    sprite_claw_sash2.BackColor = currentCharacter.sprite.GetColor("sash2");

                    sprite_claw_stripe.BackColor = currentCharacter.sprite.GetColor("stripe");
                    sprite_claw_outline.BackColor = currentCharacter.sprite.GetColor("outline");

                    sprite_claw_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    sprite_claw_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    sprite_claw_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    sprite_claw_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    break;
                case CharacterConfig.CHARACTERS.Boxer:
                    BOX_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    BOX_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    BOX_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    BOX_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    BOX_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");
                    BOX_sprite_skin6.BackColor = currentCharacter.sprite.GetColor("skin6");

                    BOX_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    BOX_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    BOX_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    BOX_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    BOX_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");

                    BOX_sprite_gloves1.BackColor = currentCharacter.sprite.GetColor("gloves1");
                    BOX_sprite_gloves2.BackColor = currentCharacter.sprite.GetColor("gloves2");
                    BOX_sprite_gloves3.BackColor = currentCharacter.sprite.GetColor("gloves3");

                    BOX_sprite_shine.BackColor = currentCharacter.sprite.GetColor("shine");
                    break;
                case CharacterConfig.CHARACTERS.Guile:
                    GUI_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    GUI_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    GUI_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    GUI_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    GUI_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");
                    GUI_sprite_darkcamo1.BackColor = currentCharacter.sprite.GetColor("darkcamo1");
                    GUI_sprite_darkcamo2.BackColor = currentCharacter.sprite.GetColor("darkcamo2");

                    GUI_sprite_flag1.BackColor = currentCharacter.sprite.GetColor("flag1");
                    GUI_sprite_flag2.BackColor = currentCharacter.sprite.GetColor("flag2");

                    GUI_sprite_hair.BackColor = currentCharacter.sprite.GetColor("hair");

                    GUI_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    GUI_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    GUI_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    GUI_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    GUI_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");
                    break;
                case CharacterConfig.CHARACTERS.Ryu:
                    RYU_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    RYU_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    RYU_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    RYU_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");

                    RYU_sprite_hair1.BackColor = currentCharacter.sprite.GetColor("hair1");
                    RYU_sprite_hair2.BackColor = currentCharacter.sprite.GetColor("hair2");

                    RYU_sprite_belt.BackColor = currentCharacter.sprite.GetColor("belt");

                    RYU_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    RYU_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    RYU_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    RYU_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    RYU_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");
                    RYU_sprite_costume6.BackColor = currentCharacter.sprite.GetColor("costume6");

                    RYU_sprite_headband1.BackColor = currentCharacter.sprite.GetColor("headband1");
                    RYU_sprite_headband2.BackColor = currentCharacter.sprite.GetColor("headband2");
                    break;
                case CharacterConfig.CHARACTERS.Ken:
                    KEN_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    KEN_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    KEN_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    KEN_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    KEN_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");
                    KEN_sprite_skin6.BackColor = currentCharacter.sprite.GetColor("skin6");

                    KEN_sprite_hair1.BackColor = currentCharacter.sprite.GetColor("hair1");
                    KEN_sprite_hair2.BackColor = currentCharacter.sprite.GetColor("hair2");

                    KEN_sprite_belt.BackColor = currentCharacter.sprite.GetColor("belt");

                    KEN_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    KEN_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    KEN_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    KEN_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    KEN_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");
                    KEN_sprite_costume6.BackColor = currentCharacter.sprite.GetColor("costume6");

                    break;
                case CharacterConfig.CHARACTERS.Chun:
                    CHU_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    CHU_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    CHU_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    CHU_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    CHU_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");

                    CHU_sprite_hair1.BackColor = currentCharacter.sprite.GetColor("hair1");
                    CHU_sprite_hair2.BackColor = currentCharacter.sprite.GetColor("hair2");
                    CHU_sprite_hair3.BackColor = currentCharacter.sprite.GetColor("hair3");
                    CHU_sprite_hair4.BackColor = currentCharacter.sprite.GetColor("hair4");
                    CHU_sprite_hair5.BackColor = currentCharacter.sprite.GetColor("hair5");

                    CHU_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    CHU_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    CHU_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    CHU_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    CHU_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");
                    break;
                case CharacterConfig.CHARACTERS.Ehonda:
                    EHO_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    EHO_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    EHO_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    EHO_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    EHO_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");
                    EHO_sprite_skin6.BackColor = currentCharacter.sprite.GetColor("skin6");
                    EHO_sprite_skin7.BackColor = currentCharacter.sprite.GetColor("skin7");

                    EHO_sprite_hair1.BackColor = currentCharacter.sprite.GetColor("hair1");
                    EHO_sprite_hair2.BackColor = currentCharacter.sprite.GetColor("hair2");

                    EHO_sprite_facepaint.BackColor = currentCharacter.sprite.GetColor("facepaint");

                    EHO_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    EHO_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    EHO_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    EHO_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    EHO_sprite_costume5.BackColor = currentCharacter.sprite.GetColor("costume5");

                    break;
                case CharacterConfig.CHARACTERS.Zangief:
                    ZAN_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
                    ZAN_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
                    ZAN_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
                    ZAN_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");
                    ZAN_sprite_skin5.BackColor = currentCharacter.sprite.GetColor("skin5");

                    ZAN_sprite_hair1.BackColor = currentCharacter.sprite.GetColor("hair1");
                    ZAN_sprite_hair2.BackColor = currentCharacter.sprite.GetColor("hair2");
                    ZAN_sprite_hair3.BackColor = currentCharacter.sprite.GetColor("hair3");

                    ZAN_sprite_belt1.BackColor = currentCharacter.sprite.GetColor("belt1");
                    ZAN_sprite_belt2.BackColor = currentCharacter.sprite.GetColor("belt2");
                    ZAN_sprite_belt3.BackColor = currentCharacter.sprite.GetColor("belt3");

                    ZAN_sprite_costume1.BackColor = currentCharacter.sprite.GetColor("costume1");
                    ZAN_sprite_costume2.BackColor = currentCharacter.sprite.GetColor("costume2");
                    ZAN_sprite_costume3.BackColor = currentCharacter.sprite.GetColor("costume3");
                    ZAN_sprite_costume4.BackColor = currentCharacter.sprite.GetColor("costume4");
                    break;

            }
        }

        private void load_portrait_buttons()
        {
            switch (currentCharacterType)
            {
                case CharacterConfig.CHARACTERS.Dictator:
                    portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");
                    portrait_skin5.BackColor = currentCharacter.portrait.GetColor("skin5");
                    portrait_skin6.BackColor = currentCharacter.portrait.GetColor("skin6");
                    portrait_skin7.BackColor = currentCharacter.portrait.GetColor("skin7");

                    portrait_teeth1.BackColor = currentCharacter.portrait.GetColor("teeth1");
                    portrait_teeth2.BackColor = currentCharacter.portrait.GetColor("teeth2");
                    portrait_teeth3.BackColor = currentCharacter.portrait.GetColor("teeth3");
                    portrait_teeth4.BackColor = currentCharacter.portrait.GetColor("teeth4");

                    portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");

                    portrait_costumeloss1.BackColor = currentCharacter.portrait.GetColor("costumeloss1");
                    portrait_costumeloss2.BackColor = currentCharacter.portrait.GetColor("costumeloss2");
                    portrait_costumeloss3.BackColor = currentCharacter.portrait.GetColor("costumeloss3");
                    portrait_costumeloss4.BackColor = currentCharacter.portrait.GetColor("costumeloss4");

                    portrait_piping1.BackColor = currentCharacter.portrait.GetColor("piping1");
                    portrait_piping2.BackColor = currentCharacter.portrait.GetColor("piping2");
                    portrait_piping3.BackColor = currentCharacter.portrait.GetColor("piping3");
                    portrait_piping4.BackColor = currentCharacter.portrait.GetColor("piping4");

                    portrait_pipingloss1.BackColor = currentCharacter.portrait.GetColor("pipingloss1");
                    portrait_pipingloss2.BackColor = currentCharacter.portrait.GetColor("pipingloss2");
                    portrait_pipingloss3.BackColor = currentCharacter.portrait.GetColor("pipingloss3");
                    portrait_pipingloss4.BackColor = currentCharacter.portrait.GetColor("pipingloss4");

                    portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");
                    break;

                case CharacterConfig.CHARACTERS.Claw:
                    portrait_claw_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    portrait_claw_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    portrait_claw_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");

                    portrait_claw_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    portrait_claw_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    portrait_claw_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");

                    portrait_claw_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    portrait_claw_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    portrait_claw_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");
                    portrait_claw_hair4.BackColor = currentCharacter.portrait.GetColor("hair4");

                    portrait_claw_metal1.BackColor = currentCharacter.portrait.GetColor("metal1");
                    portrait_claw_metal2.BackColor = currentCharacter.portrait.GetColor("metal2");
                    portrait_claw_metal3.BackColor = currentCharacter.portrait.GetColor("metal3");
                    portrait_claw_metal4.BackColor = currentCharacter.portrait.GetColor("metal4");
                    portrait_claw_metal5.BackColor = currentCharacter.portrait.GetColor("metal5");

                    portrait_claw_iris.BackColor = currentCharacter.portrait.GetColor("iris");

                    portrait_claw_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    portrait_claw_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    portrait_claw_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");
                    break;

                case CharacterConfig.CHARACTERS.Guile:
                    GUI_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    GUI_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    GUI_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    GUI_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");
                    GUI_portrait_skin5.BackColor = currentCharacter.portrait.GetColor("skin5");
                    GUI_portrait_skin6.BackColor = currentCharacter.portrait.GetColor("skin6");
                    GUI_portrait_skin7.BackColor = currentCharacter.portrait.GetColor("skin7");

                    GUI_portrait_chain1.BackColor = currentCharacter.portrait.GetColor("chain1");
                    GUI_portrait_chain2.BackColor = currentCharacter.portrait.GetColor("chain2");
                    GUI_portrait_chain3.BackColor = currentCharacter.portrait.GetColor("chain3");
                    GUI_portrait_chain4.BackColor = currentCharacter.portrait.GetColor("chain4");
                    GUI_portrait_chain5.BackColor = currentCharacter.portrait.GetColor("chain5");

                    GUI_portrait_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    GUI_portrait_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    GUI_portrait_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");
                    GUI_portrait_hair4.BackColor = currentCharacter.portrait.GetColor("hair4");
                    GUI_portrait_hair5.BackColor = currentCharacter.portrait.GetColor("hair5");

                    GUI_portrait_bruise1.BackColor = currentCharacter.portrait.GetColor("bruise1");
                    GUI_portrait_bruise2.BackColor = currentCharacter.portrait.GetColor("bruise2");
                    GUI_portrait_bruise3.BackColor = currentCharacter.portrait.GetColor("bruise3");
                    GUI_portrait_bruise4.BackColor = currentCharacter.portrait.GetColor("bruise4");
                    GUI_portrait_bruise5.BackColor = currentCharacter.portrait.GetColor("bruise5");

                    GUI_portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    GUI_portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    GUI_portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");

                    GUI_portrait_shirt1.BackColor = currentCharacter.portrait.GetColor("shirt1");
                    GUI_portrait_shirt2.BackColor = currentCharacter.portrait.GetColor("shirt2");
                    GUI_portrait_shirt3.BackColor = currentCharacter.portrait.GetColor("shirt3");
                    break;

                case CharacterConfig.CHARACTERS.Ryu:
                    RYU_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    RYU_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    RYU_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    RYU_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");
                    RYU_portrait_skin5.BackColor = currentCharacter.portrait.GetColor("skin5");
                    RYU_portrait_skin6.BackColor = currentCharacter.portrait.GetColor("skin6");
                    RYU_portrait_skin7.BackColor = currentCharacter.portrait.GetColor("skin7");

                    RYU_portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    RYU_portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    RYU_portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    RYU_portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");
                    RYU_portrait_costume5.BackColor = currentCharacter.portrait.GetColor("costume5");

                    RYU_portrait_eyes1.BackColor = currentCharacter.portrait.GetColor("eyes1");
                    RYU_portrait_eyes2.BackColor = currentCharacter.portrait.GetColor("eyes2");
                    RYU_portrait_eyes3.BackColor = currentCharacter.portrait.GetColor("eyes3");

                    RYU_portrait_headband1.BackColor = currentCharacter.portrait.GetColor("headband1");
                    RYU_portrait_headband2.BackColor = currentCharacter.portrait.GetColor("headband2");
                    RYU_portrait_headband3.BackColor = currentCharacter.portrait.GetColor("headband3");

                    RYU_portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    RYU_portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    RYU_portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");

                    RYU_portrait_teeth1.BackColor = currentCharacter.portrait.GetColor("teeth1");
                    RYU_portrait_teeth2.BackColor = currentCharacter.portrait.GetColor("teeth2");
                    break;

                case CharacterConfig.CHARACTERS.Chun:
                    CHU_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    CHU_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");

                    CHU_portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    CHU_portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    CHU_portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    CHU_portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");
                    CHU_portrait_costume5.BackColor = currentCharacter.portrait.GetColor("costume5");

                    CHU_portrait_lips1.BackColor = currentCharacter.portrait.GetColor("lips1");
                    CHU_portrait_lips2.BackColor = currentCharacter.portrait.GetColor("lips2");
                    CHU_portrait_lips3.BackColor = currentCharacter.portrait.GetColor("lips3");
                    CHU_portrait_lips4.BackColor = currentCharacter.portrait.GetColor("lips4");

                    CHU_portrait_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    CHU_portrait_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    CHU_portrait_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");
                    CHU_portrait_hair4.BackColor = currentCharacter.portrait.GetColor("hair4");

                    CHU_portrait_bruise1.BackColor = currentCharacter.portrait.GetColor("bruise1");
                    CHU_portrait_bruise2.BackColor = currentCharacter.portrait.GetColor("bruise2");
                    break;
                case CharacterConfig.CHARACTERS.Boxer:
                    BOX_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    BOX_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    BOX_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    BOX_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");
                    BOX_portrait_skin5.BackColor = currentCharacter.portrait.GetColor("skin5");
                    BOX_portrait_skin6.BackColor = currentCharacter.portrait.GetColor("skin6");

                    BOX_portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    BOX_portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    BOX_portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    BOX_portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");

                    BOX_portrait_bruise1.BackColor = currentCharacter.portrait.GetColor("bruise1");
                    BOX_portrait_bruise2.BackColor = currentCharacter.portrait.GetColor("bruise2");
                    BOX_portrait_bruise3.BackColor = currentCharacter.portrait.GetColor("bruise3");
                    BOX_portrait_bruise4.BackColor = currentCharacter.portrait.GetColor("bruise4");

                    BOX_portrait_teeth1.BackColor = currentCharacter.portrait.GetColor("teeth1");
                    BOX_portrait_teeth2.BackColor = currentCharacter.portrait.GetColor("teeth2");
                    BOX_portrait_teeth3.BackColor = currentCharacter.portrait.GetColor("teeth3");
                    BOX_portrait_teeth4.BackColor = currentCharacter.portrait.GetColor("teeth4");
                    BOX_portrait_teeth5.BackColor = currentCharacter.portrait.GetColor("teeth5");
                    BOX_portrait_teeth6.BackColor = currentCharacter.portrait.GetColor("teeth6");

                    BOX_portrait_gloves1.BackColor = currentCharacter.portrait.GetColor("gloves1");
                    BOX_portrait_gloves2.BackColor = currentCharacter.portrait.GetColor("gloves2");
                    BOX_portrait_gloves3.BackColor = currentCharacter.portrait.GetColor("gloves3");
                    BOX_portrait_gloves4.BackColor = currentCharacter.portrait.GetColor("gloves4");
                    BOX_portrait_gloves5.BackColor = currentCharacter.portrait.GetColor("gloves5");
                    BOX_portrait_gloves6.BackColor = currentCharacter.portrait.GetColor("gloves6");
                    BOX_portrait_gloves7.BackColor = currentCharacter.portrait.GetColor("gloves7");
                    BOX_portrait_gloves8.BackColor = currentCharacter.portrait.GetColor("gloves8");
                    BOX_portrait_gloves9.BackColor = currentCharacter.portrait.GetColor("gloves9");
                    BOX_portrait_gloves10.BackColor = currentCharacter.portrait.GetColor("gloves10");

                    BOX_portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    BOX_portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    BOX_portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");
                    break;

                case CharacterConfig.CHARACTERS.Ken:
                    KEN_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    KEN_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    KEN_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    KEN_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");
                    KEN_portrait_skin5.BackColor = currentCharacter.portrait.GetColor("skin5");
                    KEN_portrait_skin6.BackColor = currentCharacter.portrait.GetColor("skin6");
                    KEN_portrait_skin7.BackColor = currentCharacter.portrait.GetColor("skin7");

                    KEN_portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    KEN_portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    KEN_portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    KEN_portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");

                    KEN_portrait_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    KEN_portrait_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    KEN_portrait_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");
                    KEN_portrait_hair4.BackColor = currentCharacter.portrait.GetColor("hair4");

                    KEN_portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    KEN_portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    KEN_portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");

                    KEN_portrait_teeth1.BackColor = currentCharacter.portrait.GetColor("teeth1");
                    KEN_portrait_teeth2.BackColor = currentCharacter.portrait.GetColor("teeth2");
                    KEN_portrait_teeth3.BackColor = currentCharacter.portrait.GetColor("teeth3");
                    break;

                case CharacterConfig.CHARACTERS.Zangief:
                    ZAN_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    ZAN_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    ZAN_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    ZAN_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");

                    ZAN_portrait_costume1.BackColor = currentCharacter.portrait.GetColor("costume1");
                    ZAN_portrait_costume2.BackColor = currentCharacter.portrait.GetColor("costume2");
                    ZAN_portrait_costume3.BackColor = currentCharacter.portrait.GetColor("costume3");
                    ZAN_portrait_costume4.BackColor = currentCharacter.portrait.GetColor("costume4");
                    ZAN_portrait_costume5.BackColor = currentCharacter.portrait.GetColor("costume5");

                    ZAN_portrait_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    ZAN_portrait_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    ZAN_portrait_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");

                    ZAN_portrait_blood1.BackColor = currentCharacter.portrait.GetColor("blood1");
                    ZAN_portrait_blood2.BackColor = currentCharacter.portrait.GetColor("blood2");
                    ZAN_portrait_blood3.BackColor = currentCharacter.portrait.GetColor("blood3");

                    ZAN_portrait_eyes1.BackColor = currentCharacter.portrait.GetColor("eyes1");
                    ZAN_portrait_eyes2.BackColor = currentCharacter.portrait.GetColor("eyes2");
                    ZAN_portrait_eyes3.BackColor = currentCharacter.portrait.GetColor("eyes3");
                    ZAN_portrait_eyes4.BackColor = currentCharacter.portrait.GetColor("eyes4");
                    ZAN_portrait_eyes5.BackColor = currentCharacter.portrait.GetColor("eyes5");
                    break;
                case CharacterConfig.CHARACTERS.Ehonda:
                    EHO_portrait_skin1.BackColor = currentCharacter.portrait.GetColor("skin1");
                    EHO_portrait_skin2.BackColor = currentCharacter.portrait.GetColor("skin2");
                    EHO_portrait_skin3.BackColor = currentCharacter.portrait.GetColor("skin3");
                    EHO_portrait_skin4.BackColor = currentCharacter.portrait.GetColor("skin4");

                    EHO_portrait_hair1.BackColor = currentCharacter.portrait.GetColor("hair1");
                    EHO_portrait_hair2.BackColor = currentCharacter.portrait.GetColor("hair2");
                    EHO_portrait_hair3.BackColor = currentCharacter.portrait.GetColor("hair3");

                    EHO_portrait_mouth1.BackColor = currentCharacter.portrait.GetColor("mouth1");
                    EHO_portrait_mouth2.BackColor = currentCharacter.portrait.GetColor("mouth2");

                    EHO_portrait_facepaint1.BackColor = currentCharacter.portrait.GetColor("facepaint1");
                    EHO_portrait_facepaint2.BackColor = currentCharacter.portrait.GetColor("facepaint2");
                    EHO_portrait_facepaint3.BackColor = currentCharacter.portrait.GetColor("facepaint3");

                    EHO_portrait_facepaintloss1.BackColor = currentCharacter.portrait.GetColor("facepaintloss1");
                    EHO_portrait_facepaintloss2.BackColor = currentCharacter.portrait.GetColor("facepaintloss2");
                    EHO_portrait_facepaintloss3.BackColor = currentCharacter.portrait.GetColor("facepaintloss3");

                    EHO_portrait_teeth1.BackColor = currentCharacter.portrait.GetColor("teeth1");
                    EHO_portrait_teeth2.BackColor = currentCharacter.portrait.GetColor("teeth2");
                    EHO_portrait_teeth3.BackColor = currentCharacter.portrait.GetColor("teeth3");
                    break;
            }
        }

        public Bitmap magnify_sprite(Image img, int factor)
        {
            int neww = img.Width * factor;
            int newh = img.Height * factor;
            Bitmap newbmp = new Bitmap(neww, newh);

            Bitmap bmp = new Bitmap(img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);
                    for (int i = 0; i < factor; i++)
                    {
                        for (int j = 0; j < factor; j++)
                        {
                            newbmp.SetPixel(factor * x + i, factor * y + j, gotColor);
                        }
                    }
                }
            }

            return newbmp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSpritesAndPalettesFromDropDown();
            if (currentlyZoomedLabel != null)
                z.displayZoomImage();
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void COLlabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            byte[] lineasbytes = File.ReadAllBytes(s[0]);
            string colstr = System.Text.Encoding.UTF8.GetString(lineasbytes);
            var v = colstr.Split(':');
            currentCharacter.loadFromColFormat(colstr);
            reload_everything();
        }

        private void COLlabel_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            byte[] lineasbytes = File.ReadAllBytes(s[0]);
            string colstr = System.Text.Encoding.UTF8.GetString(lineasbytes);
            var colCharacter = Character.CharacterFromColFormat(colstr);
            if (colCharacter.characterType == currentCharacter.characterType)
            {
                currentCharacter = Character.CharacterFromColFormat(colstr);
                reload_everything();
            }
            else
                MessageBox.Show("Incorrect character type");
        }


        private void pal_square_click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (previouslySelectedSquare != null)
                previouslySelectedSquare.BorderStyle = BorderStyle.FixedSingle;
            p.BorderStyle = BorderStyle.Fixed3D;
            previouslySelectedSquare = p;
            currentlySelectedColor = p;
            Color c = p.BackColor;
            int r = c.R / 17;
            int g = c.G / 17;
            int b = c.B / 17;
            skip_image_recolors = true;
            pal_val_R.Text = r.ToString();
            pal_val_G.Text = g.ToString();
            pal_val_B.Text = b.ToString();

            trackBarR.Value = r;
            trackBarG.Value = g;
            trackBarB.Value = b;
            skip_image_recolors = false;
        }

        private void zoom(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (z.IsDisposed)
                z = new ZoomForm(this);
            z.Show();
            // todo replace this with a regex
            switch (p.Name)
            {
                case "psychopunchBox":
                    currentlyZoomedLabel = "psychopunch";
                    break;
                case "psychoprepBox":
                    currentlyZoomedLabel = "psychoprep";
                    break;
                case "crushertopBox":
                    currentlyZoomedLabel = "crushertop";
                    break;
                case "crusherbottomBox":
                    currentlyZoomedLabel = "crusherbottom";
                    break;
                case "BOX_neutralStandBox":
                case "DIC_neutralStandBox":
                case "CLA_neutralStandBox":
                case "CHU_neutralStandBox":
                case "GUI_neutralStandBox":
                case "RYU_neutralStandBox":
                case "KEN_neutralStandBox":
                case "ZAN_neutralStandBox":
                case "EHO_neutralStandBox":
                    currentlyZoomedLabel = "neutral";
                    break;
                case "BOX_portraitVictoryBox":
                case "DIC_portraitVictoryBox":
                case "CLA_portraitVictoryBox":
                case "CHU_portraitVictoryBox":
                case "GUI_portraitVictoryBox":
                case "RYU_portraitVictoryBox":
                case "KEN_portraitVictoryBox":
                case "ZAN_portraitVictoryBox":
                case "EHO_portraitVictoryBox":
                    currentlyZoomedLabel = "victory";
                    break;
                case "BOX_portraitLossBox":
                case "DIC_portraitLossBox":
                case "CLA_portraitLossBox":
                case "CHU_portraitLossBox":
                case "GUI_portraitLossBox":
                case "RYU_portraitLossBox":
                case "KEN_portraitLossBox":
                case "ZAN_portraitLossBox":
                case "EHO_portraitLossBox":
                    currentlyZoomedLabel = "loss";
                    break;
            }
            z.displayZoomImage();
        }

        private void trackBarR_Scroll(object sender, EventArgs e)
        {
            pal_val_R.Text = "" + trackBarR.Value;
        }

        private void trackBarG_Scroll(object sender, EventArgs e)
        {
            pal_val_G.Text = "" + trackBarG.Value;
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            pal_val_B.Text = "" + trackBarB.Value;
        }

        private int clamp(int i)
        {
            if (i < 0)
                return 0;
            if (i > 15)
                return 15;
            return i;
        }

        private void pal_val_TextChanged(object sender, EventArgs e)
        {
            if (currentlySelectedColor == null)
                return;
            if (skip_image_recolors)
                return;
            int value = 0;
            var tb = (TextBox)sender;
            try
            {
                value = Int32.Parse(tb.Text);
            }
            catch (Exception)
            {
                // nothing
            }
            value = clamp(value);
            var c = currentlySelectedColor.BackColor;
            Color newcolor = Color.Black;
            switch (tb.Name)
            {
                case "pal_val_R":
                    trackBarR.Value = value;
                    newcolor = Color.FromArgb(c.A, value * 17, c.G, c.B);
                    break;
                case "pal_val_G":
                    trackBarG.Value = value;
                    newcolor = Color.FromArgb(c.A, c.R, value * 17, c.B);
                    break;
                case "pal_val_B":
                    trackBarB.Value = value;
                    newcolor = Color.FromArgb(c.A, c.R, c.G, value * 17);
                    break;
            }
            currentlySelectedColor.BackColor = newcolor;
        }

        private void updatePortraitColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            string label = extractLabel(p.Name);
            currentCharacter.portrait.SetColor(label, c);

            load_portrait_victory();
            load_portrait_loss();
        }

        private string extractLabel(string s)
        {
            Match m = rx.Match(s);
            string label = m.Value;
            return label;
        }

        private void updateSpriteColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            string label = extractLabel(p.Name);
            currentCharacter.sprite.SetColor(label, c);
            load_sprites();
        }

        private void load_sprites()
        {
            load_sprite_neutralstand();
            if (currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                load_sprite_load_sprite_psychopunch();
                load_sprite_load_sprite_psychoprep();
                load_sprite_load_sprite_crushertop();
            }
        }

        private void updateSpriteCrusherColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            string label = extractLabel(p.Name);
            currentCharacter.sprite.SetColor(label, c);

            load_sprite_load_sprite_crushertop();
            load_sprite_load_sprite_crusherbottom();
        }

        private void portrait_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updatePortraitColor(c, p);
        }

        private void sprite_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updateSpriteColor(c, p);
        }

        private void spriteCrusher_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updateSpriteCrusherColor(c, p);
        }

        private void colorSwapBG_Click(object sender, EventArgs e)
        {
            var b = pal_val_B.Text;
            pal_val_B.Text = pal_val_G.Text;
            pal_val_G.Text = b;
        }

        private void colorSwapRB_Click(object sender, EventArgs e)
        {
            var r = pal_val_R.Text;
            pal_val_R.Text = pal_val_B.Text;
            pal_val_B.Text = r;
        }

        private void colorSwapGR_Click(object sender, EventArgs e)
        {
            var g = pal_val_G.Text;
            pal_val_G.Text = pal_val_R.Text;
            pal_val_R.Text = g;
        }

        private void colorCycleRGB_Click(object sender, EventArgs e)
        {
            var r = pal_val_R.Text;
            var g = pal_val_G.Text;
            var b = pal_val_B.Text;
            pal_val_R.Text = g;
            pal_val_G.Text = b;
            pal_val_B.Text = r;
        }


        private void colorCycleRBG_Click(object sender, EventArgs e)
        {
            var r = pal_val_R.Text;
            var g = pal_val_G.Text;
            var b = pal_val_B.Text;
            pal_val_R.Text = b;
            pal_val_B.Text = g;
            pal_val_G.Text = r;
        }

        private void invertColorsButton_Click(object sender, EventArgs e)
        {
            var g = trackBarG.Value;
            var r = trackBarR.Value;
            var b = trackBarB.Value;
            pal_val_G.Text = (17 - g).ToString();
            pal_val_R.Text = (17 - r).ToString();
            pal_val_B.Text = (17 - b).ToString();
        }

        // todo fix this for new col format
        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Color Files (*.col)|*.col";
            saveFileDialog1.Title = "Save a color File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    string s = currentCharacter.ToColFormat();
                    var b = Encoding.ASCII.GetBytes(s); // todo can we replace the palettehelper method with this?
                    fs.Seek(0, SeekOrigin.End);
                    await fs.WriteAsync(b, 0, b.Length);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCharacterColorToSet();
        }

        private void saveCharacterColorToSet()
        {
            characterSet.characterColors[colorSelectorBox.SelectedIndex] = currentCharacter;
        }


        private void resetCurrentCharacterColorFromDropDown()
        {
            currentCharacter = characterSet.characterColors[colorSelectorBox.SelectedIndex];
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var version2 = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
           

            //string version = System.Windows.Forms.Application.ProductVersion;
            var t = String.Format("Palette Swapper Version {0}", version2);

            MessageBox.Show(t + "\nby Zass, 2020");
        }

        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    try
                    {
                        //Read the contents of the file into a stream
                        FileStream fileStream = (System.IO.FileStream)openFileDialog.OpenFile();
                        gameSet = GameSet.GameSetFromZipStream(fileStream);
                        characterSet = gameSet.characterDictionary[currentCharacterType];
                        resetCurrentCharacterColorFromDropDown();
                        reload_everything();
                        fileStream.Close();
                    }
                    catch (Exception){
                        MessageBox.Show("Invalid ROM format");
                    }
                }
            }
        }

        private void savePatchedRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRom(sender, e, ROMSTYLE.us);
        }

        private void savePhoenixRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRom(sender, e, ROMSTYLE.phoenix);
        }

        private void saveJapaneseRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePatchedRom(sender, e, ROMSTYLE.japanese);
        }

        private void colorSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c.IsDisposed)
                c = new ColorSetForm(this);
            c.Reload();
            c.Show();
        }

        private void savePatchedRom(object sender, EventArgs e, ROMSTYLE r)
        {
            if (DISABLE_PATCHING == true)
                return;
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save a rom";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    using (var archive = new ZipArchive(fs, ZipArchiveMode.Create, true))
                    {
                        string _03filename;
                        string _04filename;
                        string _06filename;

                        byte[] p_stream;
                        byte[] s_stream;
                        byte[] oldbisonpunches_stream;

                        switch (r)
                        {
                            case ROMSTYLE.japanese:
                                _03filename = "sfxj.03c";
                                _04filename = "sfxj.04a";
                                _06filename = "sfxj.06a";

                                p_stream = gameSet.portraits_stream03japanese();
                                s_stream = gameSet.sprites_stream04japanese();
                                oldbisonpunches_stream = gameSet.PatchOldBisonPunches06japanese();
                                break;
                            case ROMSTYLE.phoenix:
                                _03filename = "sfxjd.03c";
                                _04filename = "sfxjd.04a";
                                _06filename = "sfxjd.06a";

                                p_stream = gameSet.portraits_stream03phoenix();
                                s_stream = gameSet.sprites_stream04phoenix();
                                oldbisonpunches_stream = gameSet.PatchOldBisonPunches06phoenix();
                                break;
                            default:
                            case ROMSTYLE.us:
                                _03filename = "sfxe.03c";
                                _04filename = "sfxe.04a";
                                _06filename = "sfxe.06a";

                                p_stream = gameSet.portraits_stream03();
                                s_stream = gameSet.sprites_stream04();
                                oldbisonpunches_stream = gameSet.PatchOldBisonPunches06();
                                break;
                        }

                        var _03file = archive.CreateEntry(_03filename);
                        using (var entryStream = _03file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            var c = entryStream.CanSeek;
                            entryStream.Write(p_stream, 0, p_stream.Length);
                        }

                        var _04file = archive.CreateEntry(_04filename);
                        using (var entryStream = _04file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            var c = entryStream.CanSeek;
                            entryStream.Write(s_stream, 0, s_stream.Length);
                        }

                        var _06file = archive.CreateEntry(_06filename);
                        using (var entryStream = _06file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            var c = entryStream.CanSeek;
                            entryStream.Write(oldbisonpunches_stream, 0, oldbisonpunches_stream.Length);
                        }
                    }
                }
            }
        }

        private void tabSelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = (TabControl)sender;
            var selt = tabControl.SelectedTab;
            if (selt.Name == "TabPageClaw")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Claw;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Claw];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageDictator")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Dictator;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Dictator];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageGuile")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Guile;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Guile];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageRyu")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Ryu;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Ryu];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageKen")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Ken;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Ken];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageChun")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Chun;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Chun];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageBoxer")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Boxer;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Boxer];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageZangief")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Zangief;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Zangief];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
            else if (selt.Name == "TabPageHonda")
            {
                currentCharacterType = CharacterConfig.CHARACTERS.Ehonda;
                characterSet = gameSet.characterDictionary[CharacterConfig.CHARACTERS.Ehonda];
                currentCharacter = characterSet.characterColors[0];
                SetDefaultDropDown();
                reload_everything();
            }
        }

        private void saveGameColorSetAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save a rom";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                using (System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile())
                {
                    using (var archive = new ZipArchive(fs, ZipArchiveMode.Create, true))
                    {
                        // for each character
                        // create an folder with the 3 letter code
                        // in that folder
                        // for each of the 10 colors, create a a file
                        // 01.col, 02.col... 10.col

                        foreach (var charType in gameSet.characterDictionary.Keys)
                        {
                            var charCode = CharacterConfig.CodeFromCharacterEnum(charType);
                            // add colorset key
                            var keyentry = archive.CreateEntry(charCode + @"/" + "NeutralKey.png");
                            using (var entryStream = keyentry.Open())
                            {
                                var b = gameSet.characterDictionary[charType].GenerateSpriteKey();
                                b.Save(entryStream, System.Drawing.Imaging.ImageFormat.Png);
                            }

                            keyentry = archive.CreateEntry(charCode + @"/" + "PortraitKey.png");
                            using (var entryStream = keyentry.Open())
                            {
                                var b = gameSet.characterDictionary[charType].GeneratePortraitKey();
                                b.Save(entryStream, System.Drawing.Imaging.ImageFormat.Png);
                            }

                            keyentry = archive.CreateEntry(charCode + @"/" + "LossKey.png");
                            using (var entryStream = keyentry.Open())
                            {
                                var b = gameSet.characterDictionary[charType].GenerateLossKey();
                                b.Save(entryStream, System.Drawing.Imaging.ImageFormat.Png);
                            }

                            for (int i = 0; i < 10; i++)
                            {
                                var entry = archive.CreateEntry(charCode + @"/" + "0" + i.ToString() + ".col");

                                using (var entryStream = entry.Open())
                                {
                                    using (var streamWriter = new StreamWriter(entryStream))
                                    {
                                        streamWriter.Write(gameSet.characterDictionary[charType].characterColors[i].ToColFormat());
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void colorSetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    try
                    {
                        //Read the contents of the file into a stream
                        FileStream fileStream = (System.IO.FileStream)openFileDialog.OpenFile();
                        gameSet = GameSet.GameSetFromZipColorSet(fileStream);
                        characterSet = gameSet.characterDictionary[currentCharacterType];
                        resetCurrentCharacterColorFromDropDown();
                        reload_everything();
                        fileStream.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Invalid colorset format");
                    }
                }
            }
        }

        private void resetColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (CharacterConfig.CHARACTERS character in supportedCharacters)
            {
                gameSet.characterDictionary[character] = new CharacterSet(character);
            }
            characterSet = gameSet.characterDictionary[currentCharacterType];
            resetCurrentCharacterColorFromDropDown();
            reload_everything();
        }
    }
}