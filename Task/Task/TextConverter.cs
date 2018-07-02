using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task
{
    public class TextConverter
    {
        List<string> textInPairs;
        List<string> textInWords;

        public TextConverter()
        {
            textInPairs = new List<string>();
            textInWords = new List<string>();
        }

        public List<string> SplitTextIntoParts(string text, int symbolsCountInRow)
        {
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

        void FirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            // if there is enough room for word to fill in one row
            if (textInWords[index].Length <= symbolsCountInRow)
            {
                // add word to new line of text pairs list
                textInPairs.Add(textInWords[index]);
                textInPairs[textInPairs.Count - 1] += IfNotLastAddWhiteSpace(index, textInWords.Count - 1);
            }
            else // if there is not enough room for word to fill in one row
            {
                AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
                textInPairs[textInPairs.Count - 1] += IfNotLastAddWhiteSpace(index, textInWords.Count - 1);
            }
        }

        void WordsAfterFirstWordProcessing(int index, List<string> textInWords, List<string> textInPairs, int symbolsCountInRow)
        {
            int leftSymbolsAmountInRow = symbolsCountInRow - textInPairs[textInPairs.Count - 1].Length; // number of empty space in the row

            if (textInWords[index].Length > leftSymbolsAmountInRow)
            { // if word size is bigger than empty space in the row
                if (leftSymbolsAmountInRow >= textInWords[index].Length / 2)
                {
                    textInPairs[textInPairs.Count - 1] += textInWords[index].Substring(0, leftSymbolsAmountInRow);
                    textInWords[index] = textInWords[index].Remove(0, leftSymbolsAmountInRow);
                }

                AddWordToPartsListWhileReducingWordLength(textInPairs, textInWords[index], symbolsCountInRow);
                textInPairs[textInPairs.Count - 1] += IfNotLastAddWhiteSpace(index, textInWords.Count - 1);
            }
            else // if word half size is lower than empty space in the row
            {
                // add word to last used line of text pairs list
                textInPairs[textInPairs.Count - 1] += textInWords[index];
                textInPairs[textInPairs.Count - 1] += IfNotLastAddWhiteSpace(index, textInWords.Count - 1);
            }

        }

        void AddWordToPartsListWhileReducingWordLength(List<string> textInPairs, string word, int symbolsCountInRow)
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

        string ReduceWordAndAddWordToList(List<string> textInPairs, string word, int length)
        {
            // Adds word to list and adds white space at the end of word
            textInPairs.Add(word.Substring(0, length));

            // Reduces word by given length
            return word.Remove(0, length);
        }

        string IfNotLastAddWhiteSpace(int index, int count)
        {
            if (index < count)
            {
                return " ";
            }

            return "";
        }
    }
}
