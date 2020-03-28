﻿using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteTest
    {
        private PaletteConfig GenerateDictatorConfig()
        {
            int ROWLEN = 32;
            int MEMLEN = 160;
            Dictionary<string, List<int>> dictatorSpriteOffsets = new Dictionary<string, List<int>>
        {
            { "pads5", new List<int>() { 0, ROWLEN * 3 + 0, ROWLEN * 4 + 0 } },
            { "costume5", new List<int>() { 2, ROWLEN * 3 + 2, ROWLEN * 4 + 2 } },
            { "costume4", new List<int>() { 4, ROWLEN * 3 + 4, ROWLEN * 4 + 4 } },
            { "costume3", new List<int>() { 6, ROWLEN * 3 + 6, ROWLEN * 4 + 6 } },
            { "costume2", new List<int>() { 8, ROWLEN * 3 + 8, ROWLEN * 4 + 8 } },
            { "costume1", new List<int>() { 10, ROWLEN * 3 + 10, ROWLEN * 4 + 10 } },
            { "pads4", new List<int>() { 12 } },
            { "stripe", new List<int>() { 14, ROWLEN * 3 + 14 } },
            { "pads1", new List<int>() { 16, ROWLEN * 1 + 16, ROWLEN * 3 + 16 } },
            { "pads2", new List<int>() { 18, ROWLEN * 1 + 18, ROWLEN * 3 + 18 } },
            { "pads3", new List<int>() { 20, ROWLEN * 1 + 20, ROWLEN * 3 + 20 } },
            { "skin1", new List<int>() { 22, ROWLEN * 1 + 22, ROWLEN * 3 + 22, ROWLEN * 4 + 22 } },
            { "skin2", new List<int>() { 24, ROWLEN * 1 + 24, ROWLEN * 3 + 24, ROWLEN * 4 + 24 } },
            { "skin3", new List<int>() { 26, ROWLEN * 1 + 26, ROWLEN * 3 + 26, ROWLEN * 4 + 26 } },
            { "skin4", new List<int>() { 28, ROWLEN * 1 + 28, ROWLEN * 3 + 28, ROWLEN * 4 + 28 } },

            { "crusherpads5", new List<int>() { ROWLEN * 2 + 0 } },
            { "crushercostume4", new List<int>() { ROWLEN * 2 + 4 } },
            { "crushercostume3", new List<int>() { ROWLEN * 2 + 6 } },
            { "crushercostume2", new List<int>() { ROWLEN * 2 + 8 } },
            { "crushercostume1", new List<int>() { ROWLEN * 2 + 10 } },
            { "crusherpads4", new List<int>() { ROWLEN * 2 + 12 } },
            { "crusherflame1", new List<int>() { ROWLEN * 2 + 14 } },
            { "crusherpads1", new List<int>() { ROWLEN * 2 + 16 } },
            { "crusherpads2", new List<int>() { ROWLEN * 2 + 18 } },
            { "crusherpads3", new List<int>() { ROWLEN * 2 + 20 } },
            { "crusherflame2", new List<int>() { ROWLEN * 2 + 22 } },
            { "crusherhands1", new List<int>() { ROWLEN * 2 + 24 } },
            { "crusherhands2", new List<int>() { ROWLEN * 2 + 26 } },
            { "psychoglow", new List<int>() { ROWLEN * 3 + 12 } },
            { "psychopunch1", new List<int>() { ROWLEN * 4 + 12 } },
            { "psychopunch2", new List<int>() { ROWLEN * 4 + 14 } },
            { "psychopunch3", new List<int>() { ROWLEN * 4 + 16 } },
            { "psychopunch4", new List<int>() { ROWLEN * 4 + 18 } },
            { "psychopunch5", new List<int>() { ROWLEN * 4 + 20 } },
        };

            PaletteConfig pc = new PaletteConfig();
            pc.labelOffsets = dictatorSpriteOffsets;
            pc.unusedOffsets = new List<int>() { 66, 67, 92, 93 };
            string defaults = "0007 2302 3403 5605 6706 7807 8A08 9B09";
            int defaultsOffset = 32;
            pc.createColorOffsets(defaults, defaultsOffset);
            pc.streamLength = MEMLEN;
            return pc;
        }

        [TestMethod]
        public void PaletteNewTest()
        {
            var s = new Palette();
            var c = Color.FromArgb(0, 17, 34, 51);
            s.SetColor("skin1", c);
            var result = s.getColor("skin1");
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
            Assert.AreEqual(s.getColor("skin1"), s2.getColor("skin1"));

        }


        [TestMethod]
        public void OffsetsTest()
        {
            var s = new Palette();
            s.setAllOffSets(Character.dictatorSpriteOffsets);
            Assert.AreEqual(0, s.getOffsets("pads5")[0]);
            Assert.AreEqual(12, s.getOffsets("pads4")[0]);
        }

        [TestMethod]
        public void createDefaultCharacterTest()
        {
            Character d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hk);
            var s = d.sprite;
            Assert.IsNotNull(s);
            Assert.AreEqual("FD0A", PaletteHelper.ColorToMemFormat(s.getColor("pads1")));
            Assert.AreEqual("DB06", PaletteHelper.ColorToMemFormat(s.getColor("pads2")));
            Assert.AreEqual("A803", PaletteHelper.ColorToMemFormat(s.getColor("pads3")));
            Assert.AreEqual("6402", PaletteHelper.ColorToMemFormat(s.getColor("pads4")));
            Assert.AreEqual("4101", PaletteHelper.ColorToMemFormat(s.getColor("pads5")));
            
            Assert.AreEqual("BF0F", PaletteHelper.ColorToMemFormat(s.getColor("costume1")));
            Assert.AreEqual("8E0E", PaletteHelper.ColorToMemFormat(s.getColor("costume2")));
            Assert.AreEqual("6C0C", PaletteHelper.ColorToMemFormat(s.getColor("costume3")));
            Assert.AreEqual("4909", PaletteHelper.ColorToMemFormat(s.getColor("costume4")));
            Assert.AreEqual("0606", PaletteHelper.ColorToMemFormat(s.getColor("costume5")));

            Assert.AreEqual("FE0E", PaletteHelper.ColorToMemFormat(s.getColor("skin1")));
            Assert.AreEqual("B90C", PaletteHelper.ColorToMemFormat(s.getColor("skin2")));
            Assert.AreEqual("7609", PaletteHelper.ColorToMemFormat(s.getColor("skin3")));
            Assert.AreEqual("5307", PaletteHelper.ColorToMemFormat(s.getColor("skin4")));

            Assert.AreEqual("330D", PaletteHelper.ColorToMemFormat(s.getColor("stripe")));

            Assert.AreEqual("FE0D", PaletteHelper.ColorToMemFormat(s.getColor("psychoglow")));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(s.getColor("psychopunch1")));
            Assert.AreEqual("DF0C", PaletteHelper.ColorToMemFormat(s.getColor("psychopunch2")));
            Assert.AreEqual("AF09", PaletteHelper.ColorToMemFormat(s.getColor("psychopunch3")));
            Assert.AreEqual("8F07", PaletteHelper.ColorToMemFormat(s.getColor("psychopunch4")));
            Assert.AreEqual("6F05", PaletteHelper.ColorToMemFormat(s.getColor("psychopunch5")));

            Assert.AreEqual("DF0F", PaletteHelper.ColorToMemFormat(s.getColor("crushercostume1")));
            Assert.AreEqual("AE0E", PaletteHelper.ColorToMemFormat(s.getColor("crushercostume2")));
            Assert.AreEqual("7C0C", PaletteHelper.ColorToMemFormat(s.getColor("crushercostume3")));
            Assert.AreEqual("6A0A", PaletteHelper.ColorToMemFormat(s.getColor("crushercostume4")));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(s.getColor("crusherpads1")));
            Assert.AreEqual("DA08", PaletteHelper.ColorToMemFormat(s.getColor("crusherpads2")));
            Assert.AreEqual("9506", PaletteHelper.ColorToMemFormat(s.getColor("crusherpads3")));
            Assert.AreEqual("7305", PaletteHelper.ColorToMemFormat(s.getColor("crusherpads4")));
            Assert.AreEqual("5200", PaletteHelper.ColorToMemFormat(s.getColor("crusherpads5")));

            Assert.AreEqual("EC0B", PaletteHelper.ColorToMemFormat(s.getColor("crusherhands1")));
            Assert.AreEqual("CA08", PaletteHelper.ColorToMemFormat(s.getColor("crusherhands2")));

            Assert.AreEqual("EF0D", PaletteHelper.ColorToMemFormat(s.getColor("crusherflame1")));
            Assert.AreEqual("F80E", PaletteHelper.ColorToMemFormat(s.getColor("crusherflame2")));

            d = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mp);
            s = d.sprite;
            Assert.IsNotNull(s);

            Assert.AreEqual("FE0B", PaletteHelper.ColorToMemFormat(s.getColor("costume1")));
            Assert.AreEqual("EB08", PaletteHelper.ColorToMemFormat(s.getColor("costume2")));
            Assert.AreEqual("C705", PaletteHelper.ColorToMemFormat(s.getColor("costume3")));
            Assert.AreEqual("9503", PaletteHelper.ColorToMemFormat(s.getColor("costume4")));
            Assert.AreEqual("7300", PaletteHelper.ColorToMemFormat(s.getColor("costume5")));

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
    }
}
