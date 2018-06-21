using System;
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
    }
}
