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
        Bitmap magnified_losstop;
        Bitmap magnified_lossbottom; // special case for loss portrait which is made of two bitmaps
        img_type zoomed_img;
        PaletteImage currentImage;

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
            ColorMap[] remapTable = new ColorMap[0];
            switch (zoomed_img)
            {
                case img_type.neutral:
                    zoomBox.BackgroundImage = mainform.currentCharacter.sprite.GetImage("neutral").RemappedScaledImage();
                    break;
                case img_type.psychopunch:
                    zoomBox.BackgroundImage = mainform.currentCharacter.sprite.GetImage("psychopunch").RemappedScaledImage();
                    break;
                case img_type.psychoprep:
                    zoomBox.BackgroundImage = mainform.currentCharacter.sprite.GetImage("psychoprep").RemappedScaledImage();
                    break;
                case img_type.crushertop:
                    zoomBox.BackgroundImage = mainform.currentCharacter.sprite.GetImage("crushertop").RemappedScaledImage();
                    break;
                case img_type.crusherside:
                    zoomBox.BackgroundImage = mainform.currentCharacter.sprite.GetImage("crusherbottom").RemappedScaledImage();
                    break;
                case img_type.victoryportrait:
                    zoomBox.BackgroundImage = mainform.currentCharacter.portrait.GetImage("victory").RemappedScaledImage();
                    break;
                case img_type.lossportrait:
                    zoomBox.BackgroundImage = mainform.currentCharacter.portrait.GetImage("losstop").RemappedScaledImage();
                    break;
            }
            
/*            switch (zoomed_img)
            {
                case img_type.neutral:
                    remapTable = mainform.currentSprite.StandingSpriteColorsRemapTable();
                    break;
                case img_type.psychopunch:
                    remapTable = mainform.currentSprite.PsychoPunchColorsRemapTable();
                    break;
                case img_type.psychoprep:
                     remapTable = mainform.currentSprite.PsychoPrepColorsRemapTable();
                    break;
                case img_type.crushertop:
                     remapTable = mainform.currentSprite.CrusherColorsRemapTable();
                    break;
                case img_type.crusherside:
                    remapTable = mainform.currentSprite.CrusherColorsRemapTable();
                    break;
                case img_type.victoryportrait:
                    remapTable = mainform.currentPortrait.VictoryColorsRemapTable();
                    break;
                case img_type.lossportrait:
                    if (magnified_losstop == null)
                    {
                        magnified_losstop = mainform.magnify_sprite(Properties.Resources.dicportraitlosstop5, scale);
                    }
                    if (magnified_lossbottom == null)
                    {
                        magnified_lossbottom = mainform.magnify_sprite(Properties.Resources.dicportraitlossbottom5, scale);

                    }
                    mainform.imagepaint(e, magnified_losstop, mainform.currentPortrait.LossTopColorsRemapTable());
                    mainform.imagepaint(e, magnified_lossbottom, mainform.currentPortrait.LossBottomColorsRemapTable());
                    return;
            }
            mainform.imagepaint(e, magnified_sprite, remapTable);
            */
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
            zoomBox.BackgroundImage = mainform.currentlySelectedZoomPaletteImage.RemappedScaledImage();
//            magnified_sprite = mainform.magnify_sprite(b, scale);
//            zoomBox.BackgroundImage = magnified_sprite;
            zoomed_img = i;
        }
    }
}
