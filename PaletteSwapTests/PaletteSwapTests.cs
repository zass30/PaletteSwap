using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTests
{
    [TestClass]
    public class PaletteSwapTests
    {
        [TestMethod]
        public void PaletteACT()
        {
            var pal = new Palette();
            string sACT = new string("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            string result = pal.asACT();
            Assert.AreEqual(sACT, pal.asACT());
        }

        [TestMethod]
        public void PaletteMem()
        {
            var pal = new Palette();
            string sMem = new string("0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000");
            string result = pal.asMem();
            Assert.AreEqual(sMem, result);
        }

        [TestMethod]
        public void PaletteFromACT()
        {
            string s = "77 00 00 00 77 33 33 99 55 55 CC 77 88 EE BB BB FF EE AA 00 00 CC 44 00 FF BB 77 FF 88 44 DD 55 00 FF EE BB EE BB 77 AA 77 44 66 44 33";
            var pal = Palette.PaletteFromACT(s);
            string result = pal.asACT();
            Assert.AreEqual(s, result);

            s = "0007 7300 9503 C705 EB08 FE0B 000A 400C B70F 840F 500D EB0F B70E 740A 4306";
            result = pal.asMem();
            Assert.AreEqual(s, result);
        }

        [TestMethod]
        public void PaletteFromMem()
        {
            string s = "0007 7300 9503 C705 EB08 FE0B 000A 400C B70F 840F 500D EB0F B70E 740A 4306";
            var pal = Palette.PaletteFromMem(s);
            string result = pal.asMem();
            Assert.AreEqual(s, result);

            s = "77 00 00 00 77 33 33 99 55 55 CC 77 88 EE BB BB FF EE AA 00 00 CC 44 00 FF BB 77 FF 88 44 DD 55 00 FF EE BB EE BB 77 AA 77 44 66 44 33";
            result = pal.asACT();
            Assert.AreEqual(s, result);
        }
    }
}
