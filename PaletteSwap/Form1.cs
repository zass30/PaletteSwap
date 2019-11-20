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

        public Form1()
        {
            InitializeComponent();
            textBox1.DragDrop += new DragEventHandler(textBox1_DragDrop);
            textBox1.DragEnter += new DragEventHandler(textBox1_DragEnter);
            palette = new byte[16*4];
            pal_dictionary = new Dictionary<string, int>();
            palcol_dict = new Dictionary<Color, int>();
            // set up palettes for all original colors? 
            // load up a palette string from mem, or an ACT file

            // create an image? each color is 0-16? 0 is alpha, and 1-16 are the colors index?
            // need a palette object, that can:
            // export string as mem
            // export as ACT
            // index as hat, suit, pads, etc
            pictureBox1.ImageLocation = @"..\..\Resources\dicstand1.png";
            pictureBox2.ImageLocation = @"..\..\Resources\dicportrait0.png";
            masterStand = new Bitmap(@"..\..\Resources\dicstand1.png");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Palette pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    pal_dest = Palette.PaletteFromMem(Palette.bis0Mem);
                    break;
                case 1:
                    pal_dest = Palette.PaletteFromMem(Palette.bis1Mem);
                    break;
                case 2:
                    pal_dest = Palette.PaletteFromMem(Palette.bis2Mem);
                    break;
                case 3:
                    pal_dest = Palette.PaletteFromMem(Palette.bis3Mem);
                    break;
                case 4:
                    pal_dest = Palette.PaletteFromMem(Palette.bis4Mem);
                    break;
                case 5:
                    pal_dest = Palette.PaletteFromMem(Palette.bis5Mem);
                    break;
                case 6:
                    pal_dest = Palette.PaletteFromMem(Palette.bis6Mem);
                    break;
                case 7:
                    pal_dest = Palette.PaletteFromMem(Palette.bis7Mem);
                    break;
                case 8:
                    pal_dest = Palette.PaletteFromMem(Palette.bis8Mem);
                    break;
            }
            swap_stand_bmp(pal_dest);
        }

        private void swap_stand_bmp( Palette pal_dest)
        {
            Bitmap imgsource = masterStand;
            Palette pal_src = Palette.PaletteFromMem(Palette.bis1Mem);
            Bitmap swappedBmp = Palette.PaletteSwap(imgsource, pal_src, pal_dest);
            pictureBox1.Image = swappedBmp;
            display_magnified_sprite();
        }

        private void load_palette()
        {
            Image Img = pictureBox1.Image;
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
            var bmp = magnify_sprite(pictureBox1.Image, 4);
            pictureBox3.Image = bmp;
        }

        private Bitmap magnify_sprite(Image img, int factor)
        {
            int neww = img.Width * factor;
            int newh = img.Height * factor;
            Bitmap newbmp = new Bitmap(neww, newh);

            Image Img = pictureBox1.Image;
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
            load_palette();

            Image Img = pictureBox1.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);                            
                    bmp.SetPixel(x, y, Color.FromArgb(gotColor.A, gotColor.R, gotColor.B, gotColor.G));
                }
            }
            pictureBox1.Image = bmp;
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
            swap_stand_bmp(pal_dest);
        }

        private void loadACT_Click(object sender, EventArgs e)
        {
            string newACT = textBox1.Text;
            Palette pal_dest = Palette.PaletteFromACT(newACT);
            swap_stand_bmp(pal_dest);
        }
    }
}
