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

            var spriteType = s.GetType();
            foreach (var label in Enum.GetNames(typeof(SpriteTestClass.PALETTE_COLORS)))
            {
                Console.WriteLine(label);
            }

            string foo = s.first_enum();
            int x = 0;
            x = x + 1;
        }
    }
}
