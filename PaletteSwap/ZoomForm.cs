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
        crusherside
    }

    public partial class ZoomForm : Form
    {
        Form1 mainform;
        int scale; // how much is image zoomed
        double factor = 1.2; // how much buffer around window and image
        Bitmap magnified_sprite;
        img_type zoomed_img;

        public ZoomForm(Form1 parentform)
        {
            InitializeComponent();
            mainform = parentform;
            scale = 4;
            magnified_sprite = parentform.magnify_sprite(Properties.Resources.dicstand1, scale);
            zoomBox.Paint += new System.Windows.Forms.PaintEventHandler(this.zoomBox_Paint);
        }

        private void zoomBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            ColorMap[] remapTable;
            switch (zoomed_img)
            {
                case img_type.neutral:
                    remapTable = mainform.currentSprite.StandingSpriteColorsRemapTable();
                    mainform.imagepaint(e, magnified_sprite, remapTable, 1);
                    break;
                case img_type.psychopunch:
                    remapTable = mainform.currentSprite.PsychoPunchColorsRemapTable();
                    mainform.imagepaint(e, magnified_sprite, remapTable, 1);
                    break;
            }
        }

        public void refreshZoomBox()
        {
            zoomBox.Refresh();
        }

        public void displayZoomImage(Bitmap b, img_type i)
        {
            zoomBox.Height = b.Height * scale;
            zoomBox.Width = b.Width * scale;
            this.Height = (int) (b.Height * factor * scale);
            this.Width = (int)(b.Width * factor * scale);
            magnified_sprite = mainform.magnify_sprite(b, scale);
            zoomed_img = i;
        }
    }
}
