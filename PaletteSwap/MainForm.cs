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

namespace PaletteSwap
{
    public partial class MainForm : Form
    {
        ZoomForm z;
        ColorSetForm c;
        PictureBox currentlySelectedZoomImage;
        PictureBox currentlySelectedColor;
        public CharacterColorSet characterColorSet;
        public Portrait currentPortrait;
        public Sprite currentSprite;
        bool skip_image_recolors = false;
        int DEFAULT_DROPDOWN_INDEX = 0;

        public MainForm()
        {
            InitializeComponent();
            EnableDragAndDrop();
            EnablePaintRefresh();
            loadImages();
            Setup();
            loadSpritesAndPalettesFromDropDown();

            var remapTable = currentSprite.StandingSpriteColorsRemapTable();
            Bitmap b = new Bitmap(Properties.Resources.dicstand1);
            imagepaint2(b, remapTable);
        }

        public void Setup()
        {
            characterColorSet = new CharacterColorSet();
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

        public void loadImages()
        {
            neutralStandBox.Image = Properties.Resources.dicstand1;
            psychopunchBox.Image = Properties.Resources.dicmp5;
            psychoprepBox.Image = Properties.Resources.dicpsychoprep5;
            crusherBox1.Image = Properties.Resources.diccrusher1_5;
            crusherBox2.Image = Properties.Resources.diccrusher2_5;
            portraitVictoryBox.Image = Properties.Resources.dicportraitwin5;
            portraitLossBox.Image = Properties.Resources.dicportraitloss5;
        }

        public void imagepaint(PaintEventArgs e, Bitmap b, ColorMap[] remapTable)
        {
            int width = b.Width;
            int height = b.Height;
            Graphics g = e.Graphics;
            Graphics gfb = Graphics.FromImage(b);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            g.DrawImage(b, new Rectangle(0, 0, width, height), 
                        0, 0, width, height,
                        GraphicsUnit.Pixel, imageAttributes); 
/*            gfb.DrawImage(b, new Rectangle(0, 0, width, height),
                        0, 0, width, height,
                        GraphicsUnit.Pixel, imageAttributes);
            psychopunchBox.BackgroundImage = b;*/
        }

        public void imagepaint2(Bitmap b, ColorMap[] remapTable)
        {
            int width = b.Width;
            int height = b.Height;
            Graphics gfb = Graphics.FromImage(b);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            gfb.DrawImage(b, new Rectangle(0, 0, width, height),
                                    0, 0, width, height,
                                    GraphicsUnit.Pixel, imageAttributes);
        }

        private void EnablePaintRefresh()
        {
            portraitVictoryBox.Paint += new System.Windows.Forms.PaintEventHandler(this.portraitBox_Paint);
            portraitLossBox.Paint += new System.Windows.Forms.PaintEventHandler(this.portraitLossBox_Paint);
            neutralStandBox.Paint += new System.Windows.Forms.PaintEventHandler(this.neutralStandBox_Paint);
            psychopunchBox.Paint += new System.Windows.Forms.PaintEventHandler(this.psychopunchBox_Paint);
            psychoprepBox.Paint += new System.Windows.Forms.PaintEventHandler(this.psychoprepBox_Paint);
            crusherBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.crusherBox1_Paint);
            crusherBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.crusherBox2_Paint);
        }

