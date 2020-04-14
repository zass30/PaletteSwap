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
            var DIC = mainform.gameSet.characterDictionary[CharacterConfig.CHARACTERS.Dictator];
            var CLA = mainform.gameSet.characterDictionary[CharacterConfig.CHARACTERS.Claw];
            var GUI = mainform.gameSet.characterDictionary[CharacterConfig.CHARACTERS.Guile];
            var RYU = mainform.gameSet.characterDictionary[CharacterConfig.CHARACTERS.Ryu];

            if (DIC.characterColors[0].sprite != null)
                _00psychopunchBox.Image = DIC.characterColors[0].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[1].sprite != null)
                _01psychopunchBox.Image = DIC.characterColors[1].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[2].sprite != null)
                _02psychopunchBox.Image = DIC.characterColors[2].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[3].sprite != null)
                _03psychopunchBox.Image = DIC.characterColors[3].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[4].sprite != null)
                _04psychopunchBox.Image = DIC.characterColors[4].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[5].sprite != null)
                _05psychopunchBox.Image = DIC.characterColors[5].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[6].sprite != null)
                _06psychopunchBox.Image = DIC.characterColors[6].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[7].sprite != null)
                _07psychopunchBox.Image = DIC.characterColors[7].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[8].sprite != null)
                _08psychopunchBox.Image = DIC.characterColors[8].sprite.GetBitmap("psychopunch");
            if (DIC.characterColors[9].sprite != null)
                _09psychopunchBox.Image = DIC.characterColors[9].sprite.GetBitmap("psychopunch");

            if (CLA.characterColors[0].sprite != null)
                CLAW_neutralStandBox00.Image = CLA.characterColors[0].sprite.GetBitmap("neutral");
            if (CLA.characterColors[1].sprite != null)
                CLAW_neutralStandBox01.Image = CLA.characterColors[1].sprite.GetBitmap("neutral");
            if (CLA.characterColors[2].sprite != null)
                CLAW_neutralStandBox02.Image = CLA.characterColors[2].sprite.GetBitmap("neutral");
            if (CLA.characterColors[3].sprite != null)
                CLAW_neutralStandBox03.Image = CLA.characterColors[3].sprite.GetBitmap("neutral");
            if (CLA.characterColors[4].sprite != null)
                CLAW_neutralStandBox04.Image = CLA.characterColors[4].sprite.GetBitmap("neutral");
            if (CLA.characterColors[5].sprite != null)
                CLAW_neutralStandBox05.Image = CLA.characterColors[5].sprite.GetBitmap("neutral");
            if (CLA.characterColors[6].sprite != null)
                CLAW_neutralStandBox06.Image = CLA.characterColors[6].sprite.GetBitmap("neutral");
            if (CLA.characterColors[7].sprite != null)
                CLAW_neutralStandBox07.Image = CLA.characterColors[7].sprite.GetBitmap("neutral");
            if (CLA.characterColors[8].sprite != null)
                CLAW_neutralStandBox08.Image = CLA.characterColors[8].sprite.GetBitmap("neutral");
            if (CLA.characterColors[9].sprite != null)
                CLAW_neutralStandBox09.Image = CLA.characterColors[9].sprite.GetBitmap("neutral");

            if (GUI.characterColors[0].sprite != null)
                GUI_neutralStandBox00.Image = GUI.characterColors[0].sprite.GetBitmap("neutral");
            if (GUI.characterColors[1].sprite != null)
                GUI_neutralStandBox01.Image = GUI.characterColors[1].sprite.GetBitmap("neutral");
            if (GUI.characterColors[2].sprite != null)
                GUI_neutralStandBox02.Image = GUI.characterColors[2].sprite.GetBitmap("neutral");
            if (GUI.characterColors[3].sprite != null)
                GUI_neutralStandBox03.Image = GUI.characterColors[3].sprite.GetBitmap("neutral");
            if (GUI.characterColors[4].sprite != null)
                GUI_neutralStandBox04.Image = GUI.characterColors[4].sprite.GetBitmap("neutral");
            if (GUI.characterColors[5].sprite != null)
                GUI_neutralStandBox05.Image = GUI.characterColors[5].sprite.GetBitmap("neutral");
            if (GUI.characterColors[6].sprite != null)
                GUI_neutralStandBox06.Image = GUI.characterColors[6].sprite.GetBitmap("neutral");
            if (GUI.characterColors[7].sprite != null)
                GUI_neutralStandBox07.Image = GUI.characterColors[7].sprite.GetBitmap("neutral");
            if (GUI.characterColors[8].sprite != null)
                GUI_neutralStandBox08.Image = GUI.characterColors[8].sprite.GetBitmap("neutral");
            if (GUI.characterColors[9].sprite != null)
                GUI_neutralStandBox09.Image = GUI.characterColors[9].sprite.GetBitmap("neutral");

            if (RYU.characterColors[0].sprite != null)
                RYU_neutralStandBox00.Image = RYU.characterColors[0].sprite.GetBitmap("neutral");
            if (RYU.characterColors[1].sprite != null)
                RYU_neutralStandBox01.Image = RYU.characterColors[1].sprite.GetBitmap("neutral");
            if (RYU.characterColors[2].sprite != null)
                RYU_neutralStandBox02.Image = RYU.characterColors[2].sprite.GetBitmap("neutral");
            if (RYU.characterColors[3].sprite != null)
                RYU_neutralStandBox03.Image = RYU.characterColors[3].sprite.GetBitmap("neutral");
            if (RYU.characterColors[4].sprite != null)
                RYU_neutralStandBox04.Image = RYU.characterColors[4].sprite.GetBitmap("neutral");
            if (RYU.characterColors[5].sprite != null)
                RYU_neutralStandBox05.Image = RYU.characterColors[5].sprite.GetBitmap("neutral");
            if (RYU.characterColors[6].sprite != null)
                RYU_neutralStandBox06.Image = RYU.characterColors[6].sprite.GetBitmap("neutral");
            if (RYU.characterColors[7].sprite != null)
                RYU_neutralStandBox07.Image = RYU.characterColors[7].sprite.GetBitmap("neutral");
            if (RYU.characterColors[8].sprite != null)
                RYU_neutralStandBox08.Image = RYU.characterColors[8].sprite.GetBitmap("neutral");
            if (RYU.characterColors[9].sprite != null)
                RYU_neutralStandBox09.Image = RYU.characterColors[9].sprite.GetBitmap("neutral");

            if (RYU.characterColors[0].portrait != null)
                RYU_portraitVictoryBox00.Image = RYU.characterColors[0].portrait.GetBitmap("victory");
            if (RYU.characterColors[1].portrait != null)
                RYU_portraitVictoryBox01.Image = RYU.characterColors[1].portrait.GetBitmap("victory");
            if (RYU.characterColors[2].portrait != null)
                RYU_portraitVictoryBox02.Image = RYU.characterColors[2].portrait.GetBitmap("victory");
            if (RYU.characterColors[3].portrait != null)
                RYU_portraitVictoryBox03.Image = RYU.characterColors[3].portrait.GetBitmap("victory");
            if (RYU.characterColors[4].portrait != null)
                RYU_portraitVictoryBox04.Image = RYU.characterColors[4].portrait.GetBitmap("victory");
            if (RYU.characterColors[5].portrait != null)
                RYU_portraitVictoryBox05.Image = RYU.characterColors[5].portrait.GetBitmap("victory");
            if (RYU.characterColors[6].portrait != null)
                RYU_portraitVictoryBox06.Image = RYU.characterColors[6].portrait.GetBitmap("victory");
            if (RYU.characterColors[7].portrait != null)
                RYU_portraitVictoryBox07.Image = RYU.characterColors[7].portrait.GetBitmap("victory");
            if (RYU.characterColors[8].portrait != null)
                RYU_portraitVictoryBox08.Image = RYU.characterColors[8].portrait.GetBitmap("victory");
            if (RYU.characterColors[9].portrait != null)
                RYU_portraitVictoryBox09.Image = RYU.characterColors[9].portrait.GetBitmap("victory");

        }
    }
}
