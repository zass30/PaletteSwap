using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteTest
    {
        [TestMethod]
        public void PaletteNewTest()
        {
            var s = new Palette();
            var c = Color.FromArgb(255, 17, 34, 51);
            s.SetColor("skin1", c);
            var result = s.GetColor("skin1");
            Assert.AreEqual(c, result);

            s.setOffsets("skin1", new List<int>() { 0, 4, 6});
            var r_offset = s.getOffsets("skin1");
            Assert.AreEqual(0, r_offset[0]);
            Assert.AreEqual(6, r_offset[2]);

            s.streamLength = 10;
            var b_result = s.ToByteStream();
            Assert.AreEqual(10, b_result.Length);

            byte[] byte_color = new byte[2];
            byte_color[0] = b_result[0];
            byte_color[1] = b_result[1];

            var result_color = PaletteHelper.ByteToColor(byte_color);
            Assert.AreEqual(c, result_color);

            byte_color[0] = b_result[4];
            byte_color[1] = b_result[5];

            result_color = PaletteHelper.ByteToColor(byte_color);
            Assert.AreEqual(c, result_color);

            byte_color[0] = b_result[6];
            byte_color[1] = b_result[7];

            result_color = PaletteHelper.ByteToColor(byte_color);
            Assert.AreEqual(c, result_color);

            var s2 = new Palette();
            s2.setOffsets("skin1", new List<int>() { 0, 4, 6 });
            s2.loadStream(b_result);
            Assert.AreEqual(s.GetColor("skin1"), s2.GetColor("skin1"));

        }

        [TestMethod]
        public void CreateDefaultCharacterTest()
        {
            Character d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hk);
            var s = d.sprite;
            Assert.IsNotNull(s);
            Assert.AreEqual("FD0A", PaletteHelper.ColorToMemFormat(s.GetColor("pads1")));
            Assert.AreEqual("DB06", PaletteHelper.ColorToMemFormat(s.GetColor("pads2")));
            Assert.AreEqual("A803", PaletteHelper.ColorToMemFormat(s.GetColor("pads3")));
            Assert.AreEqual("6402", PaletteHelper.ColorToMemFormat(s.GetColor("pads4")));
            Assert.AreEqual("4101", PaletteHelper.ColorToMemFormat(s.GetColor("pads5")));
            
            Assert.AreEqual("BF0F", PaletteHelper.ColorToMemFormat(s.GetColor("costume1")));
            Assert.AreEqual("8E0E", PaletteHelper.ColorToMemFormat(s.GetColor("costume2")));
            Assert.AreEqual("6C0C", PaletteHelper.ColorToMemFormat(s.GetColor("costume3")));
            Assert.AreEqual("4909", PaletteHelper.ColorToMemFormat(s.GetColor("costume4")));
            Assert.AreEqual("0606", PaletteHelper.ColorToMemFormat(s.GetColor("costume5")));

            Assert.AreEqual("FE0E", PaletteHelper.ColorToMemFormat(s.GetColor("skin1")));
            Assert.AreEqual("B90C", PaletteHelper.ColorToMemFormat(s.GetColor("skin2")));
            Assert.AreEqual("7609", PaletteHelper.ColorToMemFormat(s.GetColor("skin3")));
            Assert.AreEqual("5307", PaletteHelper.ColorToMemFormat(s.GetColor("skin4")));

            Assert.AreEqual("330D", PaletteHelper.ColorToMemFormat(s.GetColor("stripe")));

            Assert.AreEqual("FE0D", PaletteHelper.ColorToMemFormat(s.GetColor("psychoglow")));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(s.GetColor("psychopunch1")));
            Assert.AreEqual("DF0C", PaletteHelper.ColorToMemFormat(s.GetColor("psychopunch2")));
            Assert.AreEqual("AF09", PaletteHelper.ColorToMemFormat(s.GetColor("psychopunch3")));
            Assert.AreEqual("8F07", PaletteHelper.ColorToMemFormat(s.GetColor("psychopunch4")));
            Assert.AreEqual("6F05", PaletteHelper.ColorToMemFormat(s.GetColor("psychopunch5")));

            Assert.AreEqual("DF0F", PaletteHelper.ColorToMemFormat(s.GetColor("crushercostume1")));
            Assert.AreEqual("AE0E", PaletteHelper.ColorToMemFormat(s.GetColor("crushercostume2")));
            Assert.AreEqual("7C0C", PaletteHelper.ColorToMemFormat(s.GetColor("crushercostume3")));
            Assert.AreEqual("6A0A", PaletteHelper.ColorToMemFormat(s.GetColor("crushercostume4")));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(s.GetColor("crusherpads1")));
            Assert.AreEqual("DA08", PaletteHelper.ColorToMemFormat(s.GetColor("crusherpads2")));
            Assert.AreEqual("9506", PaletteHelper.ColorToMemFormat(s.GetColor("crusherpads3")));
            Assert.AreEqual("7305", PaletteHelper.ColorToMemFormat(s.GetColor("crusherpads4")));
            Assert.AreEqual("5200", PaletteHelper.ColorToMemFormat(s.GetColor("crusherpads5")));

            Assert.AreEqual("EC0B", PaletteHelper.ColorToMemFormat(s.GetColor("crusherhands1")));
            Assert.AreEqual("CA08", PaletteHelper.ColorToMemFormat(s.GetColor("crusherhands2")));

            Assert.AreEqual("EF0D", PaletteHelper.ColorToMemFormat(s.GetColor("crusherflame1")));
            Assert.AreEqual("F80E", PaletteHelper.ColorToMemFormat(s.GetColor("crusherflame2")));

            d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mp);
            s = d.sprite;
            Assert.IsNotNull(s);

            Assert.AreEqual("FE0B", PaletteHelper.ColorToMemFormat(s.GetColor("costume1")));
            Assert.AreEqual("EB08", PaletteHelper.ColorToMemFormat(s.GetColor("costume2")));
            Assert.AreEqual("C705", PaletteHelper.ColorToMemFormat(s.GetColor("costume3")));
            Assert.AreEqual("9503", PaletteHelper.ColorToMemFormat(s.GetColor("costume4")));
            Assert.AreEqual("7300", PaletteHelper.ColorToMemFormat(s.GetColor("costume5")));
        }

        [TestMethod]
        public void WriteByteStreamBasicTest()
        {
            // test #1
            // check that a bis0sprite's byte representation is what we expect 
            string s_expected = PaletteSwap.Properties.Resources.bis0sprite;
            var data_expected = PaletteHelper.StringToByteStream(s_expected);

            var d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var s = d.sprite;
            var data_result = s.ToByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                Assert.AreEqual(data_expected[0], data_result[0]);
            }
        }

        [TestMethod]
        public void WriteByteStreamChangeFieldsTest()
        {
            var d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var sprite = d.sprite;
            string s_expected = PaletteSwap.Properties.Resources.bis0sprite;
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            // test #2
            // modify a few fields, check that results are what we expect 

            //  pads 5
            data_expected[0] = 0x23;
            data_expected[1] = 0x01;
            data_expected[0 + 3 * 32] = 0x23;
            data_expected[1 + 3 * 32] = 0x01;
            data_expected[0 + 4 * 32] = 0x23;
            data_expected[1 + 4 * 32] = 0x01;

            // costume 5
            data_expected[2] = 0x12;
            data_expected[3] = 0x03;
            data_expected[2 + 3 * 32] = 0x12;
            data_expected[3 + 3 * 32] = 0x03;
            data_expected[2 + 4 * 32] = 0x12;
            data_expected[3 + 4 * 32] = 0x03;

            sprite.SetColor("pads5", Color.FromArgb(0, 17, 34, 51));
            sprite.SetColor("costume5", Color.FromArgb(0, 51, 17, 34));

            var data_result = sprite.ToByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                if (sprite.unusedOffsets.Contains(i))
                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }

        [TestMethod]
        public void WriteSpriteByteStreamChangeSpriteTest()
        {
            var d_lp = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var sprite_lp = d_lp.sprite;

            var d_hk = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hk);
            var sprite_hk = d_hk.sprite;

            Assert.AreNotEqual(sprite_hk.GetColor("costume1"), sprite_lp.GetColor("costume1"));

            // now get each label
            foreach (var k in sprite_hk.labelsToMemOffsets)
            {
                string label = k.Key;
                sprite_lp.SetColor(label, sprite_hk.GetColor(label));
            }

            Assert.AreEqual(sprite_hk.GetColor("costume1"), sprite_lp.GetColor("costume1"));

            // check byte strings are identical

            var data_expected = sprite_hk.ToByteStream();
            var data_result = sprite_lp.ToByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                if (sprite_lp.unusedOffsets.Contains(i))
                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }

        [TestMethod]
        public void CreateDicPortraitFromConfigTest()
        {
            Character d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var p = d.portrait;
            Assert.IsNotNull(p);
            Assert.AreEqual("FF0F", PaletteHelper.ColorToMemFormat(p.GetColor("skin1")));
            Assert.AreEqual("D90F", PaletteHelper.ColorToMemFormat(p.GetColor("skin2")));
            Assert.AreEqual("960E", PaletteHelper.ColorToMemFormat(p.GetColor("skin3")));
            Assert.AreEqual("750C", PaletteHelper.ColorToMemFormat(p.GetColor("skin4")));
            Assert.AreEqual("640A", PaletteHelper.ColorToMemFormat(p.GetColor("skin5")));
            Assert.AreEqual("5408", PaletteHelper.ColorToMemFormat(p.GetColor("skin6")));
            Assert.AreEqual("4306", PaletteHelper.ColorToMemFormat(p.GetColor("skin7")));

            Assert.AreEqual("FE0F", PaletteHelper.ColorToMemFormat(p.GetColor("piping1")));
            Assert.AreEqual("F90F", PaletteHelper.ColorToMemFormat(p.GetColor("piping2")));
            Assert.AreEqual("D50F", PaletteHelper.ColorToMemFormat(p.GetColor("piping3")));
            Assert.AreEqual("A00F", PaletteHelper.ColorToMemFormat(p.GetColor("piping4")));

            Assert.AreEqual("8E00", PaletteHelper.ColorToMemFormat(p.GetColor("costume1")));
            Assert.AreEqual("6D03", PaletteHelper.ColorToMemFormat(p.GetColor("costume2")));
            Assert.AreEqual("4C00", PaletteHelper.ColorToMemFormat(p.GetColor("costume3")));
            Assert.AreEqual("2A02", PaletteHelper.ColorToMemFormat(p.GetColor("costume4")));

            Assert.AreEqual("000F", PaletteHelper.ColorToMemFormat(p.GetColor("blood1")));
            Assert.AreEqual("000C", PaletteHelper.ColorToMemFormat(p.GetColor("blood2")));
            Assert.AreEqual("000A", PaletteHelper.ColorToMemFormat(p.GetColor("blood3")));

            Assert.AreEqual("FF0F", PaletteHelper.ColorToMemFormat(p.GetColor("teeth1")));
            Assert.AreEqual("CC0C", PaletteHelper.ColorToMemFormat(p.GetColor("teeth2")));
            Assert.AreEqual("9909", PaletteHelper.ColorToMemFormat(p.GetColor("teeth3")));
            Assert.AreEqual("7707", PaletteHelper.ColorToMemFormat(p.GetColor("teeth4")));

            Assert.AreEqual("7F09", PaletteHelper.ColorToMemFormat(p.GetColor("pipingloss1")));
            Assert.AreEqual("5D09", PaletteHelper.ColorToMemFormat(p.GetColor("pipingloss2")));
            Assert.AreEqual("3B09", PaletteHelper.ColorToMemFormat(p.GetColor("pipingloss3")));
            Assert.AreEqual("0909", PaletteHelper.ColorToMemFormat(p.GetColor("pipingloss4")));

            Assert.AreEqual("7C00", PaletteHelper.ColorToMemFormat(p.GetColor("costumeloss1")));
            Assert.AreEqual("5B03", PaletteHelper.ColorToMemFormat(p.GetColor("costumeloss2")));
            Assert.AreEqual("4A00", PaletteHelper.ColorToMemFormat(p.GetColor("costumeloss3")));
            Assert.AreEqual("0900", PaletteHelper.ColorToMemFormat(p.GetColor("costumeloss4")));

            var portrait_bytestream_expected = PaletteHelper.StringToByteStream(PaletteSwap.Properties.Resources.bis0portrait);
            var portrait_bytestream_result = p.ToByteStream();

            for (int i = 0; i < portrait_bytestream_expected.Length; i++)
            {
                Assert.AreEqual(portrait_bytestream_expected[i], portrait_bytestream_result[i]);
            }
        }

        [TestMethod]
        public void ColorsFromListOfLabelsTest()
        {
            Character d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var p = d.portrait;
            Assert.IsNotNull(p);
            Color[] expected_colors = new Color[] { p.GetColor("skin1"), p.GetColor("blood1") };
            var result_colors = p.ColorsFromListOfLabels(new List<string> { "skin1", "blood1" });
            Assert.AreEqual(expected_colors[0], result_colors[0]);
            Assert.AreEqual(expected_colors[1], result_colors[1]);
        }

        [TestMethod]
        public void ToColFormatTest()
        {
            var pal = new Palette();
            var c1 = Color.FromArgb(255, 17, 34, 51);
            var c2 = Color.FromArgb(255, 51, 17, 34);
            var c3 = Color.FromArgb(255, 34, 51, 17);
            pal.SetColor("foo", c1);
            pal.SetColor("bar", c2);
            pal.SetColor("baz", c3);

            var s_expected = "";
            var s_result = pal.ToColFormat();
        }
    }
}
