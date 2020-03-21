using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;
using System.Drawing;

namespace PaletteSwapTest
{
    [TestClass]
    public class SpriteTest
    {
        [TestMethod]
        public void SpriteNewTest()
        {
            string s = Sprite.bis0sprite;
            var sprite = new Sprite(s);
            Assert.AreEqual("0007 0800 2A02 4C00 6D03 8E00 300A B00F F70F B00F 700F FC0F C80D 7309 4005 0000", sprite.row1);
            Assert.AreEqual("0007 0800 2A02 4C00 6D03 8E00 FF0F E90F A40E 700E 400D FC0F C80D 7309 4005 0000", sprite.row5);
        }


        [TestMethod]
        public void SpriteColorsTest()
        {
            string s = Sprite.bis5sprite;
            var p = new Sprite(s);
            Assert.AreEqual("FD0A", PaletteHelper.ColorToMemFormat(p.pads1));
            Assert.AreEqual("DB06", PaletteHelper.ColorToMemFormat(p.pads2));
            Assert.AreEqual("A803", PaletteHelper.ColorToMemFormat(p.pads3));
            Assert.AreEqual("6402", PaletteHelper.ColorToMemFormat(p.pads4));
            Assert.AreEqual("4101", PaletteHelper.ColorToMemFormat(p.pads5));

            Assert.AreEqual("BF0F", PaletteHelper.ColorToMemFormat(p.costume1));
            Assert.AreEqual("8E0E", PaletteHelper.ColorToMemFormat(p.costume2));
            Assert.AreEqual("6C0C", PaletteHelper.ColorToMemFormat(p.costume3));
            Assert.AreEqual("4909", PaletteHelper.ColorToMemFormat(p.costume4));
            Assert.AreEqual("0606", PaletteHelper.ColorToMemFormat(p.costume5));

            Assert.AreEqual("FE0E", PaletteHelper.ColorToMemFormat(p.skin1));
            Assert.AreEqual("B90C", PaletteHelper.ColorToMemFormat(p.skin2));
            Assert.AreEqual("7609", PaletteHelper.ColorToMemFormat(p.skin3));
            Assert.AreEqual("5307", PaletteHelper.ColorToMemFormat(p.skin4));

            Assert.AreEqual("330D", PaletteHelper.ColorToMemFormat(p.stripe));

            Assert.AreEqual("FE0D", PaletteHelper.ColorToMemFormat(p.psychoglow));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(p.psychopunch1));
            Assert.AreEqual("DF0C", PaletteHelper.ColorToMemFormat(p.psychopunch2));
            Assert.AreEqual("AF09", PaletteHelper.ColorToMemFormat(p.psychopunch3));
            Assert.AreEqual("8F07", PaletteHelper.ColorToMemFormat(p.psychopunch4));
            Assert.AreEqual("6F05", PaletteHelper.ColorToMemFormat(p.psychopunch5));

            Assert.AreEqual("DF0F", PaletteHelper.ColorToMemFormat(p.crushercostume1));
            Assert.AreEqual("AE0E", PaletteHelper.ColorToMemFormat(p.crushercostume2));
            Assert.AreEqual("7C0C", PaletteHelper.ColorToMemFormat(p.crushercostume3));
            Assert.AreEqual("6A0A", PaletteHelper.ColorToMemFormat(p.crushercostume4));

            Assert.AreEqual("FF0E", PaletteHelper.ColorToMemFormat(p.crusherpads1));
            Assert.AreEqual("DA08", PaletteHelper.ColorToMemFormat(p.crusherpads2));
            Assert.AreEqual("9506", PaletteHelper.ColorToMemFormat(p.crusherpads3));
            Assert.AreEqual("7305", PaletteHelper.ColorToMemFormat(p.crusherpads4));
            Assert.AreEqual("5200", PaletteHelper.ColorToMemFormat(p.crusherpads5));

            Assert.AreEqual("EC0B", PaletteHelper.ColorToMemFormat(p.crusherhands1));
            Assert.AreEqual("CA08", PaletteHelper.ColorToMemFormat(p.crusherhands2));

            Assert.AreEqual("EF0D", PaletteHelper.ColorToMemFormat(p.crusherflame1));
            Assert.AreEqual("F80E", PaletteHelper.ColorToMemFormat(p.crusherflame2));


            s = Sprite.bis1sprite;
            p = new Sprite(s);
            Assert.AreEqual("FE0B", PaletteHelper.ColorToMemFormat(p.costume1));
            Assert.AreEqual("EB08", PaletteHelper.ColorToMemFormat(p.costume2));
            Assert.AreEqual("C705", PaletteHelper.ColorToMemFormat(p.costume3));
            Assert.AreEqual("9503", PaletteHelper.ColorToMemFormat(p.costume4));
            Assert.AreEqual("7300", PaletteHelper.ColorToMemFormat(p.costume5));
        }


