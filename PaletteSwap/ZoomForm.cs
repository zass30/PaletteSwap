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
        Form1 mainform;
        int scale;
        Bitmap magnified_sprite;

        public ZoomForm(Form1 parentform)
        {
            InitializeComponent();
            mainform = parentform;
            scale = 6;
            magnified_sprite = parentform.magnify_sprite(Properties.Resources.dicstand1, 6);
            zoomBox.Paint += new System.Windows.Forms.PaintEventHandler(this.zoomBox_Paint);
        }

        private void zoomBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            var remapTable = mainform.currentSprite.StandingSpriteColorsRemapTable();
            mainform.imagepaint(e, magnified_sprite, remapTable, 1);
        }

        public void refreshZoomBox()
        {
            zoomBox.Refresh();
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
