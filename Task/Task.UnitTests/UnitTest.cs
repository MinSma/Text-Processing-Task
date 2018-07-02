using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task.UnitTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void ReadFile_IfFileExists_ShouldReturnTwoValues()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("duomenys.txt");

            Assert.AreEqual(8, symbolsCountInRow);
            Assert.AreEqual("Padarëme darbà, kurá turëjome padaryti gynyboje – tai ir padëjo laimëti. Rungtynës nebuvo idealios, " +
                "taèiau ágyvendinome savo planà ir savo sàskaitoje turime dar vienà pergalæ. Judame, padedame vienas kitam, vyksta " +
                "kaþkokios gynybinës rotacijos. Randome kaip uþkaiðyti skyles, tai mums ir padeda apsiginti ir laimëti“, – rimtai " +
                "interviu pradþioje kalbëjo vidurio puolëjas. Lietuvos rinktinë didþiàjà rungtyniø dalá savo þaidimà grindë per " +
                "aukðtaûgius. Paklaustas, ar nepavargo, J. Valanèiûnas prajuko. „Þaidþiame po 15 minuèiø, mums dar po 25 metus – " +
                "negalime pavargti“, – paþymëjo jis. Netrukus juoko buvo dar daugiau, nes bekalbantá Jonà ið tribûnø pertraukë " +
                "krepðininko vardà ir pavardæ ëmæ skanduoti Lietuvos krepðinio sirgaliai. „Labai trukdo uþ manæs esantys susikaupti. " +
                "Tie kaþkokie þali þmogeliukai, – juokësi J. Valanèiûnas. – Að manau, kad mes dël tø þmoniø ir þaidþiame. Fanø, kurie " +
                "liko Lietuvoje ir kurie atvaþiavo èia. Eina pagaugai, ane? Kai girdi tokius dalykus, dël to ir þaidi.“ Paklaustas, " +
                "ar padës rinktinei per kità atrankos varþybø „langà“ rugsëjo mënesá, JV tvirtino turintis pasikonsultuoti su Kanados " +
                "klubo atstovais. „Sësim su „Raptors“ klubu, þiûrësime kada yra pasiruoðimas sezonui, kada suplanuota treniruoèiø " +
                "stovykla. Jeigu tik laikas leis, bûtinai“, – teigë aukðtaûgis. Kitame atrankos varþybø cikle Lietuvos rinktinei " +
                "teks þaisti su Italijos, Kroatijos ir Nyderlandø rinktinëmis. Lietuviai jau dabar yra tapæ vienvaldþiais naujai " +
                "suformuotos J grupës lyderiais, artimiausius persekiotojus lenkdami 2 taðkais.", text);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFile_IfFileNotExists_ShouldThrowFileNotFoundException()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("aaaa.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadFile_IfFileFormatIsWrong_ShouldThrowFormatException()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("wrong_data_for_test.txt");
        }

        [TestMethod]
        public void SplitTextIntoParts_IfWorks_ShouldReturnListWithValues()
        {
            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("þodis þodis þodis", 13);

            CollectionAssert.AreEqual(new List<string> { "þodis þodis ", "þodis" }, textInPairs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SplitTextIntoParts_IfSymbolsCountInRowNumberIsLessThenZero_ShouldBeArgumentOfRangeException()
        {
            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("þodis þodis þodis", -1);
        }

        [TestMethod]
        public void SplitTextIntoParts_IfTextNotExist_ShouldReturnListWithFirstClearEment()
        {
            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("", 10);

            Assert.AreEqual("", textInPairs[0]);
        }

        [TestMethod]
        public void PrintToFile_IfWorks_ShoudCreateResultsFile()
        {
            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("þodis þodis þodis", 13);

            Program.PrintToFile("rez.txt", textInPairs);

            string[] lines = File.ReadAllLines("rez.txt");

            Assert.AreEqual(2, lines.Length);
        }
    }
}
