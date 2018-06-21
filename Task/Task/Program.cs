using System;
using System.Collections.Generic;
using System.IO;

namespace Task
{
    class Program
    {
        const string PRIMARY_DATA_FILE = "duomenys.txt";
        const string RESULTS_DATA_FILE = "rez.txt";

        static void Main(string[] args)
        {
            string text;
            int symbolsCountInRow;

            ReadFile(PRIMARY_DATA_FILE, out text, out symbolsCountInRow);

            List<string> textInPairs = SplitTextIntoParts(text, symbolsCountInRow);

            PrintToConsole(textInPairs);
            PrintToFile(RESULTS_DATA_FILE, textInPairs);
        }

        public static void ReadFile(string fileName, out string text, out int symbolsCountInRow)
        {
            text = "";
            symbolsCountInRow = 0;

            try
            {
                string[] lines = File.ReadAllLines(fileName);

                text = lines[0];
                symbolsCountInRow = Convert.ToInt32(lines[1]);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error. File not found!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Error. Wrong file data format!");
            }
        }

        public static List<string> SplitTextIntoParts(string text, int symbolsCountInRow)
        {
            List<string> textInPairs = new List<string>();
            List<string> textInWords = new List<string>();

            textInWords.AddRange(text.Split(" "));

            for (int i = 0; i < textInWords.Count; i++)
            {
                if (textInPairs.Count == 0)
                {
                    FirstWordProcessing(i, textInWords, textInPairs, symbolsCountInRow);
                }
                else
                {
                    WordsAfterFirstWordProcessing(i, textInWords, textInPairs, symbolsCountInRow);
                }
            }

            return textInPairs;
        }

        public static void FirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            if (textInWords[index].Length <= symbolsCountInRow)
            {
                textInPairs.Add(textInWords[index] + " ");
            }
            else
            {
                AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
            }
        }

        public static void WordsAfterFirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            int leftSymbolsAmountInRow = symbolsCountInRow - textInPairs[textInPairs.Count - 1].Length;

            if (textInWords[index].Length / 2 >= leftSymbolsAmountInRow)
            {
                if (textInWords[index].Length <= symbolsCountInRow)
                {
                    textInPairs.Add(textInWords[index].Substring(0, textInWords[index].Length) + " ");
                }
                else
                {
                    AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
                }
            }
            else
            {
                textInPairs[textInPairs.Count - 1] += textInWords[index] + " ";
            }
        }

        public static void AddWordToPartsListWhileReducingWordLength(List<string> textInPairs, string word, int symbolsCountInRow)
        {
            while (word.Length > 0)
            {
                word = word.Length >= symbolsCountInRow ? 
                    ReduceWordAndAddWordToList(textInPairs, word, symbolsCountInRow) : 
                    ReduceWordAndAddWordToList(textInPairs, word, word.Length);
            }
        }

        public static string ReduceWordAndAddWordToList(List<string> textInPairs, string word, int length)
        {
            textInPairs.Add(word.Substring(0, length));
            textInPairs[textInPairs.Count - 1] += " ";

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
