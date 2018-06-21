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
            Assert.AreEqual("�odis �odis �odis", text);
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

        [TestMethod]
        public void SplitTextIntoParts_IfWorks_ReturnsListWithValues()
        {
            List<string> textInPairs = Program.SplitTextIntoParts("�odis �odis �odis", 13);

            Assert.AreEqual(2, textInPairs.Count);
        }

        [TestMethod]
        public void PrintToFile_IfPrints_CreatesResultsFile()
        {
            List<string> values = new List<string> { "abc", "adc", "bde", "dea" };

            Program.PrintToFile("rez.txt", values);

            string[] lines = File.ReadAllLines("rez.txt");

            Assert.AreEqual(4, lines.Length);
        }

        [TestMethod]
        public void ReduceWordAndAddWordToList_IfWorks_ReducesWordAndPutItInList()
        {
            List<string> values = new List<string>();

            string word = Program.ReduceWordAndAddWordToList(values, "abbbbbbbbcd", 5);

            Assert.AreEqual("abbbb ", values[0]);
            Assert.AreEqual(6, word.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReduceWordAndAddWordToList_IfFails_GetException()
        {
            List<string> values = new List<string>();

            string word = Program.ReduceWordAndAddWordToList(values, "abc", 5);
        }

        [TestMethod]
        public void AddWordToPartsListWhileReducingWordLength_IfWorks_ReturnsWordSplittedIntoList()
        {
            List<string> values = new List<string>();
            string word = "abbccccddddd";

            Program.AddWordToPartsListWhileReducingWordLength(values, word, 3);

            Assert.AreEqual(4, values.Count);
        }

        [TestMethod]
        public void FirstWordProcessing_IfWorks_IncreaseList()
        {
            List<string> words = new List<string>() { "zodiakas" };
            List<string> parts = new List<string>();

            Program.FirstWordProcessing(0, words, parts, 5);

            Assert.AreEqual(2, parts.Count);
        }

        [TestMethod]
        public void WordsAfterFirstWordProcessing_IfPartsListNotEmpty_IncreaseList()
        {
            List<string> words = new List<string>() { "zodiakas" };
            List<string> parts = new List<string>() { "la " };

            Program.WordsAfterFirstWordProcessing(0, words, parts, 5);

            Assert.AreEqual(3, parts.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WordsAfterFirstWordProcessing_IfPartsListIsEmpty_IncreaseList()
        {
            List<string> words = new List<string>() { "zodiakas" };
            List<string> parts = new List<string>();

            Program.WordsAfterFirstWordProcessing(0, words, parts, 5);
        }
    }
}
