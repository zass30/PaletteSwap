using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DicPic
{
    class Program
    {
        // sprites
        public static string filelocation04 = @"C:\Users\b-julbea\Documents\ST colors\PALMOD Tutorial\sfxe.04a";

        // portraits
        public static string filelocation03 = @"C:\Users\b-julbea\Documents\ST colors\PALMOD Tutorial\sfxe.03c";

        // darksoft combined
        public static string darksoftlocation = @"C:\Users\b-julbea\Documents\ST colors\PALMOD Tutorial\sfx.02";

        static void aMain(string[] args)
        {
            

            /* palette key: 0: unused. In row one, this has value of 05 00. Affects transparency of stage, dont override. 
                            1: belt/cap/pad under
                            2-6: costume, from dark to light
                            7: leg under, belt bits, cap bits. In pal 4, it's psychoglow
                            8: stripe/cap insignia
                            9-11: pads, from light to dark
                            12-15: skin (light to dark)
            */

            byte[] skin = new byte[8];
            byte[] psychocolor = new byte[2];

            for (int palid = 0; palid < 8 ; palid++)
            {
                SetPalette(palid);
            }
        }

        private static void SetPalette(int palid)
        {
            int offset = palid * (16 * 10 + 2);

            byte[] palette = new byte[30];
            byte[] costume = new byte[10];
            byte[] pads = new byte[6];


            using (BinaryReader b = new BinaryReader(File.Open(filelocation04,
                                                               FileMode.Open)))
            {
                int length = (int)b.BaseStream.Length;
                palette[0] = 0xFF;
                int pos = 0x00042E7E + offset; //start of dictator palette 0
                int origpos = pos;
                int required = 30; // 15 values for his palette, 2 bytes each
                int count = 0;

                // Seek the required index.
                b.BaseStream.Seek(pos, SeekOrigin.Begin);

                // Loop through the bytes.
                while (pos < length && count < required)
                {
                    byte y = b.ReadByte();
                    palette[count] = y;
                    pos++;
                    count++;
                }
            }

            // copy into costume array
            for (int i = 0; i < 10; i++)
            {
                costume[i] = palette[i + 2];
            }
            // copy into pads array
            for (int i = 0; i < 6; i++)
            {
                pads[i] = palette[i + 16];
            }

            using (BinaryWriter b = new BinaryWriter(File.Open(filelocation04,
                                                   FileMode.Open)))
            {
                int pos = 0x00042EDE+offset; //start of dictator palette 0, row 4
                b.BaseStream.Seek(pos, SeekOrigin.Begin);

                int i = 0;
                foreach (byte y in palette)
                {
                    // skip index 6, that's psychoglow
//                    if (i == 6) // commenting this out seems to fix the start/end frames of crusher
//                        continue;
                    b.Write(y);
                    i++;
                }
                pos = 0x00042F00 + offset; //start of dictator palette 0, row 5
                b.BaseStream.Seek(pos, SeekOrigin.Begin);
                foreach (byte y in costume)
                {
                    b.Write(y);
                }
            }

            // now write to file 03 (portraits)
            using (BinaryWriter b = new BinaryWriter(File.Open(filelocation03,
                                       FileMode.Open)))
            {
                offset = palid * 128;
                int pos = 0x00034456 + offset; //start of dictator palette 0, row 4
                b.BaseStream.Seek(pos, SeekOrigin.Begin);

                for (int i = 0; i <6; i++)
                {
                    b.Write(pads[i]); // write in pipipng
                }
                // now write in the last piping again, dividing by 2
                byte green = pads[4];
                byte blue = pads[4];
                byte alpha = pads[5];
                byte red = pads[5];
                green = (byte)(green & 0xf0);
                blue = (byte)(blue & 0x0f);
                alpha = (byte)(alpha & 0xf0);
                red = (byte)(red & 0x0f);
                green = (byte)(green / 2);
                blue = (byte)(blue / 2);
                red = (byte)(red / 2);
                green = (byte)(green & 0xf0);
                blue = (byte)(blue & 0x0f);
                alpha = (byte)(alpha & 0xf0);
                red = (byte)(red & 0x0f);
                byte GB = (byte)(green + blue);
                byte AR = (byte)(alpha + red);
                b.Write(GB);
                b.Write(AR);

                // now write costume, in reverse order, skipping last (first) color

                for (int i = 0; i<8; i=i+2)
                {
                    b.Write(costume[8 - i]);
                    b.Write(costume[9 - i]);
                }

            }
        }
    }
}
