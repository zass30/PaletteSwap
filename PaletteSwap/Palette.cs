using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PaletteSwap
{
    public class Palette
    {
        private Color[] colors;

        public static Palette PaletteFromACT(string s)
        {
            var pal = new Palette();
            string[] s_colors = s.Split(' ');
            for (int i = 0; i < pal.colors.Length; i++)
            {
                var c = "00" + s_colors[3 * i] + s_colors[3 * i + 1] + s_colors[3 * i + 2];
                var cint = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                pal.colors[i] = Color.FromArgb(cint);
            }
            return pal;
        }

        public static Palette PaletteFromMem(string s)
        {
            var pal = new Palette();
            string[] s_colors = s.Split(' ');
            for (int i = 0; i < pal.colors.Length; i++)
            {
                string c = s_colors[i][2].ToString() + s_colors[i][2].ToString() + s_colors[i][3].ToString() + 
                    s_colors[i][3].ToString() + s_colors[i][0].ToString() + s_colors[i][0].ToString() 
                    + s_colors[i][1].ToString() + s_colors[i][1].ToString();
                var cint = int.Parse(c, System.Globalization.NumberStyles.HexNumber);
                pal.colors[i] = Color.FromArgb(cint);
            }
            return pal;
        }

        public Palette()
        {
            colors = new Color[15];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.FromArgb(0, 0, 0, 0);
            }
        }

        public string asACT()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < colors.Length; i++)
            {
                sb.Append(toACTFormat(colors[i]));
                sb.Append(" ");
            }
            return sb.ToString().Trim();
        }

        public string asMem()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < colors.Length; i++)
            {
                sb.Append(toMemFormat(colors[i]));
                sb.Append(" ");
            }
            return sb.ToString().Trim();
        }

        private static string toMemFormat(Color c)
        {
            string s = (c.G / 16).ToString("X1") + (c.B / 16).ToString("X1") + "0" + (c.R / 16).ToString("X1");
            return s;
        }

        private static string toACTFormat(Color c)
        {
            string s = c.R.ToString("X2") + " " + c.G.ToString("X2") + " " + c.B.ToString("X2");
            return s;
        }

    }
}
