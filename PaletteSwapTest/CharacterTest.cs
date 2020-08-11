using System;
using System.Drawing;
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
            var dic = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.lp);
            var s = dic.sprite;
            var pal = s.GetImage("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.DIC_neutral1, pal.baseImage));

            var remapped_img = s.GetBitmap("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.DIC_neutral0, remapped_img));

            dic = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.mp);
            s = dic.sprite;
            remapped_img = s.GetBitmap("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.DIC_neutral1, remapped_img));
        }

        [TestMethod]
        public void GetDictatorPsychoPunchImageTest()
        {
            var dic = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.mp);
            var s = dic.sprite;
            var remapped_img = s.GetBitmap("psychopunch");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.DIC_psychopunch1, remapped_img));

            dic = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Dictator, CharacterConfig.BUTTONS.hk);
            s = dic.sprite;
            remapped_img = s.GetBitmap("psychopunch");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.DIC_psychopunch5, remapped_img));
        }

        [TestMethod]
        public void GetClawStandingNeutralImageTest()
        {
            var claw = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold);
            var s = claw.sprite;
            var pal = s.GetImage("neutral");
            Assert.IsTrue(PaletteHelper.areBitmapsSameSkipTransparencies(PaletteSwap.Properties.Resources.CLA_neutral7, pal.baseImage));
        }

        [TestMethod]
        public void GetClawScaledStandingNeutralImageTest()
        {
            var claw = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold);
            var s = claw.sprite;
            var image = s.GetImage("neutral").RemappedImage();
            var scaledImage = s.GetImage("neutral").RemappedScaledImage();
            Assert.AreEqual(image.Height * 4, scaledImage.Height);
            Assert.AreEqual(image.Width * 4, scaledImage.Width);
        }

        [TestMethod]
        public void ToColFormatTest()
        {
            var claw = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Claw, CharacterConfig.BUTTONS.hold);
            var s_expected = claw.ToColFormat();

            var result = Character.CharacterFromColFormat(s_expected);
            var s_result = result.ToColFormat();
            Assert.AreEqual(s_expected, s_result);
        }

        [TestMethod]
        public void CharacterRemapTest()
        {/*
            var gou = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Gouki, CharacterConfig.BUTTONS.lp);
            var gou_remap = gou.sprite.GetBitmap("teleport1");
            int x = 0;
            gou_remap.Save(@"C:\temp\tele.png");
            gou.sprite.SetColor("t1costume2", Color.CornflowerBlue);
            gou_remap.Save(@"C:\temp\tele.png");
            //   var ryu = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Ryu, CharacterConfig.BUTTONS.lp);
            //   var ryu_remap = ryu.GetBitmap("neutral");

            var dee = Character.CreateDefaultCharacter(CharacterConfig.CHARACTERS.Deejay
                   , CharacterConfig.BUTTONS.lp);
               var remapimg = dee.GetBitmap("neutral");
               remapimg.Save(@"C:\temp\deen.png");
                remapimg = dee.GetBitmap("victory");
               remapimg.Save(@"C:\temp\deev.png");
                remapimg = dee.GetBitmap("loss");
               remapimg.Save(@"C:\temp\deel.png"); */
/*
               // check honda's byte stream? maybe that's wrong
               var s = PaletteHelper.ByteStreamToString(CharacterConfig.GetSpriteResourceFromRom(CharacterConfig.CHARACTERS.Ehonda, CharacterConfig.BUTTONS.lp));
               int x = 0;
               x++;*/
        }
    }
}
