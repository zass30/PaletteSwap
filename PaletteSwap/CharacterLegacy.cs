using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    public partial class Character
    {

        // legacy function to delete eventually after old cols are convereted
        public void loadFromColFormat(string colstr)
        {
            var v = colstr.Split(':');
            loadSpriteColFormat(v[0]);
            loadPortraitColFormat(v[1]);
        }

        private void loadPortraitColFormat(string s)
        {
            var p = this.portrait;
            var v = s.Split('\n');
            p.SetColor("skin1", PaletteHelper.RGBFormatToColor(v[0]));
            p.SetColor("skin2", PaletteHelper.RGBFormatToColor(v[1]));
            p.SetColor("skin3", PaletteHelper.RGBFormatToColor(v[2]));
            p.SetColor("skin4", PaletteHelper.RGBFormatToColor(v[3]));
            p.SetColor("skin5", PaletteHelper.RGBFormatToColor(v[4]));
            p.SetColor("skin6", PaletteHelper.RGBFormatToColor(v[5]));
            p.SetColor("skin7", PaletteHelper.RGBFormatToColor(v[6]));
            p.SetColor("costume1", PaletteHelper.RGBFormatToColor(v[7]));
            p.SetColor("costume2", PaletteHelper.RGBFormatToColor(v[8]));
            p.SetColor("costume3", PaletteHelper.RGBFormatToColor(v[9]));
            p.SetColor("costume4", PaletteHelper.RGBFormatToColor(v[10]));
            p.SetColor("teeth1", PaletteHelper.RGBFormatToColor(v[11]));
            p.SetColor("teeth2", PaletteHelper.RGBFormatToColor(v[12]));
            p.SetColor("teeth3", PaletteHelper.RGBFormatToColor(v[13]));
            p.SetColor("teeth4", PaletteHelper.RGBFormatToColor(v[14]));
            p.SetColor("piping1", PaletteHelper.RGBFormatToColor(v[15]));
            p.SetColor("piping2", PaletteHelper.RGBFormatToColor(v[16]));
            p.SetColor("piping3", PaletteHelper.RGBFormatToColor(v[17]));
            p.SetColor("piping4", PaletteHelper.RGBFormatToColor(v[18]));
            p.SetColor("pipingloss1", PaletteHelper.RGBFormatToColor(v[19]));
            p.SetColor("pipingloss2", PaletteHelper.RGBFormatToColor(v[20]));
            p.SetColor("pipingloss3", PaletteHelper.RGBFormatToColor(v[21]));
            p.SetColor("pipingloss4", PaletteHelper.RGBFormatToColor(v[22]));
            p.SetColor("costumeloss1", PaletteHelper.RGBFormatToColor(v[23]));
            p.SetColor("costumeloss2", PaletteHelper.RGBFormatToColor(v[24]));
            p.SetColor("costumeloss3", PaletteHelper.RGBFormatToColor(v[25]));
            p.SetColor("costumeloss4", PaletteHelper.RGBFormatToColor(v[26]));
            p.SetColor("blood1", PaletteHelper.RGBFormatToColor(v[27]));
            p.SetColor("blood2", PaletteHelper.RGBFormatToColor(v[28]));
            p.SetColor("blood3", PaletteHelper.RGBFormatToColor(v[29]));
        }

        private void loadSpriteColFormat(string s)
        {
            var sp = this.sprite;
            var v = s.Split('\n');
            sp.SetColor("skin1", PaletteHelper.RGBFormatToColor(v[0]));
            sp.SetColor("skin2", PaletteHelper.RGBFormatToColor(v[1]));
            sp.SetColor("skin3", PaletteHelper.RGBFormatToColor(v[2]));
            sp.SetColor("skin4", PaletteHelper.RGBFormatToColor(v[3]));
            sp.SetColor("costume1", PaletteHelper.RGBFormatToColor(v[4]));
            sp.SetColor("costume2", PaletteHelper.RGBFormatToColor(v[5]));
            sp.SetColor("costume3", PaletteHelper.RGBFormatToColor(v[6]));
            sp.SetColor("costume4", PaletteHelper.RGBFormatToColor(v[7]));
            sp.SetColor("costume5", PaletteHelper.RGBFormatToColor(v[8]));
            sp.SetColor("pads1", PaletteHelper.RGBFormatToColor(v[9]));
            sp.SetColor("pads2", PaletteHelper.RGBFormatToColor(v[10]));
            sp.SetColor("pads3", PaletteHelper.RGBFormatToColor(v[11]));
            sp.SetColor("pads4", PaletteHelper.RGBFormatToColor(v[12]));
            sp.SetColor("pads5", PaletteHelper.RGBFormatToColor(v[13]));
            sp.SetColor("stripe", PaletteHelper.RGBFormatToColor(v[14]));
            sp.SetColor("psychoglow", PaletteHelper.RGBFormatToColor(v[15]));
            sp.SetColor("psychopunch1", PaletteHelper.RGBFormatToColor(v[16]));
            sp.SetColor("psychopunch2", PaletteHelper.RGBFormatToColor(v[17]));
            sp.SetColor("psychopunch3", PaletteHelper.RGBFormatToColor(v[18]));
            sp.SetColor("psychopunch4", PaletteHelper.RGBFormatToColor(v[19]));
            sp.SetColor("psychopunch5", PaletteHelper.RGBFormatToColor(v[20]));
            sp.SetColor("crushercostume1", PaletteHelper.RGBFormatToColor(v[21]));
            sp.SetColor("crushercostume2", PaletteHelper.RGBFormatToColor(v[22]));
            sp.SetColor("crushercostume3", PaletteHelper.RGBFormatToColor(v[23]));
            sp.SetColor("crushercostume4", PaletteHelper.RGBFormatToColor(v[24]));
            sp.SetColor("crusherpads1", PaletteHelper.RGBFormatToColor(v[25]));
            sp.SetColor("crusherpads2", PaletteHelper.RGBFormatToColor(v[26]));
            sp.SetColor("crusherpads3", PaletteHelper.RGBFormatToColor(v[27]));
            sp.SetColor("crusherpads4", PaletteHelper.RGBFormatToColor(v[28]));
            sp.SetColor("crusherpads5", PaletteHelper.RGBFormatToColor(v[29]));
            sp.SetColor("crusherhands1", PaletteHelper.RGBFormatToColor(v[30]));
            sp.SetColor("crusherhands2", PaletteHelper.RGBFormatToColor(v[31]));
            sp.SetColor("crusherflame1", PaletteHelper.RGBFormatToColor(v[32]));
            sp.SetColor("crusherflame2", PaletteHelper.RGBFormatToColor(v[33]));

        }
        // end legacy function block
    }
}
