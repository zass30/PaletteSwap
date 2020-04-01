using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace PaletteSwapTest
{
    [TestClass]
    public class PortraitTest
    {/*
        [TestMethod]
        public void PortraitNewTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            Assert.AreEqual("FF0F D90F 960E 750C 640A 5408 4306 FE0F F90F D50F A00F 8E00 6D03 4C00 2A02 0A00", p.row1);
            Assert.AreEqual("FF0F D90F 960E 750C 640A 5408 4306 7F09 5D09 3B09 0909 7C00 5B03 4A00 0900 0A00", p.row4);
        }

        [TestMethod]
        public void PortraitColorsTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            Assert.AreEqual("FF0F", PaletteHelper.ColorToMemFormat(p.skin1));
            Assert.AreEqual("D90F", PaletteHelper.ColorToMemFormat(p.skin2));
            Assert.AreEqual("960E", PaletteHelper.ColorToMemFormat(p.skin3));
            Assert.AreEqual("750C", PaletteHelper.ColorToMemFormat(p.skin4));
            Assert.AreEqual("640A", PaletteHelper.ColorToMemFormat(p.skin5));
            Assert.AreEqual("5408", PaletteHelper.ColorToMemFormat(p.skin6));
            Assert.AreEqual("4306", PaletteHelper.ColorToMemFormat(p.skin7));

            Assert.AreEqual("FE0F", PaletteHelper.ColorToMemFormat(p.piping1));
            Assert.AreEqual("F90F", PaletteHelper.ColorToMemFormat(p.piping2));
            Assert.AreEqual("D50F", PaletteHelper.ColorToMemFormat(p.piping3));
            Assert.AreEqual("A00F", PaletteHelper.ColorToMemFormat(p.piping4));

            Assert.AreEqual("8E00", PaletteHelper.ColorToMemFormat(p.costume1));
            Assert.AreEqual("6D03", PaletteHelper.ColorToMemFormat(p.costume2));
            Assert.AreEqual("4C00", PaletteHelper.ColorToMemFormat(p.costume3));
            Assert.AreEqual("2A02", PaletteHelper.ColorToMemFormat(p.costume4));

            Assert.AreEqual("000F", PaletteHelper.ColorToMemFormat(p.blood1));
            Assert.AreEqual("000C", PaletteHelper.ColorToMemFormat(p.blood2));
            Assert.AreEqual("000A", PaletteHelper.ColorToMemFormat(p.blood3));

            Assert.AreEqual("FF0F", PaletteHelper.ColorToMemFormat(p.teeth1));
            Assert.AreEqual("CC0C", PaletteHelper.ColorToMemFormat(p.teeth2));
            Assert.AreEqual("9909", PaletteHelper.ColorToMemFormat(p.teeth3));
            Assert.AreEqual("7707", PaletteHelper.ColorToMemFormat(p.teeth4));

            Assert.AreEqual("7F09", PaletteHelper.ColorToMemFormat(p.pipingloss1));
            Assert.AreEqual("5D09", PaletteHelper.ColorToMemFormat(p.pipingloss2));
            Assert.AreEqual("3B09", PaletteHelper.ColorToMemFormat(p.pipingloss3));
            Assert.AreEqual("0909", PaletteHelper.ColorToMemFormat(p.pipingloss4));

            Assert.AreEqual("7C00", PaletteHelper.ColorToMemFormat(p.costumeloss1));
            Assert.AreEqual("5B03", PaletteHelper.ColorToMemFormat(p.costumeloss2));
            Assert.AreEqual("4A00", PaletteHelper.ColorToMemFormat(p.costumeloss3));
            Assert.AreEqual("0900", PaletteHelper.ColorToMemFormat(p.costumeloss4));
        }

        [TestMethod]
        public void PaletteSwapArrayTest()
        {
            Bitmap srcbmp = new Bitmap(30, 1);
            var src = new Portrait(Portrait.bis0portrait);
            var dest = new Portrait(Portrait.bis1portrait);
            var src_colors = src.VictoryColorsArray();
            var dest_colors = src.VictoryColorsArray();
            int l = src_colors.Length;

            for (int i = 0; i < l; i++)
            {
                srcbmp.SetPixel(i, 0, src_colors[i]);
            }

            var swappedbmp = PaletteHelper.PaletteSwap(srcbmp, src_colors, dest_colors);
            for (int i = 0; i < l; i++)
            {
                Assert.AreEqual(dest_colors[i], swappedbmp.GetPixel(i, 0));
            }
        }

        [TestMethod]
        public void PortraitRowTest()
        {
            string s = Portrait.bis0portrait;
            var p = new Portrait(s);
            var expected = "FF0F D90F 960E 750C 640A 5408 4306";
            Assert.AreEqual(expected, p.facerow());
            expected = "FE0F F90F D50F A00F";
            Assert.AreEqual(expected, p.pipingrow());
            expected = "8E00 6D03 4C00 2A02";
            Assert.AreEqual(expected, p.costumerow());
            expected = "000F 000C 000A";
            Assert.AreEqual(expected, p.bloodrow());
            expected = "FF0F CC0C 9909 7707";
            Assert.AreEqual(expected, p.teethrow());
            expected = "7F09 5D09 3B09 0909";
            Assert.AreEqual(expected, p.pipinglossrow());
            expected = "7C00 5B03 4A00 0900";
            Assert.AreEqual(expected, p.costumelossrow());

            var r = p.portraitmem();
            Assert.AreEqual(s, r);
        }

        [TestMethod]
        public void GenerateVictoryPortraitTest()
        {
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            string s = Portrait.bis5portrait;
            var p = new Portrait(s);
            Bitmap portrait_result = p.GenerateVictoryPortrait();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(portrait_expected, portrait_result));

            portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin0);
            s = Portrait.bis0portrait;
            p = new Portrait(s);
            portrait_result = p.GenerateVictoryPortrait();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(portrait_expected, portrait_result));
        }

        [TestMethod]
        public void GenerateVictoryPortraitPerfTest()
        {
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin0);
            string s = Portrait.bis0portrait;

            for (int i = 0; i < 50; i++)
            {
                var p = new Portrait(s);
                Bitmap portrait_result = p.GenerateVictoryPortrait();
                Assert.IsTrue(PaletteHelper.areBitmapsSame(portrait_expected, portrait_result));
            }

            Bitmap portrait_orig = new Bitmap(PaletteSwap.Properties.Resources.dicportraitwin5);
            Graphics g = Graphics.FromImage(portrait_orig);
            int width = portrait_orig.Width;
            int height = portrait_orig.Height;

            for (int i = 0; i < 2; i++)
            {
                ImageAttributes imageAttributes = new ImageAttributes();
                var orig = new Portrait(Portrait.bis5portrait);
                var orig_colors = orig.VictoryColorsArray();
                var dest = new Portrait(Portrait.bis5portrait);
                var dest_colors = dest.VictoryColorsArray();
                ColorMap[] remapTable = new ColorMap[orig_colors.Length];

                for (int j = 0; j < orig_colors.Length; j++)
                {
                    ColorMap colorMap = new ColorMap();
                    colorMap.OldColor = orig_colors[j];
                    colorMap.NewColor = dest_colors[j];
                    remapTable[j] = colorMap;
                }

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
                g.DrawImage(
   portrait_orig,
   new Rectangle(150, 10, width, height),  // destination rectangle 
   0, 0,        // upper-left corner of source rectangle 
   width,       // width of source rectangle
   height,      // height of source rectangle
   GraphicsUnit.Pixel,
   imageAttributes);
            }
        }

        [TestMethod]
        public void GenerateLossPortraitTest()
        {
            // this one works b/c of flames, eventually fix this test to ignore the flames.
            Bitmap portrait_expected = new Bitmap(PaletteSwap.Properties.Resources.dicportraitloss5);
            string s = Portrait.bis5portrait;
            var p = new Portrait(s);
            Bitmap portrait_result = p.GenerateLossPortrait();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(portrait_expected, portrait_result));
        }

        [TestMethod]
        public void WritePortraitByteStreamBasicTest()
        {
            // test #1
            // check that a bis0sprite's byte representation is what we expect 
            var portrait = new Portrait(Portrait.bis0portrait);
            string s_expected = Portrait.portraitAsTextLine(Portrait.bis0portrait);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);

            var data_result = portrait.ByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                Assert.AreEqual(data_expected[0], data_result[0]);
            }
        }

        [TestMethod]
        public void WritePortraitByteStreamChangeFieldsTest()
        {
            var portrait = new Portrait(Portrait.bis0portrait);
            string s_expected = Portrait.portraitAsTextLine(Portrait.bis0portrait);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            // test #2
            // modify a few fields, check that results are what we expect 

            //  skin 1
            data_expected[0] = 0x23;
            data_expected[1] = 0x01;
            data_expected[0 + 32] = 0x23;
            data_expected[1 + 32] = 0x01;
            data_expected[0 + 2*32] = 0x23;
            data_expected[1 + 2*32] = 0x01;
            data_expected[0 + 3 * 32] = 0x23;
            data_expected[1 + 3 * 32] = 0x01;

            //  skin 2
            data_expected[2] = 0x12;
            data_expected[3] = 0x03;
            data_expected[2 + 32] = 0x12;
            data_expected[3 + 32] = 0x03;
            data_expected[2 + 2*32] = 0x12;
            data_expected[3 + 2*32] = 0x03;
            data_expected[2 + 3*32] = 0x12;
            data_expected[3 + 3*32] = 0x03;

            portrait.skin1 = Color.FromArgb(0, 17, 34, 51);
            portrait.skin2 = Color.FromArgb(0, 51, 17, 34);

            var data_result = portrait.ByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
//                if (Portrait.unusedOffsets.ContainsKey(i))
//                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }

        [TestMethod]
        public void WritePortraitByteStreamChangeSpriteTest()
        {
            var portrait = new Portrait(Portrait.bis0portrait);
            string s_expected = Portrait.portraitAsTextLine(Portrait.bis0portrait);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            // test #3
            // change all fields in portrait0 to portrait5, check new byte 
            // representation is correct

            var portrait0 = new Portrait(Portrait.bis0portrait);
            var portrait5 = new Portrait(Portrait.bis5portrait);

            Assert.AreNotEqual(portrait5.costume1, portrait0.costume1);

            var portraitType = portrait.GetType();
            foreach (var label in Enum.GetNames(typeof(Portrait.PORTRAIT_COLORS)))
            {
                var myFieldInfo = portraitType.GetField(label.ToString());
                var portrait5color = (Color)myFieldInfo.GetValue(portrait5);
                myFieldInfo.SetValue(portrait0, portrait5color);
            }

            Assert.AreEqual(portrait5.costume1, portrait0.costume1);

            // check that they have identical byte strings
            var data_result = portrait0.ByteStream();
            s_expected = Portrait.portraitAsTextLine(Portrait.bis5portrait);
            data_expected = PaletteHelper.StringToByteStream(s_expected);

            for (int i = 0; i < data_expected.Length; i++)
            {
//                if (Portrait.unusedOffsets.ContainsKey(i))
//                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }

        [TestMethod]
        public void WritePortraitToColFormatTest()
        {
            string s = Portrait.bis0portrait;
            var portrait = new Portrait(s);

            string string_expected = "255 255 255\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            string string_result = portrait.ToColFormat();
            Assert.AreEqual(string_expected, string_result);

            portrait.skin1 = Color.FromArgb(0, 17, 34, 51);
            string_expected = "17 34 51\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            string_result = portrait.ToColFormat();
            Assert.AreEqual(string_expected, string_result);
        }

        [TestMethod]
        public void LoadPortraitFromColFormatTest()
        {
            string s = "17 34 51\r\n255 221 153\r\n238 153 102\r\n204 119 85\r\n170 102 68\r\n136 85 68\r\n102 68 51\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n255 255 255\r\n204 204 204\r\n153 153 153\r\n119 119 119\r\n255 255 238\r\n255 255 153\r\n255 221 85\r\n255 170 0\r\n153 119 255\r\n153 85 221\r\n153 51 187\r\n153 0 153\r\n0 119 204\r\n51 85 187\r\n0 68 170\r\n0 0 153\r\n255 0 0\r\n204 0 0\r\n170 0 0\r\n";
            var portrait = Portrait.LoadFromColFormat(s);
            string string_result = portrait.ToColFormat();
            Assert.AreEqual(s, string_result);
        }

        [TestMethod]
        public void LoadPortraitFromStreamTest()
        {
            Portrait p_expected = new Portrait(Portrait.bis1portrait);
            var b = p_expected.ByteStream();

            Portrait p_result = Portrait.LoadFromStream(b);
            Assert.AreEqual(p_expected.costume1, p_result.costume1);
            Assert.AreEqual(p_expected.piping1, p_result.piping1);
        }

        [TestMethod]
        public void ColorFromPortraitColorsLabelTest()
        {
            string p = Portrait.bis0portrait;
            var portrait = new Portrait(p);

            var myType = portrait.GetType();
            foreach (var label in Enum.GetNames(typeof(Portrait.PORTRAIT_COLORS)))
            {
                var myFieldInfo = myType.GetField(label.ToString());
                var expected = (Color)myFieldInfo.GetValue(portrait);
                var sprite_color = (Portrait.PORTRAIT_COLORS)Enum.Parse(typeof(Portrait.PORTRAIT_COLORS), label);
                var result = portrait.GetColorFromAttributeLabel(sprite_color);
                Assert.AreEqual(expected, result);
            }        
        }

        [TestMethod]
        public void SetColorFromAttributeLabelTest()
        {
            string p = Portrait.bis0portrait;
            var portrait = new Portrait(p);
            portrait.SetColorFromAttributeLabel(Portrait.PORTRAIT_COLORS.costume1, Color.Azure);
            Assert.AreEqual(portrait.costume1, Color.Azure);
        }*/
    }
}
