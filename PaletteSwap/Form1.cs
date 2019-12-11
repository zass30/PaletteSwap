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


namespace PaletteSwap
{
    public partial class Form1 : Form
    {
        byte[] palette;
        Dictionary<string, int> pal_dictionary;
        Dictionary<Color, int> palcol_dict;
        Bitmap masterStand;
        Bitmap[] standMasks;

        public Form1()
        {
            InitializeComponent();
            EnableDragAndDrop();

            palette = new byte[16*4];
            pal_dictionary = new Dictionary<string, int>();
            palcol_dict = new Dictionary<Color, int>();

            spriteBox.ImageLocation = @"..\..\Resources\dicstand1.png";
            psychopunchBox.ImageLocation = @"..\..\Resources\dicmp5.png";
            psychoprepBox.ImageLocation = @"..\..\Resources\dicpsychoprep5.png";
            crusherBox1.ImageLocation = @"..\..\Resources\diccrusher1-5.png";
            crusherBox2.ImageLocation = @"..\..\Resources\diccrusher2-5.png";
            portraitBox.ImageLocation = @"..\..\Resources\dicportraitwin5.png";
            portraitLossBox.ImageLocation = @"..\..\Resources\dicportraitloss5.png";
            masterStand = new Bitmap(@"..\..\Resources\dicstand1.png");
            standMasks = new Bitmap[15];
            for (int i = 0; i < 15; i++)
            {
                standMasks[i] = new Bitmap(@"..\..\Resources\dicstandmask" + i + ".png");
            }
            comboBox1.SelectedIndex = 0;
            loadPalette();
            //            createColorMasks();
            overlayTransparency();

        }

        private void EnableDragAndDrop()
        {
            // drag and drop functionality
            textBox1.DragDrop += new DragEventHandler(textBox1_DragDrop);
            textBox1.DragEnter += new DragEventHandler(textBox1_DragEnter);
        }

        private void loadPalette()
        {
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
            Portrait portrait_dest = new Portrait(Portrait.bis4portrait);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    pal_dest = Palette.PaletteFromMem(Palette.bis0Mem);
                    portrait_dest = new Portrait(Portrait.bis0portrait);
                    break;
                case 1:
                    pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
                    portrait_dest = new Portrait(Portrait.bis1portrait);
                    break;
                case 2:
                    pal_dest = Palette.PaletteFromMem(Palette.bis2Mem);
                    portrait_dest = new Portrait(Portrait.bis2portrait);
                    break;
                case 3:
                    pal_dest = Palette.PaletteFromMem(Palette.bis3Mem);
                    portrait_dest = new Portrait(Portrait.bis3portrait);
                    break;
                case 4:
                    pal_dest = Palette.PaletteFromMem(Palette.bis4Mem);
                    portrait_dest = new Portrait(Portrait.bis4portrait);
                    break;
                case 5:
                    pal_dest = Palette.PaletteFromMem(Palette.bis5Mem);
                    portrait_dest = new Portrait(Portrait.bis5portrait);
                    break;
                case 6:
                    pal_dest = Palette.PaletteFromMem(Palette.bis6Mem);
                    portrait_dest = new Portrait(Portrait.bis6portrait);
                    break;
                case 7:
                    pal_dest = Palette.PaletteFromMem(Palette.bis7Mem);
                    portrait_dest = new Portrait(Portrait.bis7portrait);
                    break;
                case 8:
                    pal_dest = Palette.PaletteFromMem(Palette.bis8Mem);
                    portrait_dest = new Portrait(Portrait.bis8portrait);
                    break;
            }
            swap_standing_sprite(pal_dest);
            load_portrait_buttons(portrait_dest);
        }

        private void load_portrait_buttons(Portrait p)
        {
            portrait_skin1.BackColor = p.face1;
            portrait_skin2.BackColor = p.face2;
            portrait_skin3.BackColor = p.face3;
            portrait_skin4.BackColor = p.face4;
            portrait_skin5.BackColor = p.face5;
            portrait_skin6.BackColor = p.face6;
            portrait_skin7.BackColor = p.face7;

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

        private void button1_Click(object sender, EventArgs e)
        {
            loadPalette();
        }

        private void swap_standing_sprite( Palette pal_dest)
        {
            Bitmap imgsource = masterStand;
            Palette pal_src = Palette.PaletteFromMem(Palette.bis1Mem);
            Bitmap swappedBmp = Palette.PaletteSwap(imgsource, pal_src, pal_dest);
            spriteBox.Image = swappedBmp;
            pal_sprite_skin1.BackColor = pal_dest.colors[11];
            pal_sprite_skin2.BackColor = pal_dest.colors[12];
            pal_sprite_skin3.BackColor = pal_dest.colors[13];
            pal_sprite_skin4.BackColor = pal_dest.colors[14];

            pal_sprite_pads1.BackColor = pal_dest.colors[8];
            pal_sprite_pads2.BackColor = pal_dest.colors[9];
            pal_sprite_pads3.BackColor = pal_dest.colors[10];
            pal_sprite_pads4.BackColor = pal_dest.colors[6];
            pal_sprite_pads5.BackColor = pal_dest.colors[0];

            pal_sprite_cost1.BackColor = pal_dest.colors[5];
            pal_sprite_cost2.BackColor = pal_dest.colors[4];
            pal_sprite_cost3.BackColor = pal_dest.colors[3];
            pal_sprite_cost4.BackColor = pal_dest.colors[2];
            pal_sprite_cost5.BackColor = pal_dest.colors[1];

            pal_sprite_stripe1.BackColor = pal_dest.colors[7];

            display_magnified_sprite();

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
            var p_src = new Bitmap(@"..\..\Resources\dicmp1.png");
            var p_dest = new Bitmap(@"..\..\Resources\dicmp5.png");


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
            Image Img = portraitBox.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);
                    string colstr = coltohex(gotColor) ;
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

        private void display_magnified_sprite()
        {
            var bmp = magnify_sprite(spriteBox.Image, 4);
            pictureBox3.Image = bmp;
        }

        private Bitmap magnify_sprite(Image img, int factor)
        {
            int neww = img.Width * factor;
            int newh = img.Height * factor;
            Bitmap newbmp = new Bitmap(neww, newh);

            Image Img = spriteBox.Image;
            Bitmap bmp = new Bitmap(Img);
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

            Image Img = spriteBox.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);                            
                    bmp.SetPixel(x, y, Color.FromArgb(gotColor.A, gotColor.R, gotColor.B, gotColor.G));
                }
            }
            spriteBox.Image = bmp;
            display_magnified_sprite();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
            swap_standing_sprite(pal_dest);
        }

        private void loadACT_Click(object sender, EventArgs e)
        {
            string newACT = textBox1.Text;
            Palette pal_dest = Palette.PaletteFromACT(newACT);
            swap_standing_sprite(pal_dest);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pal_square_click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            Color c = p.BackColor;
            int r = c.R / 17;
            int g = c.G / 17;
            int b = c.B / 17;

            pal_val_R.Text = r.ToString();
            pal_val_G.Text = g.ToString();
            pal_val_B.Text = b.ToString();

            trackBarR.Value = r;
            trackBarG.Value = g;
            trackBarB.Value = b;

        }

        private void label6_Click(object sender, EventArgs e)
        {

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
        }

        private void pal_val_G_TextChanged(object sender, EventArgs e)
        {
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
        }

        private void pal_val_B_TextChanged(object sender, EventArgs e)
        {
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
