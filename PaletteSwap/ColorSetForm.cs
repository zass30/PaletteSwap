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
            if (mainform.currentCharacterType == CharacterConfig.CHARACTERS.Dictator)
            {
                neutralKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GenerateKey("psychopunch");
            }
            spriteKeyBox.Height = neutralKey.Height;
            spriteKeyBox.Width = neutralKey.Width;
            spriteKeyBox.BackgroundImage = neutralKey;

            var victoryKey = mainform.gameSet.characterDictionary[mainform.currentCharacterType].GeneratePortraitKey();
            portraitKeyBox.Height = victoryKey.Height;
            portraitKeyBox.Width = victoryKey.Width;
            portraitKeyBox.BackgroundImage = victoryKey;
            setBackColor();
        }

        public void setBackColor()
        {
            portraitKeyBox.BackColor = mainform.backgroundcolor;
            spriteKeyBox.BackColor = mainform.backgroundcolor;
        }
    }
}
