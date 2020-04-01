using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace PaletteSwap
{
    public partial class MainForm : Form
    {
        ZoomForm z;
        ColorSetForm c;
        PictureBox previouslySelectedSquare;
        PictureBox currentlySelectedZoomImage;
        PictureBox currentlySelectedColor;
        public CharacterSet characterSet;
        public Character currentCharacter;
        bool skip_image_recolors = false;
        int DEFAULT_DROPDOWN_INDEX = 0;
        enum ROMSTYLE { us, japanese, phoenix };
        Regex rx = new Regex(@"[^_]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public MainForm()
        {
            InitializeComponent();
            EnableDragAndDrop();
            Setup();
            loadSpritesAndPalettesFromDropDown();
        }

        public void Setup()
        {
            characterSet = CharacterSet.GenerateDictatorCharacterSet();
            for (int i = 0; i < 10; i++)
            {
                colorSelectorBox.SelectedIndex = i;
                loadSpritesAndPalettesFromDropDown();
                saveCharacterColorToSet();
            }
            colorSelectorBox.SelectedIndex = DEFAULT_DROPDOWN_INDEX;
            z = new ZoomForm(this);
            c = new ColorSetForm(this);
        }

        private void EnableDragAndDrop()
        {
            // drag and drop functionality
            label1.DragDrop += new DragEventHandler(label1_DragDrop);
            label1.DragEnter += new DragEventHandler(label1_DragEnter);
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
            load_sprite_neutralstandredo();
            load_sprite_load_sprite_psychopunchredo();
            load_sprite_load_sprite_psychoprepredo();
            load_sprite_load_sprite_crushertopredo();
            load_sprite_load_sprite_crusherbottomredo();
            load_portrait_victoryredo();
            load_portrait_lossredo();

            refreshZoom();
            skip_image_recolors = false;
        }

        private void load_sprite_neutralstandredo()
        {
            neutralStandBoxRedo.BackgroundImage = currentCharacter.sprite.GetBitmap("neutral");
        }

        private void load_sprite_load_sprite_psychopunchredo()
        {
            psychopunchBoxRedo.BackgroundImage = currentCharacter.sprite.GetBitmap("psychopunch");
        }

        private void load_sprite_load_sprite_psychoprepredo()
        {
            psychoprepBoxRedo.BackgroundImage = currentCharacter.sprite.GetBitmap("psychoprep");
        }

        private void load_sprite_load_sprite_crushertopredo()
        {
            crushertopBoxRedo.BackgroundImage = currentCharacter.sprite.GetBitmap("crushertop");
        }

        private void load_sprite_load_sprite_crusherbottomredo()
        {
            crusherbottomBoxRedo.BackgroundImage = currentCharacter.sprite.GetBitmap("crusherbottom");
        }

        private void load_portrait_victoryredo()
        {
            portraitVictoryBoxRedo.BackgroundImage = currentCharacter.portrait.GetBitmap("victory");
        }

        private void load_portrait_lossredo()
        {
            Bitmap result = new Bitmap(portraitLossBoxRedo.Width, portraitLossBoxRedo.Height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(currentCharacter.portrait.GetBitmap("losstop"), Point.Empty);
                g.DrawImage(currentCharacter.portrait.GetBitmap("lossbottom"), Point.Empty);
            }
            portraitLossBoxRedo.BackgroundImage = result;
        }
       

        private void load_sprite_buttons()
        {
            pal_sprite_skin1.BackColor = currentCharacter.sprite.GetColor("skin1");
            pal_sprite_skin2.BackColor = currentCharacter.sprite.GetColor("skin2");
            pal_sprite_skin3.BackColor = currentCharacter.sprite.GetColor("skin3");
            pal_sprite_skin4.BackColor = currentCharacter.sprite.GetColor("skin4");

            pal_sprite_stripe.BackColor = currentCharacter.sprite.GetColor("stripe");

            pal_sprite_pads1.BackColor = currentCharacter.sprite.GetColor("pads1");
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
        }

        private void load_portrait_buttons()
        {
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
                            newbmp.SetPixel(factor * x+i, factor * y+j, gotColor);
                        }
                    }
                }
            }

            return newbmp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSpritesAndPalettesFromDropDown();
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
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
//            var sprite = Sprite.LoadFromColFormat(v[0]);
//            this.currentSprite = sprite;
//            var portrait = Portrait.LoadFromColFormat(v[1]);
 //           this.currentPortrait = portrait;
            currentCharacter.loadFromColFormat(colstr);
            reload_everything();
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
            currentlySelectedZoomImage = p;
            if (z.IsDisposed)
                z = new ZoomForm(this);
            z.Show();
            img_type i = img_type.neutral;
            switch (p.Name)
            {
                case "neutralStandBox":
                    i = img_type.neutral;
                    break;
                case "psychopunchBox":
                    i = img_type.psychopunch;
                    break;
                case "psychoprepBox":
                    i = img_type.psychoprep;
                    break;
                case "crusherBox1":
                    i = img_type.crushertop;
                    break;
                case "crusherBox2":
                    i = img_type.crushertop;
                    break;
                case "portraitVictoryBox":
                    i = img_type.victoryportrait;
                    break;
                case "portraitLossBox":
                    i = img_type.lossportrait;
                    break;
            }
            z.displayZoomImage((Bitmap)p.Image, i);

            z.refreshZoomBox();
        }

        private void refreshZoom()
        {
            if (currentlySelectedZoomImage == null)
                return;
            z.refreshZoomBox();
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

            load_portrait_victoryredo();
            load_portrait_lossredo();
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
            load_sprite_neutralstandredo();
            load_sprite_load_sprite_psychopunchredo();
            load_sprite_load_sprite_psychoprepredo();
            load_sprite_load_sprite_crushertopredo();
        }

        private void updateSpriteCrusherColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            string label = extractLabel(p.Name);
            currentCharacter.sprite.SetColor(label, c);

            load_sprite_load_sprite_crushertopredo();
            load_sprite_load_sprite_crusherbottomredo();
        }

        private void portrait_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updatePortraitColor(c, p);
            refreshZoom();
        }

        private void sprite_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updateSpriteColor(c, p);
            refreshZoom();
        }

        private void spriteCrusher_BackColorChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            var p = (PictureBox)sender;
            var c = p.BackColor;
            updateSpriteCrusherColor(c, p);
            refreshZoom();
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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Color Files (*.col)|*.col";
            saveFileDialog1.Title = "Save a color File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        string s = currentSprite.ToColFormat();
                        var b = Encoding.ASCII.GetBytes(s);
                        fs.Seek(0, SeekOrigin.End);
                        await fs.WriteAsync(b, 0, b.Length);
                        s = ":";
                        b = Encoding.ASCII.GetBytes(s);
                        await fs.WriteAsync(b, 0, b.Length);
                        s = currentPortrait.ToColFormat();
                        b = Encoding.ASCII.GetBytes(s);
                        await fs.WriteAsync(b, 0, b.Length);
                        break;
                }

                fs.Close();
            }*/
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCharacterColorToSet();
        }

        private void saveCharacterColorToSet()
        {
//            characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s = currentSprite;
//            characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p = currentPortrait;
            characterSet.characterColors[colorSelectorBox.SelectedIndex] = currentCharacter;
        }


        private void resetCurrentCharacterColorFromDropDown()
        {
/*            if (characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s != null)
                currentSprite = characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s;
            if (characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p != null)
                currentPortrait = characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p;
                */
            currentCharacter = characterSet.characterColors[colorSelectorBox.SelectedIndex];
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Palette Swapper, by Zass");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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

                    //Read the contents of the file into a stream
                    FileStream fileStream = (System.IO.FileStream)openFileDialog.OpenFile();
//                    characterColorSet = CharacterColorSet.CharacterColorSetFromZipStream(fileStream);
                    characterSet = CharacterSet.CharacterColorSetFromZipStream(fileStream);
                    resetCurrentCharacterColorFromDropDown();
                    reload_everything();
                    fileStream.Close();
                }
            }
        }

        private void savePatchedRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRomToolFormat(sender, e, ROMSTYLE.us);
        }

        private void savePhoenixRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRomToolFormat(sender, e, ROMSTYLE.phoenix);
        }

        private void saveJapaneseRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePatchedRomToolFormat(sender, e, ROMSTYLE.japanese);
        }

        // would like to encapsulate in object 
        private void savePatchedRomToolFormat(object sender, EventArgs e, ROMSTYLE r)
        {/*
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
                        byte[] punches_stream;

                        if (r == ROMSTYLE.phoenix)
                        {
                            _03filename = "sfxjd.03c";
                            _04filename = "sfxjd.04a";
                            _06filename = "sfxjd.06a";
                            p_stream = characterColorSet.portraits_stream03phoenix();
                            s_stream = characterColorSet.sprites_stream04phoenix();
                            punches_stream = characterColorSet.old_bison_punches_stream06phoenix();
                        }
                        else if (r == ROMSTYLE.us)
                        {
                            _03filename = "sfxe.03c";
                            _04filename = "sfxe.04a";
                            _06filename = "sfxe.06a";

                            p_stream = characterColorSet.portraits_stream03();
                            s_stream = characterColorSet.sprites_stream04();
                            punches_stream = characterColorSet.old_bison_punches_stream06();
                        }
                        else if (r == ROMSTYLE.japanese)
                        {
                            _03filename = "sfxj.03c";
                            _04filename = "sfxj.04a";
                            _06filename = "sfxj.06a";

                            p_stream = characterColorSet.portraits_stream03japanese();
                            s_stream = characterColorSet.sprites_stream04japanese();
                            punches_stream = characterColorSet.old_bison_punches_stream06japanese();
                        }
                        else
                        {
                                _03filename = "sfxj.03c";
                                _04filename = "sfxj.04a";
                                _06filename = "sfxj.06a";

                                p_stream = characterColorSet.portraits_stream03japanese();
                                s_stream = characterColorSet.sprites_stream04japanese();
                                punches_stream = characterColorSet.old_bison_punches_stream06japanese();
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
                            entryStream.Write(punches_stream, 0, punches_stream.Length);
                        }
                    }
                }
            }*/
        }

        private void colorSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (c.IsDisposed)
                c = new ColorSetForm(this);
            c.Reload();
            c.Show();
        }

        private void savePatchedRomRedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ROMSTYLE r = ROMSTYLE.us;
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

                        byte[] p_stream;
                        byte[] s_stream;

                        _03filename = "sfxe.03c";
                        _04filename = "sfxe.04a";

                        p_stream = characterSet.portraits_stream03();
                        s_stream = characterSet.sprites_stream04();

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

                        /*var _06file = archive.CreateEntry(_06filename);
                        using (var entryStream = _06file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            var c = entryStream.CanSeek;
                            entryStream.Write(punches_stream, 0, punches_stream.Length);
                        }*/
                    }
                }
            }
        }
    }
}
