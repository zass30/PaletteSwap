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
    public enum img_type
    {
        neutral,
        psychopunch,
        psychoprep,
        crushertop,
        crusherside,
        victoryportrait,
        lossportrait
    }

    public partial class ZoomForm : Form
    {
        MainForm mainform;
        int scale; // how much is image zoomed
        double factor = 1.3; // how much buffer around window and image
        Bitmap magnified_sprite;
        img_type zoomed_img;

        public ZoomForm(MainForm parentform)
        {
            InitializeComponent();
            mainform = parentform;
            scale = 4;
            magnified_sprite = parentform.magnify_sprite(Properties.Resources.dicstand1, scale);
            zoomBox.Paint += new System.Windows.Forms.PaintEventHandler(this.zoomBox_Paint);
        }

        private void zoomBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
            return;
        }

        public void refreshZoomBox()
        {
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
        }

        public void displayZoomImage(Bitmap b, img_type i)
        {
            var scaledImg = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
            zoomBox.Height = scaledImg.Height;
            zoomBox.Width = scaledImg.Width;
            this.Height = (int)(zoomBox.Height * factor);
            this.Width = (int)(zoomBox.Width * factor);
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
            zoomed_img = i;
        }
    }
}
