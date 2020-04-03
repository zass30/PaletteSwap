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
            if (mainform.currentCharacterType == Character.CHARACTERS.Dictator)
            {
                if (mainform.characterSet.characterColors[0].sprite != null)
                    _00psychopunchBox.Image = mainform.characterSet.characterColors[0].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[1].sprite != null)
                    _01psychopunchBox.Image = mainform.characterSet.characterColors[1].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[2].sprite != null)
                    _02psychopunchBox.Image = mainform.characterSet.characterColors[2].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[3].sprite != null)
                    _03psychopunchBox.Image = mainform.characterSet.characterColors[3].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[4].sprite != null)
                    _04psychopunchBox.Image = mainform.characterSet.characterColors[4].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[5].sprite != null)
                    _05psychopunchBox.Image = mainform.characterSet.characterColors[5].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[6].sprite != null)
                    _06psychopunchBox.Image = mainform.characterSet.characterColors[6].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[7].sprite != null)
                    _07psychopunchBox.Image = mainform.characterSet.characterColors[7].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[8].sprite != null)
                    _08psychopunchBox.Image = mainform.characterSet.characterColors[8].sprite.GetBitmap("psychopunch");
                if (mainform.characterSet.characterColors[9].sprite != null)
                    _09psychopunchBox.Image = mainform.characterSet.characterColors[9].sprite.GetBitmap("psychopunch");
            }
            else if (mainform.currentCharacterType == Character.CHARACTERS.Claw)
            {
                if (mainform.characterSet.characterColors[0].sprite != null)
                    _00psychopunchBox.Image = mainform.characterSet.characterColors[0].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[1].sprite != null)
                    _01psychopunchBox.Image = mainform.characterSet.characterColors[1].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[2].sprite != null)
                    _02psychopunchBox.Image = mainform.characterSet.characterColors[2].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[3].sprite != null)
                    _03psychopunchBox.Image = mainform.characterSet.characterColors[3].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[4].sprite != null)
                    _04psychopunchBox.Image = mainform.characterSet.characterColors[4].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[5].sprite != null)
                    _05psychopunchBox.Image = mainform.characterSet.characterColors[5].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[6].sprite != null)
                    _06psychopunchBox.Image = mainform.characterSet.characterColors[6].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[7].sprite != null)
                    _07psychopunchBox.Image = mainform.characterSet.characterColors[7].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[8].sprite != null)
                    _08psychopunchBox.Image = mainform.characterSet.characterColors[8].sprite.GetBitmap("neutral");
                if (mainform.characterSet.characterColors[9].sprite != null)
                    _09psychopunchBox.Image = mainform.characterSet.characterColors[9].sprite.GetBitmap("neutral");
            }
        }
    }
}