        private void neutralStandBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentSprite.StandingSpriteColorsRemapTable();
            imagepaint(e, Properties.Resources.dicstand1, remapTable);
/*            Bitmap b = new Bitmap(Properties.Resources.dicstand0);
            // imagepaint2(b, remapTable);
            neutralStandBox.BackgroundImage = b; // not working */
        }

        private void psychopunchBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentSprite.PsychoPunchColorsRemapTable();
            imagepaint(e, Properties.Resources.dicmp5, remapTable);
        }

        private void psychoprepBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentSprite.PsychoPrepColorsRemapTable();
            imagepaint(e, Properties.Resources.dicpsychoprep5, remapTable);
        }

        private void crusherBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentSprite.CrusherColorsRemapTable();
            imagepaint(e, Properties.Resources.diccrusher1_5, remapTable);
        }
        private void crusherBox2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentSprite.CrusherColorsRemapTable();
            imagepaint(e, Properties.Resources.diccrusher2_5, remapTable);
        }

        private void portraitBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = currentPortrait.VictoryColorsRemapTable();
            imagepaint(e, Properties.Resources.dicportraitwin5, remapTable);
        }

        private void portraitLossBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            imagepaint(e, Properties.Resources.dicportraitlosstop5, currentPortrait.LossTopColorsRemapTable());
            imagepaint(e, Properties.Resources.dicportraitlossbottom5, currentPortrait.LossBottomColorsRemapTable());
        }

        private void EnableDragAndDrop()
        {
            // drag and drop functionality
            label1.DragDrop += new DragEventHandler(label1_DragDrop);
            label1.DragEnter += new DragEventHandler(label1_DragEnter);
        }

        private void loadSpritesAndPalettesFromDropDown()
        {
            switch (colorSelectorBox.SelectedIndex)
            {
                case 0:
                    currentSprite = new Sprite(Sprite.bis0sprite);
                    currentPortrait = new Portrait(Portrait.bis0portrait);
                    break;
                case 1:
                    currentSprite = new Sprite(Sprite.bis1sprite);
                    currentPortrait = new Portrait(Portrait.bis1portrait);
                    break;
                case 2:
                    currentSprite = new Sprite(Sprite.bis2sprite);
                    currentPortrait = new Portrait(Portrait.bis2portrait);
                    break;
                case 3:
                    currentSprite = new Sprite(Sprite.bis3sprite);
                    currentPortrait = new Portrait(Portrait.bis3portrait);
                    break;
                case 4:
                    currentSprite = new Sprite(Sprite.bis4sprite);
                    currentPortrait = new Portrait(Portrait.bis4portrait);
                    break;
                case 5:
                    currentSprite = new Sprite(Sprite.bis5sprite);
                    currentPortrait = new Portrait(Portrait.bis5portrait);
                    break;
                case 6:
                    currentSprite = new Sprite(Sprite.bis6sprite);
                    currentPortrait = new Portrait(Portrait.bis6portrait);
                    break;
                case 7:
                    currentSprite = new Sprite(Sprite.bis7sprite);
                    currentPortrait = new Portrait(Portrait.bis7portrait);
                    break;
                case 8:
                    currentSprite = new Sprite(Sprite.bis8sprite);
                    currentPortrait = new Portrait(Portrait.bis8portrait);
                    break;
                case 9:
                    currentSprite = new Sprite(Sprite.bis9sprite);
                    currentPortrait = new Portrait(Portrait.bis9portrait);
                    break;
            }

            resetCurrentCharacterColorFromDropDown();
            reload_everything();
        }

        private void reload_everything()
        {
            skip_image_recolors = true;
            load_portrait_buttons();
            load_sprite_buttons();
            load_portrait_victory();
            load_portrait_loss();
            load_sprite_neutralstand();
            load_sprite_psychopunch();
            load_sprite_psychoprep();
            load_sprite_crushertop();
            load_sprite_crusherside();
            refreshZoom();
            skip_image_recolors = false;
        }

        private void load_sprite_neutralstand()
        {
            neutralStandBox.Refresh();
        }

        private void load_sprite_psychopunch()
        {
            psychopunchBox.Refresh();
        }

        private void load_sprite_psychoprep()
        {
            psychoprepBox.Refresh();
        }

        private void load_sprite_crushertop()
        {
            crusherBox1.Refresh();
        }

        private void load_sprite_crusherside()
        {
            crusherBox2.Refresh();
        }

        private void load_portrait_victory()
        {
            portraitVictoryBox.Refresh();
        }

        private void load_portrait_loss()
        {
            portraitLossBox.Refresh();
        }

        private void load_sprite_buttons()
        {
            var s = currentSprite;
            pal_sprite_skin1.BackColor = s.skin1;
            pal_sprite_skin2.BackColor = s.skin2;
            pal_sprite_skin3.BackColor = s.skin3;
            pal_sprite_skin4.BackColor = s.skin4;

            pal_sprite_stripe.BackColor = s.stripe;

            pal_sprite_pads1.BackColor = s.pads1;
            pal_sprite_pads2.BackColor = s.pads2;
            pal_sprite_pads3.BackColor = s.pads3;
            pal_sprite_pads4.BackColor = s.pads4;
            pal_sprite_pads5.BackColor = s.pads5;

            pal_sprite_costume1.BackColor = s.costume1;
            pal_sprite_costume2.BackColor = s.costume2;
            pal_sprite_costume3.BackColor = s.costume3;
            pal_sprite_costume4.BackColor = s.costume4;
            pal_sprite_costume5.BackColor = s.costume5;

            pal_sprite_psychoglow.BackColor = s.psychoglow;

            pal_sprite_psychopunch1.BackColor = s.psychopunch1;
            pal_sprite_psychopunch2.BackColor = s.psychopunch2;
            pal_sprite_psychopunch3.BackColor = s.psychopunch3;
            pal_sprite_psychopunch4.BackColor = s.psychopunch4;
            pal_sprite_psychopunch5.BackColor = s.psychopunch5;

            pal_sprite_crusherpads1.BackColor = s.crusherpads1;
            pal_sprite_crusherpads2.BackColor = s.crusherpads2;
            pal_sprite_crusherpads3.BackColor = s.crusherpads3;
            pal_sprite_crusherpads4.BackColor = s.crusherpads4;
            pal_sprite_crusherpads5.BackColor = s.crusherpads5;

            pal_sprite_crushercostume1.BackColor = s.crushercostume1;
            pal_sprite_crushercostume2.BackColor = s.crushercostume2;
            pal_sprite_crushercostume3.BackColor = s.crushercostume3;
            pal_sprite_crushercostume4.BackColor = s.crushercostume4;

            pal_sprite_crusherflame1.BackColor = s.crusherflame1;
            pal_sprite_crusherflame2.BackColor = s.crusherflame2;

            pal_sprite_crusherhands1.BackColor = s.crusherhands1;
            pal_sprite_crusherhands2.BackColor = s.crusherhands2;
        }

        private void load_portrait_buttons()
        {
            var p = currentPortrait;
            portrait_skin1.BackColor = p.skin1;
            portrait_skin2.BackColor = p.skin2;
            portrait_skin3.BackColor = p.skin3;
            portrait_skin4.BackColor = p.skin4;
            portrait_skin5.BackColor = p.skin5;
            portrait_skin6.BackColor = p.skin6;
            portrait_skin7.BackColor = p.skin7;

            portrait_teeth1.BackColor = p.teeth1;
            portrait_teeth2.BackColor = p.teeth2;
            portrait_teeth3.BackColor = p.teeth3;
            portrait_teeth4.BackColor = p.teeth4;

            portrait_costume1.BackColor = p.costume1;
            portrait_costume2.BackColor = p.costume2;
            portrait_costume3.BackColor = p.costume3;
            portrait_costume4.BackColor = p.costume4;

            portrait_costumeloss1.BackColor = p.costumeloss1;
            portrait_costumeloss2.BackColor = p.costumeloss2;
            portrait_costumeloss3.BackColor = p.costumeloss3;
            portrait_costumeloss4.BackColor = p.costumeloss4;

            portrait_piping1.BackColor = p.piping1;
            portrait_piping2.BackColor = p.piping2;
            portrait_piping3.BackColor = p.piping3;
            portrait_piping4.BackColor = p.piping4;

            portrait_pipingloss1.BackColor = p.pipingloss1;
            portrait_pipingloss2.BackColor = p.pipingloss2;
            portrait_pipingloss3.BackColor = p.pipingloss3;
            portrait_pipingloss4.BackColor = p.pipingloss4;

            portrait_blood1.BackColor = p.blood1;
            portrait_blood2.BackColor = p.blood2;
            portrait_blood3.BackColor = p.blood3;

        }

