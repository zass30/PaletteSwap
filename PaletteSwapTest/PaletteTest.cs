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
            var c = Color.FromArgb(0, 17, 34, 51);
            s.setColor("skin1", c);
            var result = s.getColor("skin1");
            Assert.AreEqual(c, result);

            s.setOffsets("skin1", new List<int>() { 0, 4, 6});
            var r_offset = s.getOffsets("skin1");
            Assert.AreEqual(0, r_offset[0]);
            Assert.AreEqual(6, r_offset[2]);

            s.memlen = 10;
            var b_result = s.memoryRepresentation();
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
        public void PaletteLoadTest()
        {
        }
        }
}
