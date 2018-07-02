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
            Assert.AreEqual("Padar�me darb�, kur� tur�jome padaryti gynyboje � tai ir pad�jo laim�ti. Rungtyn�s nebuvo idealios, " +
                "ta�iau �gyvendinome savo plan� ir savo s�skaitoje turime dar vien� pergal�. Judame, padedame vienas kitam, vyksta " +
                "ka�kokios gynybin�s rotacijos. Randome kaip u�kai�yti skyles, tai mums ir padeda apsiginti ir laim�ti�, � rimtai " +
                "interviu prad�ioje kalb�jo vidurio puol�jas. Lietuvos rinktin� did�i�j� rungtyni� dal� savo �aidim� grind� per " +
                "auk�ta�gius. Paklaustas, ar nepavargo, J. Valan�i�nas prajuko. ��aid�iame po 15 minu�i�, mums dar po 25 metus � " +
                "negalime pavargti�, � pa�ym�jo jis. Netrukus juoko buvo dar daugiau, nes bekalbant� Jon� i� trib�n� pertrauk� " +
                "krep�ininko vard� ir pavard� �m� skanduoti Lietuvos krep�inio sirgaliai. �Labai trukdo u� man�s esantys susikaupti. " +
                "Tie ka�kokie �ali �mogeliukai, � juok�si J. Valan�i�nas. � A� manau, kad mes d�l t� �moni� ir �aid�iame. Fan�, kurie " +
                "liko Lietuvoje ir kurie atva�iavo �ia. Eina pagaugai, ane? Kai girdi tokius dalykus, d�l to ir �aidi.� Paklaustas, " +
                "ar pad�s rinktinei per kit� atrankos var�yb� �lang�� rugs�jo m�nes�, JV tvirtino turintis pasikonsultuoti su Kanados " +
                "klubo atstovais. �S�sim su �Raptors� klubu, �i�r�sime kada yra pasiruo�imas sezonui, kada suplanuota treniruo�i� " +
                "stovykla. Jeigu tik laikas leis, b�tinai�, � teig� auk�ta�gis. Kitame atrankos var�yb� cikle Lietuvos rinktinei " +
                "teks �aisti su Italijos, Kroatijos ir Nyderland� rinktin�mis. Lietuviai jau dabar yra tap� vienvald�iais naujai " +
                "suformuotos J grup�s lyderiais, artimiausius persekiotojus lenkdami 2 ta�kais.", text);
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

            List<string> textInPairs = textConverter.SplitTextIntoParts("�odis �odis �odis", 13);

            CollectionAssert.AreEqual(new List<string> { "�odis �odis ", "�odis" }, textInPairs);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SplitTextIntoParts_IfSymbolsCountInRowNumberIsLessThenZero_ShouldBeArgumentOfRangeException()
        {
            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("�odis �odis �odis", -1);
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

            List<string> textInPairs = textConverter.SplitTextIntoParts("�odis �odis �odis", 13);

            Program.PrintToFile("rez.txt", textInPairs);

            string[] lines = File.ReadAllLines("rez.txt");

            Assert.AreEqual(2, lines.Length);
        }
    }
}
