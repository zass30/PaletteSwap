﻿using System;
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
    }
}
