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
            var s = new DictatorSprite();

            string bar = s.printFirstColor<DictatorSprite.PALETTE_COLORS>();
            int x = 5;
            x = x + 1;
            s.setColor(DictatorSprite.PALETTE_COLORS.costume1, Color.AliceBlue);
            var c = s.getColor(DictatorSprite.PALETTE_COLORS.costume1);
            x = x + 4;
            var f = s.EnumNamedValues<DictatorSprite.PALETTE_COLORS>();
            x = 7;
            var test = s.ColorFromEnum<DictatorSprite.PALETTE_COLORS>(DictatorSprite.PALETTE_COLORS.costume1);
            x = 0;

        }
    }
}
