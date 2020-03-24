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
            if (mainform.characterColorSet.characterColors[0].s != null)
                _00psychopunchBox.Image = mainform.characterColorSet.characterColors[0].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[1].s != null)
                _01psychopunchBox.Image = mainform.characterColorSet.characterColors[1].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[2].s != null)
                _02psychopunchBox.Image = mainform.characterColorSet.characterColors[2].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[3].s != null)
                _03psychopunchBox.Image = mainform.characterColorSet.characterColors[3].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[4].s != null)
                _04psychopunchBox.Image = mainform.characterColorSet.characterColors[4].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[5].s != null)
                _05psychopunchBox.Image = mainform.characterColorSet.characterColors[5].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[6].s != null)
                _06psychopunchBox.Image = mainform.characterColorSet.characterColors[6].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[7].s != null)
                _07psychopunchBox.Image = mainform.characterColorSet.characterColors[7].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[8].s != null)
                _08psychopunchBox.Image = mainform.characterColorSet.characterColors[8].s.GeneratePsychoPunchSprite();
            if (mainform.characterColorSet.characterColors[9].s != null)
                _09psychopunchBox.Image = mainform.characterColorSet.characterColors[9].s.GeneratePsychoPunchSprite();
        }
    }
}