/*        private void debugImage()
        {
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
            var mp = new Bitmap(Properties.Resources.dicmp5);
        }

        private void createColorMasks()
        {
            var p_src = new Bitmap(@"..\..\Resources\dicstand1.png");
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);

            int i = 0;
            foreach (Color c in pal_dest.colors)
            {
                var newmask = Palette.createColorMask(p_src, c);
                newmask.Save(@"..\..\Resources\dicstandmask" + i + ".png");
                i++;
            }
        }

        private void overlayTransparency()
        {
            var p_src = new Bitmap(@"..\..\Resources\diccrusher2-5.png");
            var p_dest = new Bitmap(@"..\..\Resources\diccrusher2-1.png");

            var newimg = Palette.overlayTransparency(p_src, p_dest);

            try
            {
                p_src.Save(@"..\..\Resources\src.png");
                newimg.Save(@"..\..\Resources\dest.png");
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem saving the file." +
                    "Check the file permissions.");
            }
        }
        */

        private void LoadImageIntoPalette()
        {
            Image Img = psychopunchBox.Image;
            Bitmap bmp = new Bitmap(Img);
            var colors = currentSprite.PsychoPunchSpriteColorsArray();
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);
                    if (gotColor == colors.Last())
                    {
//                        int asdf = 0;
                    }
            
