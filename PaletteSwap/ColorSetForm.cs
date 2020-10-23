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
    public partial class ColorSetForm : Form
    {
        MainForm mainform;

        public ColorSetForm(MainForm parentform)
        {
            InitializeComponent();
            mainform = parentform;
            Reload();
        }

        public void Reload()
        {
            var neutralKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GenerateSpriteKey();
            var victoryKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GeneratePortraitKey();
            if (mainform.currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                neutralKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GenerateKey("psychopunch");
            }

            var combinedKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GenerateColorSetKey();
            combinedKeyBox.BackgroundImageLayout = ImageLayout.None;
            combinedKeyBox.Height = combinedKey.Height;
            combinedKeyBox.Width = combinedKey.Width;
            combinedKeyBox.BackgroundImage = combinedKey;
            //            int maxwidth = Math.Max(neutralKey.Width, victoryKey.Width);
            /*            spriteKeyBox.BackgroundImageLayout = ImageLayout.None;
                        spriteKeyBox.Height = neutralKey.Height;
                        spriteKeyBox.Width = maxwidth;
                        spriteKeyBox.BackgroundImage = neutralKey;
            */
            /*        portraitKeyBox.BackgroundImageLayout = ImageLayout.None;
                    portraitKeyBox.Height = victoryKey.Height + neutralKey.Height;
                    portraitKeyBox.Width = maxwidth;
                    portraitKeyBox.BackgroundImage = victoryKey;
            */
            setBackColor();
        }

        public void setBackColor()
        {
            combinedKeyBox.BackColor = mainform.backgroundcolor;
        }

        private void ColorSetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Bitmap bmp = new Bitmap(combinedKeyBox.BackgroundImage);
                Clipboard.SetData(System.Windows.Forms.DataFormats.Bitmap, bmp);
            }
        }
    }
}
