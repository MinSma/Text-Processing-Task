using System;
using System.Collections.Generic;
using System.IO;

namespace Task
{
    public class Program
    {
        const string PRIMARY_DATA_FILE = "duomenys.txt";
        const string RESULTS_DATA_FILE = "rez.txt";

        static void Main(string[] args)
        {
            (string text, int symbolsCountInRow) = ReadFile(PRIMARY_DATA_FILE);

            List<string> textInPairs = SplitTextIntoParts(text, symbolsCountInRow);

            PrintToConsole(textInPairs);
            PrintToFile(RESULTS_DATA_FILE, textInPairs);
        }

        public static (string text, int symbolsCountInRow) ReadFile(string fileName)
        {
            string text = "";
            int symbolsCountInRow = 0;

            string[] lines = File.ReadAllLines(fileName);

            text = lines[0];
            symbolsCountInRow = Convert.ToInt32(lines[1]);

            return (text, symbolsCountInRow);
        }

        public static List<string> SplitTextIntoParts(string text, int symbolsCountInRow)
        {
            List<string> textInPairs = new List<string>();
            List<string> textInWords = new List<string>();

            textInWords.AddRange(text.Split(" "));

            for (int i = 0; i < textInWords.Count; i++)
            {
                // if the first word
                if (textInPairs.Count == 0)
                {
                    FirstWordProcessing(i, textInWords, textInPairs, symbolsCountInRow);
                }
                else // all other words after the first one
                {
                    WordsAfterFirstWordProcessing(i, textInWords, textInPairs, symbolsCountInRow);
                }
            }

            return textInPairs;
        }

        public static void FirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            // if there is enough room for word to fill in one row
            if (textInWords[index].Length <= symbolsCountInRow)
            {
                // add word to new line of text pairs list
                textInPairs.Add(textInWords[index] + " ");
            }
            else // if there is not enough room for word to fill in one row
            {
                AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
            }
        }

        public static void WordsAfterFirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            int leftSymbolsAmountInRow = symbolsCountInRow - textInPairs[textInPairs.Count - 1].Length; // number of empty space in the row


            if (textInWords[index].Length / 2 >= leftSymbolsAmountInRow) // if word half size is bigger than empty space in the row
            {
                if (textInWords[index].Length <= symbolsCountInRow) // if word size is lower than max row size
                {
                    // add word to new line of text pairs list
                    textInPairs.Add(textInWords[index].Substring(0, textInWords[index].Length) + " ");
                }
                else // if word size is bigger than max row size
                {
                    AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
                }
            }
            else // if word half size is lower than empty space in the row
            {
                // add word to last used line of text pairs list
                textInPairs[textInPairs.Count - 1] += textInWords[index] + " ";
            }

        }

        public static void AddWordToPartsListWhileReducingWordLength(List<string> textInPairs, string word, int symbolsCountInRow)
        {
            while (word.Length > 0)
            {
                // add word to textInParts list and
                // if word length is equal or bigger than symbolsCountInRow than reduce it by symbolsCountInRow value
                // otherwise reduce it by word's length
                word = word.Length >= symbolsCountInRow ?
                    ReduceWordAndAddWordToList(textInPairs, word, symbolsCountInRow) :
                    ReduceWordAndAddWordToList(textInPairs, word, word.Length);
            }
        }

        public static string ReduceWordAndAddWordToList(List<string> textInPairs, string word, int length)
        {
            // Adds word to list and adds white space at the end of word
            textInPairs.Add(word.Substring(0, length));
            textInPairs[textInPairs.Count - 1] += " ";

            // Reduces word by given length
            return word.Remove(0, length);
        }

        public static void PrintToConsole(List<string> textInPairs)
        {
            foreach (var t in textInPairs)
            {
                Console.WriteLine(t);
            }
        }

        public static void PrintToFile(string fileName, List<string> textInPairs)
        {
            File.WriteAllLines(fileName, textInPairs);
        }
    }
}
