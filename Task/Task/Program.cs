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

            TextConverter textConverter = new TextConverter();

            List<string> textInPairs = textConverter.SplitTextIntoParts("", symbolsCountInRow);

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
