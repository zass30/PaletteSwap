using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaletteSwap
{

    public partial class ZoomForm : Form
    {
        MainForm mainform;
        double factor = 1.3; // how much buffer around window and image

        public ZoomForm(MainForm parentform)
        {
            InitializeComponent();
            mainform = parentform;
            zoomBox.Paint += new System.Windows.Forms.PaintEventHandler(this.zoomBox_Paint);
        }

        private void zoomBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
            return;
        }

        public void displayZoomImage()
        {
            var scaledImg = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
            zoomBox.Height = scaledImg.Height;
            zoomBox.Width = scaledImg.Width;
            this.Height = (int)(zoomBox.Height * factor);
            this.Width = (int)(zoomBox.Width * factor);
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
        }
    }
}
