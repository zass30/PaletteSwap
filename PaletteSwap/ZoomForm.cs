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
            var scaledImg = getZoomedImage();
            zoomBox.BackgroundImage = scaledImg;
            return;
        }

        private Bitmap getZoomedImage()
        {
            var label = mainform.currentlyZoomedLabel;
            Palette palette;
            if (label == "victory" || label == "loss")
            {
                palette = mainform.currentCharacter.portrait;
            }
            else
            {
                palette = mainform.currentCharacter.sprite;
            }

            var scaledImg = palette.GetImage(mainform.currentlyZoomedLabel).RemappedScaledImage();
            return scaledImg;
        }

        public void displayZoomImage()
        {
            // first, make sure we aren't trying to display a dictator special image on non dictators
            var scaledImg = getZoomedImage();
            zoomBox.Height = scaledImg.Height;
            zoomBox.Width = scaledImg.Width;
            this.Height = (int)(zoomBox.Height * factor);
            this.Width = (int)(zoomBox.Width * factor);
            zoomBox.BackgroundImage = scaledImg;
            zoomBox.BackColor = mainform.backgroundcolor;
        }

        public void setBackColor()
        {
            zoomBox.BackColor = mainform.backgroundcolor;
        }
    }
}
