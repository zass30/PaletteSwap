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

namespace PaletteSwap
{
    public partial class Form1 : Form
    {
        byte[] palette;
        Dictionary<string, int> pal_dictionary;
        Dictionary<Color, int> palcol_dict;
        Bitmap masterStand;
        ZoomForm z;
        PictureBox currentlySelectedZoomImage;
        PictureBox currentlySelectedColor;
        Portrait currentPortrait;
        Sprite currentSprite;
        bool skip_image_recolors = false;

        public Form1()
        {
            InitializeComponent();
            EnableDragAndDrop();
            portraitBox.Paint += new System.Windows.Forms.PaintEventHandler(this.portraitBox_Paint);


            palette = new byte[16*4];
            pal_dictionary = new Dictionary<string, int>();
            palcol_dict = new Dictionary<Color, int>();

            neutralStandBox.Image = Properties.Resources.dicstand1;
            psychopunchBox.Image = Properties.Resources.dicmp5;
            psychoprepBox.Image = Properties.Resources.dicpsychoprep5;
            crusherBox1.Image = Properties.Resources.diccrusher1_5;
            crusherBox2.Image = Properties.Resources.diccrusher2_5;
//            portraitBox.Image = Properties.Resources.dicportraitwin5;
            portraitLossBox.Image = Properties.Resources.dicportraitloss5;
            masterStand = new Bitmap(Properties.Resources.dicstand1);
            comboBox1.SelectedIndex = 5;
            loadPalette();
//            overlayTransparency();
            z = new ZoomForm();

        }

        private void portraitBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Bitmap portrait_orig = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            int width = portrait_orig.Width;
            int height = portrait_orig.Height;

            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            ImageAttributes imageAttributes = new ImageAttributes();
            var orig = new Portrait(Portrait.bis5portrait);
            var orig_colors = orig.VictoryColorsArray();
            var dest_colors = currentPortrait.VictoryColorsArray();
            ColorMap[] remapTable = new ColorMap[orig_colors.Length];

            // change this so remaptable is a method of portrait.
            for (int j = 0; j < orig_colors.Length; j++)
            {
                ColorMap colorMap = new ColorMap();
                colorMap.OldColor = orig_colors[j];
                colorMap.NewColor = dest_colors[j];
                remapTable[j] = colorMap;
            }

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            g.DrawImage(portrait_orig, 
                new Rectangle(0, 0, width, height), 
                0, 0, width, height,
                GraphicsUnit.Pixel,
                imageAttributes);

            // make same call on zoomwindow, which needs logic to know what portrait to draw

            // this seems to work but slow?
//            portraitBox.DrawToBitmap(portrait_orig, new Rectangle(0, 0, width, height));
//            portraitBox.Image = portrait_orig;

        }

        private void EnableDragAndDrop()
        {
            // drag and drop functionality
            textBox1.DragDrop += new DragEventHandler(textBox1_DragDrop);
            textBox1.DragEnter += new DragEventHandler(textBox1_DragEnter);
        }

