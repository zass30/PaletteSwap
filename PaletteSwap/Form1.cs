using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaletteSwap
{
    public partial class Form1 : Form
    {
        byte[] palette;
        Dictionary<string, int> pal_dictionary;

        public Form1()
        {
            InitializeComponent();
            palette = new byte[16*4];
            pal_dictionary = new Dictionary<string, int>();
            // set up palettes for all original colors? 
            // load up a palette string from mem, or an ACT file
            
            // create an image? each color is 0-16? 0 is alpha, and 1-16 are the colors index?
            // need a palette object, that can:
            // export string as mem
            // export as ACT
            // index as hat, suit, pads, etc

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @"..\..\Resources\dicstand1.png";
            pictureBox2.ImageLocation = @"..\..\Resources\dicportrait0.png";
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
    }
}
