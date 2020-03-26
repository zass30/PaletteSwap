using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class PaletteTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var s = new Palette();
            var c = Color.AliceBlue;
            s.setColor("skin1", c);
            var result = s.getColor("skin1");
            Assert.AreEqual(c, result);

        }
    }
}
