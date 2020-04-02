using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaletteSwap;

namespace PaletteSwapTest
{
    [TestClass]
    public class CharacterTest
    {
        [TestMethod]
        public void GetDictatorStandingNeutralImageTest()
        {
            var dic = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.lp);
            var s = dic.sprite;
            var pal = s.GetImage("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.dicstand1, pal.baseImage));

            var remapped_img = s.GetBitmap("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.dicstand0, remapped_img));

            dic = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mp);
            s = dic.sprite;
            remapped_img = s.GetBitmap("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.dicstand1, remapped_img));
        }

        [TestMethod]
        public void GetDictatorPsychoPunchImageTest()
        {
            var dic = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.mp);
            var s = dic.sprite;
            var remapped_img = s.GetBitmap("psychopunch");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.dicmp1, remapped_img));

            dic = Character.createDefaultCharacter(Character.CHARACTERS.Dictator, Character.BUTTONS.hk);
            s = dic.sprite;
            remapped_img = s.GetBitmap("psychopunch");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.dicmp5, remapped_img));
        }

        [TestMethod]
        public void GetClawStandingNeutralImageTest()
        {
            var claw = Character.createDefaultCharacter(Character.CHARACTERS.Claw, Character.BUTTONS.hold);
            var s = claw.sprite;
            var pal = s.GetImage("neutral"); //113 h  by 75 w
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.clawneutral7, pal.baseImage));
        }
    }
}