/*
                    string colstr = coltohex(gotColor);
                    if (pal_dictionary.ContainsKey(colstr))
                        pal_dictionary[colstr]++;
                    else
                        pal_dictionary[colstr] = 1;
                    if (palcol_dict.ContainsKey(gotColor))
                        palcol_dict[gotColor]++;
                    else
                        palcol_dict[gotColor] = 1;
                        */
                }
            }
        }

        private string coltohex(Color c)
        {
            string s = (c.G / 16).ToString("X1") + (c.B / 16).ToString("X1") + "0" + (c.R / 16).ToString("X1");
            return s;
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
            var sprite = Sprite.LoadFromColFormat(v[0]);
            this.currentSprite = sprite;
            var portrait = Portrait.LoadFromColFormat(v[1]);
            this.currentPortrait = portrait;
            reload_everything();
        }

        private void pal_square_click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
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
            switch (p.Name)
            {
                case "portrait_skin1":
                    currentPortrait.skin1 = c;
                    break;
                case "portrait_skin2":
                    currentPortrait.skin2 = c;
                    break;
                case "portrait_skin3":
                    currentPortrait.skin3 = c;
                    break;
                case "portrait_skin4":
                    currentPortrait.skin4 = c;
                    break;
                case "portrait_skin5":
                    currentPortrait.skin5 = c;
                    break;
                case "portrait_skin6":
                    currentPortrait.skin6 = c;
                    break;
                case "portrait_skin7":
                    currentPortrait.skin7 = c;
                    break;

                case "portrait_teeth1":
                    currentPortrait.teeth1 = c;
                    break;
                case "portrait_teeth2":
                    currentPortrait.teeth2 = c;
                    break;
                case "portrait_teeth3":
                    currentPortrait.teeth3 = c;
                    break;
                case "portrait_teeth4":
                    currentPortrait.teeth4 = c;
                    break;

                case "portrait_costume1":
                    currentPortrait.costume1 = c;
                    break;
                case "portrait_costume2":
                    currentPortrait.costume2 = c;
                    break;
                case "portrait_costume3":
                    currentPortrait.costume3 = c;
                    break;
                case "portrait_costume4":
                    currentPortrait.costume4 = c;
                    break;

                case "portrait_costumeloss1":
                    currentPortrait.costumeloss1 = c;
                    break;
                case "portrait_costumeloss2":
                    currentPortrait.costumeloss2 = c;
                    break;
                case "portrait_costumeloss3":
                    currentPortrait.costumeloss3 = c;
                    break;
                case "portrait_costumeloss4":
                    currentPortrait.costumeloss4 = c;
                    break;
                case "portrait_piping1":
                    currentPortrait.piping1 = c;
                    break;
                case "portrait_piping2":
                    currentPortrait.piping2 = c;
                    break;
                case "portrait_piping3":
                    currentPortrait.piping3 = c;
                    break;
                case "portrait_piping4":
                    currentPortrait.piping4 = c;
                    break;

                case "portrait_pipingloss1":
                    currentPortrait.pipingloss1 = c;
                    break;
                case "portrait_pipingloss2":
                    currentPortrait.pipingloss2 = c;
                    break;
                case "portrait_pipingloss3":
                    currentPortrait.pipingloss3 = c;
                    break;
                case "portrait_pipingloss4":
                    currentPortrait.pipingloss4 = c;
                    break;

                case "portrait_blood1":
                    currentPortrait.blood1 = c;
                    break;
                case "portrait_blood2":
                    currentPortrait.blood2 = c;
                    break;
                case "portrait_blood3":
                    currentPortrait.blood3 = c;
                    break;

            }
            load_portrait_victory();
            load_portrait_loss();
        }

        private void updateSpriteColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            switch (p.Name)
            {
                case "pal_sprite_skin1":
                    currentSprite.skin1 = c;
                    break;
                case "pal_sprite_skin2":
                    currentSprite.skin2 = c;
                    break;
                case "pal_sprite_skin3":
                    currentSprite.skin3 = c;
                    break;
                case "pal_sprite_skin4":
                    currentSprite.skin4 = c;
                    break;
                case "pal_sprite_pads1":
                    currentSprite.pads1 = c;
                    break;
                case "pal_sprite_pads2":
                    currentSprite.pads2 = c;
                    break;
                case "pal_sprite_pads3":
                    currentSprite.pads3 = c;
                    break;
                case "pal_sprite_pads4":
                    currentSprite.pads4 = c;
                    break;
                case "pal_sprite_pads5":
                    currentSprite.pads5 = c;
                    break;
                case "pal_sprite_costume1":
                    currentSprite.costume1 = c;
                    break;
                case "pal_sprite_costume2":
                    currentSprite.costume2 = c;
                    break;
                case "pal_sprite_costume3":
                    currentSprite.costume3 = c;
                    break;
                case "pal_sprite_costume4":
                    currentSprite.costume4 = c;
                    break;
                case "pal_sprite_costume5":
                    currentSprite.costume5 = c;
                    break;
                case "pal_sprite_stripe":
                    currentSprite.stripe = c;
                    break;
                case "pal_sprite_psychoglow":
                    currentSprite.psychoglow = c;
                    break;
                case "pal_sprite_psychopunch1":
                    currentSprite.psychopunch1 = c;
                    break;
                case "pal_sprite_psychopunch2":
                    currentSprite.psychopunch2 = c;
                    break;
                case "pal_sprite_psychopunch3":
                    currentSprite.psychopunch3 = c;
                    break;
                case "pal_sprite_psychopunch4":
                    currentSprite.psychopunch4 = c;
                    break;
                case "pal_sprite_psychopunch5":
                    currentSprite.psychopunch5 = c;
                    break;
            }

            load_sprite_neutralstand();
            load_sprite_psychopunch();
            load_sprite_psychoprep();
        }

        private void updateSpriteCrusherColor(Color c, PictureBox p)
        {
            currentlySelectedColor = p;
            switch (p.Name)
            {
                case "pal_sprite_crusherpads1":
                    currentSprite.crusherpads1 = c;
                    break;
                case "pal_sprite_crusherpads2":
                    currentSprite.crusherpads2 = c;
                    break;
                case "pal_sprite_crusherpads3":
                    currentSprite.crusherpads3 = c;
                    break;
                case "pal_sprite_crusherpads4":
                    currentSprite.crusherpads4 = c;
                    break;
                case "pal_sprite_crusherpads5":
                    currentSprite.crusherpads5 = c;
                    break;
                case "pal_sprite_crushercostume1":
                    currentSprite.crushercostume1 = c;
                    break;
                case "pal_sprite_crushercostume2":
                    currentSprite.crushercostume2 = c;
                    break;
                case "pal_sprite_crushercostume3":
                    currentSprite.crushercostume3 = c;
                    break;
                case "pal_sprite_crushercostume4":
                    currentSprite.crushercostume4 = c;
                    break;
                case "pal_sprite_crusherflame1":
                    currentSprite.crusherflame1 = c;
                    break;
                case "pal_sprite_crusherflame2":
                    currentSprite.crusherflame2 = c;
                    break;
                case "pal_sprite_crusherhands1":
                    currentSprite.crusherhands1 = c;
                    break;
                case "pal_sprite_crusherhands2":
                    currentSprite.crusherhands2 = c;
                    break;
            }

            load_sprite_crusherside();
            load_sprite_crushertop();
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
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveCharacterColorToSet();
        }

        private void saveCharacterColorToSet()
        {
            characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s = currentSprite;
            characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p = currentPortrait;
        }


        private void resetCurrentCharacterColorFromDropDown()
        {
            if (characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s != null)
                currentSprite = characterColorSet.characterColors[colorSelectorBox.SelectedIndex].s;
            if (characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p != null)
                currentPortrait = characterColorSet.characterColors[colorSelectorBox.SelectedIndex].p;

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
                    characterColorSet = CharacterColorSet.CharacterColorSetFromZipStream(fileStream);
                    resetCurrentCharacterColorFromDropDown();
                    reload_everything();
                    fileStream.Close();
                }
            }
        }

        private void savePatchedRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRomToolFormat(sender, e, false);
        }

        private void savePhoenixRomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            savePatchedRomToolFormat(sender, e, true);
        }

        // would like to encapsulate in object 
        private void savePatchedRomToolFormat(object sender, EventArgs e, bool isPhoenix)
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
                        string _03filename;
                        string _04filename;
                        string _06filename;

                        byte[] p_stream;
                        byte[] s_stream;
                        byte[] punches_stream;
                        punches_stream = characterColorSet.old_bison_punches_stream06();
                        if (isPhoenix)
                        {
                            _03filename = "sfxjd.03c";
                            _04filename = "sfxjd.04a";
                            _06filename = "sfxjd.06a";
                            p_stream = characterColorSet.portraits_stream03phoenix();
                            s_stream = characterColorSet.sprites_stream04phoenix();
                        }
                        else
                        {
                            _03filename = "sfxe.03c";
                            _04filename = "sfxe.04a";
                            _06filename = "sfxe.06a";

                            p_stream = characterColorSet.portraits_stream03();
                            s_stream = characterColorSet.sprites_stream04();
                            
                        }

                        var _03file = archive.CreateEntry(_03filename);
                        using (var entryStream = _03file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            //var p_stream = characterColorSet.portraits_stream03();
                            var c = entryStream.CanSeek;
                            entryStream.Write(p_stream, 0, p_stream.Length);
                        }

                        var _04file = archive.CreateEntry(_04filename);
                        using (var entryStream = _04file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            //var s_stream = characterColorSet.sprites_stream04();
                            var c = entryStream.CanSeek;
                            entryStream.Write(s_stream, 0, s_stream.Length);
                        }

                        var _06file = archive.CreateEntry(_06filename);
                        using (var entryStream = _06file.Open())
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            //var s_stream = characterColorSet.sprites_stream04();
                            var c = entryStream.CanSeek;
                            entryStream.Write(punches_stream, 0, punches_stream.Length);
                        }
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
    }
}
