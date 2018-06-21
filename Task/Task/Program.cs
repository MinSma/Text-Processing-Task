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
                Console.WriteLine("Error. Wrong file format!");
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
                        textInPairs.Add(textInWords[i].Substring(0, symbolsCountInRow));
                        textInWords[i] = textInWords[i].Remove(0, symbolsCountInRow);

                        WhileWordLengthMoreThenZero(textInPairs, textInWords[i], symbolsCountInRow);
                    }
                }
                else
                {
                    int leftSymbolsAmountInRow = symbolsCountInRow - textInPairs[textInPairs.Count - 1].Length;

                    if (textInWords[i].Length / 2 >= leftSymbolsAmountInRow)
                    {
                        if (symbolsCountInRow >= textInWords[i].Length)
                        {
                            textInPairs.Add(textInWords[i].Substring(0, textInWords[i].Length) + " ");
                        }
                        else
                        {
                            WhileWordLengthMoreThenZero(textInPairs, textInWords[i], symbolsCountInRow);
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

        public static void WhileWordLengthMoreThenZero(List<string> textInPairs, string word, int symbolsCountInRow)
        {
            while (word.Length > 0)
            {
                if (word.Length >= symbolsCountInRow)
                {
                    textInPairs.Add(word.Substring(0, symbolsCountInRow));
                    textInPairs[textInPairs.Count - 1] += " ";
                    word = word.Remove(0, symbolsCountInRow);
                }
                else
                {
                    textInPairs.Add(word.Substring(0, word.Length));
                    textInPairs[textInPairs.Count - 1] += " ";
                    word = word.Remove(0, word.Length);
                }
            }
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
