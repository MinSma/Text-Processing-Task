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
        public void ReadFile_IfFileExists_ReturnTwoValues()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("duomenys.txt");

            Assert.AreEqual(13, symbolsCountInRow);
            Assert.AreEqual("þodis þodis þodis", text);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ReadFile_IfFileNotExists_GetException()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("aaaa.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ReadFile_IfFileFormatIsWrong_GetException()
        {
            (string text, int symbolsCountInRow) = Program.ReadFile("wrong_data_for_test.txt");
        }
    }
}
