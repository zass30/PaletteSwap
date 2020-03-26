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
            var c = Color.AliceBlue;
            s.setColor("skin1", c);
            var result = s.getColor("skin1");
            Assert.AreEqual(c, result);

            s.setOffsets("skin1", new List<int>() { 0, 4, 5});
            var r_offset = s.getOffsets("skin1");
            Assert.AreEqual(0, r_offset[0]);
            Assert.AreEqual(5, r_offset[2]);

            s.memlen = 10;
            var b_result = s.memoryRepresentation();
            Assert.AreEqual(10, b_result.Length);


        }
    }
}
