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
    public partial class ZoomForm : Form
    {
        public ZoomForm()
        {
            InitializeComponent();
        }

        private void zoomBox_Click(object sender, EventArgs e)
        {

        }

        public void displayZoomImage(Bitmap b)
        {
            double factor = 1.1;
            zoomBox.Height = b.Height;
            zoomBox.Width = b.Width;
            zoomBox.Image = b;
            this.Height = (int) (b.Height * factor);
            this.Width = (int)(b.Width * factor);
        }
    }
}
