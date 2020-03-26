using System;
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
            var s = new SpriteTestClass();
            var p = new PortraitTestClass();

            string foo = s.first_enum();
            int x = 0;
            x = x + 1;

            string bar = s.first_enum_templated<SpriteTestClass.PALETTE_COLORS>();
            x = x + 1;
        }
    }
}