        [TestMethod]
        public void GenerateStandSpriteTest()
        {
            Bitmap sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand1);
            var s = new Sprite(Sprite.bis1sprite);
            Bitmap sprite_result = s.GenerateStandingSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicstand0);
            s = new Sprite(Sprite.bis0sprite);
            sprite_result = s.GenerateStandingSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));
        }

        [TestMethod]
        public void GeneratePsychoPunchSpriteTest()
        {
        }


        [TestMethod]
        public void GeneratePsychoPrepSpriteTest()
        {
            Bitmap sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicpsychoprep5);
            var s = new Sprite(Sprite.bis5sprite);
            Bitmap sprite_result = s.GeneratePsychoPrepSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicpsychoprep1);
            s = new Sprite(Sprite.bis1sprite);
            sprite_result = s.GeneratePsychoPrepSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.dicpsychoprep0);
            s = new Sprite(Sprite.bis0sprite);
            sprite_result = s.GeneratePsychoPrepSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));
        }

        [TestMethod]
        public void GeneratePsychoCrusherSpriteTest()
        {
            Bitmap sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.diccrusher1_5);
            var s = new Sprite(Sprite.bis5sprite);
            Bitmap sprite_result = s.GenerateCrusherTopSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.diccrusher2_5);
            s = new Sprite(Sprite.bis5sprite);
            sprite_result = s.GenerateCrusherSideSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.diccrusher1_0);
            s = new Sprite(Sprite.bis0sprite);
            sprite_result = s.GenerateCrusherTopSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.diccrusher2_0);
            s = new Sprite(Sprite.bis0sprite);
            sprite_result = s.GenerateCrusherSideSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));

            sprite_expected = new Bitmap(PaletteSwap.Properties.Resources.diccrusher2_1);
            s = new Sprite(Sprite.bis1sprite);
            sprite_result = s.GenerateCrusherSideSprite();
            Assert.IsTrue(PaletteHelper.areBitmapsSame(sprite_expected, sprite_result));
        }


        [TestMethod]
        public void WriteSpriteByteStreamBasicTest()
        {
            // test #1
            // check that a bis0sprite's byte representation is what we expect 
            var sprite = new Sprite(Sprite.bis0sprite);
            string s_expected = Sprite.spriteAsTextLine(Sprite.bis0sprite);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);

            var data_result = sprite.ByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                Assert.AreEqual(data_expected[0], data_result[0]);
            }
        }




        [TestMethod]
        public void WriteSpriteByteStreamChangeFieldsTest()
        {
            var sprite = new Sprite(Sprite.bis0sprite);
            string s_expected = Sprite.spriteAsTextLine(Sprite.bis0sprite);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            // test #2
            // modify a few fields, check that results are what we expect 

            //  pads 5
            data_expected[2] = 0x23;
            data_expected[3] = 0x01;
            data_expected[2 + 3 * 32] = 0x23;
            data_expected[3 + 3 * 32] = 0x01;
            data_expected[2 + 4 * 32] = 0x23;
            data_expected[3 + 4 * 32] = 0x01;

            // costume 5
            data_expected[4] = 0x12;
            data_expected[5] = 0x03;
            data_expected[4 + 3 * 32] = 0x12;
            data_expected[5 + 3 * 32] = 0x03;
            data_expected[4 + 4 * 32] = 0x12;
            data_expected[5 + 4 * 32] = 0x03;

            sprite.pads5 = Color.FromArgb(0, 17, 34, 51);
            sprite.costume5 = Color.FromArgb(0, 51, 17, 34);

            var data_result = sprite.ByteStream();
            for (int i = 0; i < data_expected.Length; i++)
            {
                if (Sprite.unusedOffsets.ContainsKey(i))
                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }
        [TestMethod]
        public void WriteSpriteByteStreamChangeSpriteTest()
        {
            var sprite = new Sprite(Sprite.bis0sprite);
            string s_expected = Sprite.spriteAsTextLine(Sprite.bis0sprite);
            var data_expected = PaletteHelper.StringToByteStream(s_expected);
            // test #3
            // change all fields in sprite0 to sprite5, check new byte 
            // representation is correct

            var sprite0 = new Sprite(Sprite.bis0sprite);
            var sprite5 = new Sprite(Sprite.bis5sprite);

            Assert.AreNotEqual(sprite5.costume1, sprite0.costume1);

            var spriteType = sprite.GetType();
            foreach (var label in Enum.GetNames(typeof(Sprite.SPRITE_COLORS)))
            {
                var myFieldInfo = spriteType.GetField(label.ToString());
                var sprite5color = (Color)myFieldInfo.GetValue(sprite5);
                myFieldInfo.SetValue(sprite0, sprite5color);
            }

            Assert.AreEqual(sprite5.costume1, sprite0.costume1);

            // check that they have identical byte strings
            var data_result = sprite0.ByteStream();
            s_expected = Sprite.spriteAsTextLine(Sprite.bis5sprite);
            data_expected = PaletteHelper.StringToByteStream(s_expected);

            for (int i = 0; i < data_expected.Length; i++)
            {
                if (Sprite.unusedOffsets.ContainsKey(i))
                    continue;
                if (i == 157) // bug in data for bison5, this is a data bug in the rom
                    continue;
                Assert.AreEqual(data_expected[i], data_result[i]);
            }
        }

        [TestMethod]
        public void UnusedOffsetsTest()
        {
            Assert.IsTrue(Sprite.unusedOffsets[34]);
            Assert.IsTrue(Sprite.unusedOffsets[35]);
            Assert.IsTrue(Sprite.unusedOffsets[68]);
            Assert.IsTrue(Sprite.unusedOffsets[69]);
            Assert.IsTrue(Sprite.unusedOffsets[94]);
            Assert.IsTrue(Sprite.unusedOffsets[95]);
        }

        [TestMethod]
        public void WriteSpriteToColFormatTest()
        {
            string s = Sprite.bis0sprite;
            var sprite = new Sprite(s);

            string string_expected = "255 255 204\r\n221 204 136\r\n153 119 51\r\n85 68 0\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n0 0 136\r\n255 255 119\r\n255 187 0\r\n255 119 0\r\n170 51 0\r\n119 0 0\r\n255 187 0\r\n255 255 255\r\n255 255 255\r\n255 238 153\r\n238 170 68\r\n238 119 0\r\n221 68 0\r\n187 238 255\r\n102 204 238\r\n68 153 204\r\n34 119 170\r\n255 255 255\r\n255 255 136\r\n238 187 0\r\n187 119 0\r\n153 85 0\r\n85 187 136\r\n68 136 102\r\n255 255 204\r\n119 221 187\r\n";
            string string_result = sprite.ToColFormat();
            Assert.AreEqual(string_expected, string_result);

            sprite.skin1 = Color.FromArgb(0, 17, 34, 51);
            string_expected = "17 34 51\r\n221 204 136\r\n153 119 51\r\n85 68 0\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n0 0 136\r\n255 255 119\r\n255 187 0\r\n255 119 0\r\n170 51 0\r\n119 0 0\r\n255 187 0\r\n255 255 255\r\n255 255 255\r\n255 238 153\r\n238 170 68\r\n238 119 0\r\n221 68 0\r\n187 238 255\r\n102 204 238\r\n68 153 204\r\n34 119 170\r\n255 255 255\r\n255 255 136\r\n238 187 0\r\n187 119 0\r\n153 85 0\r\n85 187 136\r\n68 136 102\r\n255 255 204\r\n119 221 187\r\n";
            string_result = sprite.ToColFormat();
            Assert.AreEqual(string_expected, string_result);
        }

        [TestMethod]
        public void LoadSpriteFromColFormatTest()
        {
            string s = "17 34 51\r\n221 204 136\r\n153 119 51\r\n85 68 0\r\n0 136 238\r\n51 102 221\r\n0 68 204\r\n34 34 170\r\n0 0 136\r\n255 255 119\r\n255 187 0\r\n255 119 0\r\n170 51 0\r\n119 0 0\r\n255 187 0\r\n255 255 255\r\n255 255 255\r\n255 238 153\r\n238 170 68\r\n238 119 0\r\n221 68 0\r\n187 238 255\r\n102 204 238\r\n68 153 204\r\n34 119 170\r\n255 255 255\r\n255 255 136\r\n238 187 0\r\n187 119 0\r\n153 85 0\r\n85 187 136\r\n68 136 102\r\n255 255 204\r\n119 221 187\r\n";
            var sprite = Sprite.LoadFromColFormat(s);
            string string_result = sprite.ToColFormat();
            Assert.AreEqual(s, string_result);
        }

        [TestMethod]
        public void ColorFromSpriteLabelTest()
        {
            string s = Sprite.bis0sprite;
            var sprite = new Sprite(s);

            var myType = sprite.GetType();
            foreach (var label in Enum.GetNames(typeof(Sprite.SPRITE_COLORS)))
            {
                var myFieldInfo = myType.GetField(label.ToString());
                var expected = (Color)myFieldInfo.GetValue(sprite);
                var sprite_color = (Sprite.SPRITE_COLORS)Enum.Parse(typeof(Sprite.SPRITE_COLORS), label);
                var result = sprite.ColorFromSpriteColor(sprite_color);
                Assert.AreEqual(expected, result);
            }
        }
    }
}