        private void loadPalette()
        {
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis4Mem);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    pal_dest = Palette.PaletteFromMem(Palette.bis0Mem);
                    currentSprite = new Sprite(Sprite.bis0sprite);
                    currentPortrait = new Portrait(Portrait.bis0portrait);
                    break;
                case 1:
                    pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
                    currentSprite = new Sprite(Sprite.bis1sprite);
                    currentPortrait = new Portrait(Portrait.bis1portrait);
                    break;
                case 2:
                    pal_dest = Palette.PaletteFromMem(Palette.bis2Mem);
                    currentSprite = new Sprite(Sprite.bis2sprite);
                    currentPortrait = new Portrait(Portrait.bis2portrait);
                    break;
                case 3:
                    pal_dest = Palette.PaletteFromMem(Palette.bis3Mem);
                    currentSprite = new Sprite(Sprite.bis3sprite);
                    currentPortrait = new Portrait(Portrait.bis3portrait);
                    break;
                case 4:
                    pal_dest = Palette.PaletteFromMem(Palette.bis4Mem);
                    currentSprite = new Sprite(Sprite.bis4sprite);
                    currentPortrait = new Portrait(Portrait.bis4portrait);
                    break;
                case 5:
                    pal_dest = Palette.PaletteFromMem(Palette.bis5Mem);
                    currentSprite = new Sprite(Sprite.bis5sprite);
                    currentPortrait = new Portrait(Portrait.bis5portrait);
                    break;
                case 6:
                    pal_dest = Palette.PaletteFromMem(Palette.bis6Mem);
                    currentSprite = new Sprite(Sprite.bis6sprite);
                    currentPortrait = new Portrait(Portrait.bis6portrait);
                    break;
                case 7:
                    pal_dest = Palette.PaletteFromMem(Palette.bis7Mem);
                    currentSprite = new Sprite(Sprite.bis7sprite);
                    currentPortrait = new Portrait(Portrait.bis7portrait);
                    break;
                case 8:
                    pal_dest = Palette.PaletteFromMem(Palette.bis8Mem);
                    currentSprite = new Sprite(Sprite.bis8sprite);
                    currentPortrait = new Portrait(Portrait.bis8portrait);
                    break;
                case 9:
                    pal_dest = Palette.PaletteFromMem(Palette.bis8Mem);
                    currentSprite = new Sprite(Sprite.bis9sprite);
                    currentPortrait = new Portrait(Portrait.bis9portrait);
                    break;
            }
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
            skip_image_recolors = false;
        }

        private void load_sprite_neutralstand()
        {
            var b = currentSprite.GenerateStandingSprite();
            neutralStandBox.Image = b;
        }

        private void load_sprite_psychopunch()
        {
            var b = currentSprite.GeneratePsychoPunchSprite();
            psychopunchBox.Image = b;
        }

        private void load_sprite_psychoprep()
        {
            var b = currentSprite.GeneratePsychoPrepSprite();
            psychoprepBox.Image = b;
        }

        private void load_sprite_crushertop()
        {
            var b = currentSprite.GenerateCrusherTopSprite();
            crusherBox1.Image = b;
        }

        private void load_sprite_crusherside()
        {
            var b = currentSprite.GenerateCrusherSideSprite();
            crusherBox2.Image = b;
        }

        private void load_portrait_victory()
        {
            portraitBox.Refresh();
//            portraitBox.Image = currentPortrait.GenerateVictoryPortrait();
        }

        private void load_portrait_loss()
        {
            portraitLossBox.Image = currentPortrait.GenerateLossPortrait();
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

        private void debugImage()
        {
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
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

        private void LoadImageIntoPalette()
        {
            Image Img = psychopunchBox.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);
                    string colstr = coltohex(gotColor);
                    if (pal_dictionary.ContainsKey(colstr))
                        pal_dictionary[colstr]++;
                    else
                        pal_dictionary[colstr] = 1;
                    if (palcol_dict.ContainsKey(gotColor))
                        palcol_dict[gotColor]++;
                    else
                        palcol_dict[gotColor] = 1;
                }
            }
        }

        private string coltohex(Color c)
        {
            string s = (c.G / 16).ToString("X1") + (c.B / 16).ToString("X1") + "0" + (c.R / 16).ToString("X1");
            return s;
        }

        private Bitmap magnify_sprite(Image img, int factor)
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

        private void button2_Click(object sender, EventArgs e)
        {
            LoadImageIntoPalette();

            Image Img = neutralStandBox.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);                            
                    bmp.SetPixel(x, y, Color.FromArgb(gotColor.A, gotColor.R, gotColor.B, gotColor.G));
                }
            }
            neutralStandBox.Image = bmp;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPalette();
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            byte[] lineasbytes = File.ReadAllBytes(s[0]);
            byte[] reducedline = lineasbytes.Take(16 * 3).Skip(3).ToArray();
            textBox1.Text = Palette.ACTtoText(reducedline);
            string newACT = textBox1.Text;
            Palette pal_dest = Palette.PaletteFromACT(newACT);
        }

        private void loadACT_Click(object sender, EventArgs e)
        {
            string newACT = textBox1.Text;
            Palette pal_dest = Palette.PaletteFromACT(newACT);
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
            var bmp = magnify_sprite(p.Image, 6);
            if (z.IsDisposed)
                z = new ZoomForm();
            z.Show();
            z.displayZoomImage(bmp);
        }

        private void refreshZoom()
        {
            if (currentlySelectedZoomImage == null)
                return;

            var bmp = magnify_sprite(currentlySelectedZoomImage.Image, 6);
            if (z.IsDisposed)
                z = new ZoomForm();
            z.Show();
            z.displayZoomImage(bmp);
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

        private void pal_val_R_TextChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            int r = 0;
            try
            {
                r = Int32.Parse(pal_val_R.Text);
            }
            catch (Exception)
            {
                // nothing
            }
            r = clamp(r);
            trackBarR.Value = r;
            var c = currentlySelectedColor.BackColor;
            var newcolor = Color.FromArgb(c.A, r*17, c.G, c.B);
            currentlySelectedColor.BackColor = newcolor;
        }

        private void pal_val_G_TextChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            int g = 0;        
            try
            {
                g = Int32.Parse(pal_val_G.Text);
            }
            catch (Exception)
            {
                // nothing
            }
            g = clamp(g);
            trackBarG.Value = g;
            var c = currentlySelectedColor.BackColor;
            var newcolor = Color.FromArgb(c.A, c.R, g*17, c.B);
            currentlySelectedColor.BackColor = newcolor;
        }

        private void pal_val_B_TextChanged(object sender, EventArgs e)
        {
            if (skip_image_recolors)
                return;
            int b = 0;
            try
            {
                b = Int32.Parse(pal_val_B.Text);
            }
            catch (Exception)
            {
                // nothing
            }
            b = clamp(b);
            trackBarB.Value = b;
            var c = currentlySelectedColor.BackColor;
            var newcolor = Color.FromArgb(c.A, c.R, c.G, b*17);
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
//            load_portrait_loss();
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
            // problem is that this gets triggered every time we switch from drop down on each portrait button
            // above is a workaround but still slow
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
    }
}
