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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           pictureBox1.ImageLocation = @"..\..\Resources\ssf2t016.png";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image Img = pictureBox1.Image;
            Bitmap bmp = new Bitmap(Img);
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color gotColor = bmp.GetPixel(x, y);
                    if (gotColor.A != 0 && gotColor.R == gotColor.G && gotColor.R != 0 && gotColor.G != 255)
                    {
                        int asdf = 0;
                    }
                            
                    bmp.SetPixel(x, y, Color.FromArgb(gotColor.A, gotColor.B, gotColor.G, gotColor.B));
                }
            }
            pictureBox1.Image = bmp;
        }
    }
}
