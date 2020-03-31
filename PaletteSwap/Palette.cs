using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaletteSwap
{
    // palette has label->color

    // stream: object that can print out stream and load stream
    // stream can go to string and back

    // image: can print image from palette  

    public struct ColorOffset
    {
        public Color c;
        public int position;
    }

    public class Palette
    {
        public int streamLength { get; set; }
        public List<ColorOffset> defaultColorOffsets = new List<ColorOffset>();
        public List<int> unusedOffsets = new List<int>();
        public Dictionary<string, PaletteImage> images = new Dictionary<string, PaletteImage>();
        
        private Dictionary<string, Color> labelsToColors = new Dictionary<string, Color>
        {
        };

        public Dictionary<string, List<int>> labelsToMemOffsets = new Dictionary<string, List<int>>
        {
        };

        public static Palette PaletteFromConfig(PaletteConfig pc)
        {
            Palette p = new Palette();
            p.setAllOffSets(pc.labelOffsets);
            p.defaultColorOffsets = new List<ColorOffset>(pc.defaultColorOffsets);
            p.streamLength = pc.streamLength;
            p.unusedOffsets = new List<int>(pc.unusedOffsets);
            return p;
        }

        public Color[] ColorsFromListOfLabels(List<string> labels)
        {
            List<Color> colors = new List<Color>();
            foreach (string label in labels)
            {
                colors.Add(GetColor(label));
            }
            return colors.ToArray();
        }

        public Palette()
        {

        }

        public void loadStream(byte[] b)
        {
            byte[] col_byte = new byte[2];
            foreach (var k in labelsToMemOffsets.Keys)
            {
                int offset = labelsToMemOffsets[k][0];
                col_byte[0] = b[offset];
                col_byte[1] = b[offset+1];
                Color col = PaletteHelper.ByteToColor(col_byte);
                labelsToColors[k] = col;
            }
        }

        public void setAllOffSets(Dictionary<string, List<int>> offsets)
        {
            foreach (var k in offsets.Keys)
            {
                var l = new List<int>(offsets[k]);
                setOffsets(k, l);
            }
        }

        public void setOffsets(string s, List<int> l)
        {
            labelsToMemOffsets[s] = l;
        }

        public List<int> getOffsets(string s)
        {
            return labelsToMemOffsets[s];
        }


        public Color GetColor(string s)
        {
            if (labelsToColors.ContainsKey(s))
                return labelsToColors[s];
            else
                return Color.Black;
        }

        public void SetColor(string s, Color c)
        {
            labelsToColors[s] = c;
        }

        public PaletteImage GetImage(string s)
        {
                return images[s];
        }

        public void SetImage(string s, PaletteImage p)
        {
            images[s] = p;
        }


        public byte[] ToByteStream()
        {
            byte[] b = new byte[streamLength];

            foreach (var k in defaultColorOffsets)
            {
                byte[] c = PaletteHelper.ColorToByte(k.c);
                b[k.position] = c[0];
                b[k.position + 1] = c[1];
            }

            foreach (var k in labelsToMemOffsets.Keys)
            {
                Color col = this.GetColor(k);
                byte[] c = PaletteHelper.ColorToByte(col);
                foreach (int offset in labelsToMemOffsets[k])
                {
                    b[offset] = c[0];
                    b[offset + 1] = c[1];
                }
            }

            return b;
        }

    }    
}
