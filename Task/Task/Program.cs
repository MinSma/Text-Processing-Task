using System;
using System.Collections.Generic;
using System.IO;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            int symbolsCountInRow;

            ReadFile("duomenys.txt", out text, out symbolsCountInRow);

            List<string> textInPairs = SplitTextIntoParts(text, symbolsCountInRow);

            PrintToConsole(textInPairs);
            PrintToFile("rez.txt", textInPairs);
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
                    if (textInWords[i].Length <= symbolsCountInRow)
                    {
                        textInPairs.Add(textInWords[i] + " ");
                    }
                    else
                    {
                        AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[i], symbolsCountInRow);
                    }
                }
                else
                {
                    int leftSymbolsAmountInRow = symbolsCountInRow - textInPairs[textInPairs.Count - 1].Length;

                    if (textInWords[i].Length / 2 >= leftSymbolsAmountInRow)
                    {
                        if (textInWords[i].Length <= symbolsCountInRow)
                        {
                            textInPairs.Add(textInWords[i].Substring(0, textInWords[i].Length) + " ");
                        }
                        else
                        {
                            AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[i], symbolsCountInRow);
                        }
                    }
                    else
                    {
                        textInPairs[textInPairs.Count - 1] += textInWords[i] + " ";
                    }
                }
            }

            return textInPairs;
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
