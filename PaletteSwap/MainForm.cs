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
        ZoomForm z;
        ColorSetForm c;
        bool fixDicatorOldPunches = true;
        public Color backgroundcolor = Color.Black;
        public string currentlyZoomedLabel;
        public CharacterConfig.CHARACTERS currentCharacterType;
        PictureBox previouslySelectedSquare;
        PictureBox currentlySelectedColor;
        public GameSet gameSet = new GameSet();
        public CharacterSet characterSet;
        public Character currentCharacter;
        bool skip_image_recolors = false;
        int DEFAULT_DROPDOWN_INDEX = 0;
        enum ROMSTYLE { us, japanese, phoenix, newlegacy, redggpo };
        Regex rx = new Regex(@"[^_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        CharacterConfig.CHARACTERS[] supportedCharacters = new CharacterConfig.CHARACTERS[] { CharacterConfig.CHARACTERS.Dictator, CharacterConfig.CHARACTERS.Claw,
                CharacterConfig.CHARACTERS.Guile, CharacterConfig.CHARACTERS.Ryu, CharacterConfig.CHARACTERS.Chun, CharacterConfig.CHARACTERS.Boxer, CharacterConfig.CHARACTERS.Ken,
                CharacterConfig.CHARACTERS.Zangief, CharacterConfig.CHARACTERS.Ehonda,
            CharacterConfig.CHARACTERS.Sagat, CharacterConfig.CHARACTERS.Feilong, CharacterConfig.CHARACTERS.Deejay,
        CharacterConfig.CHARACTERS.Dhalsim, CharacterConfig.CHARACTERS.Cammy, CharacterConfig.CHARACTERS.Thawk,
        CharacterConfig.CHARACTERS.Blanka, CharacterConfig.CHARACTERS.Gouki};

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
            setBackColor();
            load_portrait_buttons();
            load_sprite_buttons();
            load_sprite_neutralstand();
            load_portrait_victory();
            if (currentCharacterType != CharacterConfig.CHARACTERS.Gouki)
            {
                load_portrait_loss();
            }
            if (currentCharacterType == CharacterConfig.CHARACTERS.Gouki)
            {
                load_sprite_teleports();
            }
            if (currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                load_sprite_psychopunch();
                load_sprite_psychoprep();
                load_sprite_crushertop();
                load_sprite_crusherbottom();
                load_sprite_crusherback();
            }

            skip_image_recolors = false;
        }

        private void load_sprite_neutralstand()
        {
            var n = currentCharacter.GetBitmap("neutral");
            var p = GetSpriteNeutralBox();
            p.BackgroundImage = n;
        }

        private void load_sprite_psychopunch()
        {
            psychopunchBox.BackgroundImage = currentCharacter.sprite.GetBitmap("psychopunch");
        }

        private void load_sprite_psychoprep()
        {
            psychoprepBox.BackgroundImage = currentCharacter.sprite.GetBitmap("psychoprep");
        }

        private void load_sprite_crushertop()
        {
            crushertopBox.BackgroundImage = currentCharacter.sprite.GetBitmap("crushertop");
        }

        private void load_sprite_crusherbottom()
        {
            crusherbottomBox.BackgroundImage = currentCharacter.sprite.GetBitmap("crusherbottom");
        }

        private void load_sprite_crusherback()
        {
            crusherbackBox.BackgroundImage = currentCharacter.sprite.GetBitmap("crusherback");
        }


        private void load_sprite_teleports()
        {
            load_sprite_teleport1();
            load_sprite_teleport2();
            load_sprite_teleport3();
        }

        private void load_sprite_teleport1()
        {
            GOU_teleportBox1.BackgroundImage = currentCharacter.sprite.GetBitmap("teleport1");
        }

        private void load_sprite_teleport2()
        {
            GOU_teleportBox2.BackgroundImage = currentCharacter.sprite.GetBitmap("teleport2");
        }

        private void load_sprite_teleport3()
        {
            GOU_teleportBox3.BackgroundImage = currentCharacter.sprite.GetBitmap("teleport3");
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
            return GetBoxByName("_neutralStandBox");
        }

        private PictureBox GetPortraitLossBox()
        {
            return GetBoxByName("_portraitLossBox");
        }

        private PictureBox GetPortraitVictoryBox()
        {
            return GetBoxByName("_portraitVictoryBox");
        }

        private PictureBox GetBoxByName(string boxName)
        {
            var shortcut = CharacterConfig.CodeFromCharacterEnum(currentCharacterType);
            var s = shortcut + boxName;
            PictureBox mybox = (PictureBox)this.Controls.Find(s, true)[0];
            return mybox;
        }

        private void load_sprite_buttons()
        {
            var f = currentCharacter.sprite;
            load_buttons(f, "_sprite_");
            return;
        }

        private void load_portrait_buttons()
        {
            var f = currentCharacter.portrait;
            load_buttons(f, "_portrait_");
            return;
        }

        private void load_buttons(Palette p, string boxName)
        {
            var labels = p.labelsToColors;
            foreach (var l in labels)
            {
                var s = boxName + l.Key;
                var mybox = GetBoxByName(s);
                mybox.BackColor = p.GetColor(l.Key);
            }
            return;
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
            closeZoomIfNotRelevant();

        }

        private void closeZoomIfNotRelevant()
        {
            if (currentlyZoomedLabel != null)
            {
                if (((currentlyZoomedLabel == "crusherbottom" ||
                    currentlyZoomedLabel == "crushertop" ||
                    currentlyZoomedLabel == "crusherback" ||
                    currentlyZoomedLabel == "psychoprep" ||
                    currentlyZoomedLabel == "psychopunch") &&
                    currentCharacterType != CharacterConfig.CHARACTERS.Dictator) ||
                    (currentlyZoomedLabel == "loss" &&
                    currentCharacterType == CharacterConfig.CHARACTERS.Gouki
                    )
                                        )
                    z.Close();
                else
                z.displayZoomImage();
            }
        }

        private void COLlabel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
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
                saveCharacterColorToSet();
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
            var l = extractLabel(p.Name);
            switch (l)
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
                case "crusherbackBox":
                    currentlyZoomedLabel = "crusherback";
                    break;
                case "neutralStandBox":
                    currentlyZoomedLabel = "neutral";
                    break;
                case "portraitVictoryBox":
                    currentlyZoomedLabel = "victory";
                    break;
                case "portraitLossBox":
                    currentlyZoomedLabel = "loss";
                    break;
                case "teleportBox1":
                    currentlyZoomedLabel = "teleport1";
                    break;
                case "teleportBox2":
                    currentlyZoomedLabel = "teleport2";
                    break;
                case "teleportBox3":
                    currentlyZoomedLabel = "teleport3";
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
            if (currentCharacterType != CharacterConfig.CHARACTERS.Gouki)
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
                load_sprite_psychopunch();
                load_sprite_psychoprep();
                load_sprite_crushertop();
                load_sprite_crusherback();
            }
            if (currentCharacterType == CharacterConfig.CHARACTERS.Gouki)
            {
                load_sprite_teleports();
            }
        }

        private void updateSpriteCrusherColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            string label = extractLabel(p.Name);
            currentCharacter.sprite.SetColor(label, c);

            load_sprite_crushertop();
            load_sprite_crusherbottom();
            load_sprite_crusherback();
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

        private void incrementColorsButton_Click(object sender, EventArgs e)
        {
            var g = trackBarG.Value;
            var r = trackBarR.Value;
            var b = trackBarB.Value;
            pal_val_G.Text = Math.Min((g + 1), 15).ToString();
            pal_val_R.Text = Math.Min((r + 1), 15).ToString();
            pal_val_B.Text = Math.Min((b + 1), 15).ToString();
        }

        private void decrementColorsButton_Click(object sender, EventArgs e)
        {
            var g = trackBarG.Value;
            var r = trackBarR.Value;
            var b = trackBarB.Value;
            pal_val_G.Text = Math.Max((g - 1), 0).ToString();
            pal_val_R.Text = Math.Max((r - 1), 0).ToString();
            pal_val_B.Text = Math.Max((b - 1), 0).ToString();
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

            MessageBox.Show(t + "\nby Zass");
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        private void patchRomToolStripMenuItem_Click(object sender, EventArgs e)
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
                        using (FileStream fs = new FileStream(filePath, FileMode.Open))
                        {
                            gameSet.PatchZippedRom(fs, fixDicatorOldPunches);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void colorSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c.IsDisposed)
                c = new ColorSetForm(this);
            c.Reload();
            c.Show();
        }

        private void ChangeIndexToCharacter(CharacterConfig.CHARACTERS character)
        {
            currentCharacterType = character;
            characterSet = gameSet.characterDictionary[character];
            currentCharacter = characterSet.characterColors[colorSelectorBox.SelectedIndex];
            closeZoomIfNotRelevant();
            reload_everything();
        }

        private void tabSelectedIndexChanged(object sender, EventArgs e)
        {
            var tabControl = (TabControl)sender;
            var selt = tabControl.SelectedTab;
            if (selt.Name == "TabPageClaw")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Claw);
            }
            else if (selt.Name == "TabPageDictator")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Dictator);
            }
            else if (selt.Name == "TabPageGuile")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Guile);
            }
            else if (selt.Name == "TabPageRyu")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Ryu);
            }
            else if (selt.Name == "TabPageKen")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Ken);
            }
            else if (selt.Name == "TabPageChun")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Chun);
            }
            else if (selt.Name == "TabPageBoxer")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Boxer);
            }
            else if (selt.Name == "TabPageZangief")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Zangief);
            }
            else if (selt.Name == "TabPageHonda")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Ehonda);
            }
            else if (selt.Name == "TabPageSagat")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Sagat);
            }
            else if (selt.Name == "TabPageFei")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Feilong);
            }
            else if (selt.Name == "TabPageDeejay")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Deejay);
            }
            else if (selt.Name == "TabPageDhalsim")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Dhalsim);
            }
            else if (selt.Name == "TabPageCammy")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Cammy);
            }
            else if (selt.Name == "TabPageHawk")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Thawk);
            }
            else if (selt.Name == "TabPageBlanka")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Blanka);
            }
            else if (selt.Name == "TabPageGouki")
            {
                ChangeIndexToCharacter(CharacterConfig.CHARACTERS.Gouki);
            }
        }

        private void saveGameColorSetAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save a colorset";
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
                                if (charType == CharacterConfig.CHARACTERS.Dictator)
                                    b = gameSet.characterDictionary[charType].GenerateKey("psychopunch");
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
                                if (charType != CharacterConfig.CHARACTERS.Gouki)
                                {
                                    var b = gameSet.characterDictionary[charType].GenerateLossKey();
                                    b.Save(entryStream, System.Drawing.Imaging.ImageFormat.Png);
                                }
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

        private void resetCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameSet.characterDictionary[currentCharacterType] = new CharacterSet(currentCharacterType);
            characterSet = gameSet.characterDictionary[currentCharacterType];
            resetCurrentCharacterColorFromDropDown();
            reload_everything();
        }

        private void resetThisColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameSet.characterDictionary[currentCharacterType].characterColors[colorSelectorBox.SelectedIndex] =
                Character.CreateDefaultCharacter(currentCharacterType, (CharacterConfig.BUTTONS)colorSelectorBox.SelectedIndex);
            characterSet = gameSet.characterDictionary[currentCharacterType];
            resetCurrentCharacterColorFromDropDown();
            reload_everything();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentlySelectedColor == null)
                return;
            var c = currentlySelectedColor.BackColor;
            var s = PaletteHelper.ColorFormatToRGB(c);
            Clipboard.SetText(s);
            return;
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentlySelectedColor == null)
                return;
            var name = currentlySelectedColor.Name;
            var s = Clipboard.GetText();
            var c = PaletteHelper.RGBFormatToColor(s);
            var r = ((c.R) / 17).ToString();
            var g = ((c.G) / 17).ToString();
            var b = ((c.B) / 17).ToString();
            pal_val_R.Text = r;
            pal_val_G.Text = g;
            pal_val_B.Text = b;
            return;
        }

        private void extendedColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extendedColorsToolStripMenuItem.Checked = !extendedColorsToolStripMenuItem.Checked;
        }

        private void magentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.magentaToolStripMenuItem.Checked = true;
            this.blackToolStripMenuItem.Checked = false;
            this.cyanToolStripMenuItem.Checked = false;
            backgroundcolor = Color.Magenta;
            setBackColor();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.magentaToolStripMenuItem.Checked = false;
            this.blackToolStripMenuItem.Checked = true;
            this.cyanToolStripMenuItem.Checked = false;
            backgroundcolor = Color.Black;
            setBackColor();
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cyanToolStripMenuItem.Checked = true;
            this.magentaToolStripMenuItem.Checked = false;
            this.blackToolStripMenuItem.Checked = false;
            backgroundcolor = Color.Cyan;
            setBackColor();
        }

        private void setBackColor()
        {
            GetSpriteNeutralBox().BackColor = backgroundcolor;
            if (currentCharacterType != CharacterConfig.CHARACTERS.Gouki)
                GetPortraitLossBox().BackColor = backgroundcolor;
            GetPortraitVictoryBox().BackColor = backgroundcolor;
            if (currentCharacterType == CharacterConfig.CHARACTERS.Gouki)
            {
                GOU_teleportBox1.BackColor = backgroundcolor;
                GOU_teleportBox2.BackColor = backgroundcolor;
                GOU_teleportBox3.BackColor = backgroundcolor;
                load_sprite_teleports();
            }
            if (currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                psychopunchBox.BackColor = backgroundcolor;
                psychoprepBox.BackColor = backgroundcolor;
                crushertopBox.BackColor = backgroundcolor;
                crusherbottomBox.BackColor = backgroundcolor;
                crusherbackBox.BackColor = backgroundcolor;
            }

            if (z != null)
                z.setBackColor();

            if (c != null)
                c.setBackColor();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dictatorOldPunchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            fixDicatorOldPunches = !fixDicatorOldPunches;
        }
    }
}