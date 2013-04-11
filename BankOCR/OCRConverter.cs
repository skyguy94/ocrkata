using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankOCR
{
    public class OCRConverter
    {
        private readonly int[] _weights =
            {
                1, 2, 1,
                3, 5, 7,
                11, 13, 17
            };

        private const int PrimeOne = 7 * 17;
        private const int PrimeTwo = 2 * 5 * 7 * 11 * 13;
        private const int PrimeThree = 2 * 5 * 7 * 13 * 17;
        private const int PrimeFour = 3 * 5 * 7 * 17;
        private const int PrimeFive = 2 * 3 * 5 * 13 * 17;
        private const int PrimeSix = 2 * 3 * 5 * 11 * 13 * 17;
        private const int PrimeSeven = 2 * 7 * 17;
        private const int PrimeEight = 2 * 3 * 5 * 7 * 11 * 13 * 17;
        private const int PrimeNine = 2 * 3 * 5 * 7 * 13 * 17;
        private const int PrimeZero = 2 * 3 * 7 * 11 * 13 * 17;

        public const int BadValue = 19;
        private int[] ConvertWithPrimes(string testValue)
        {
            var charValues = new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            const int blockHeight = 4;
            const int blockLength = 27;
            const int characterWidth = 3;

            var lineLength = blockLength * blockHeight + blockHeight * Environment.NewLine.Length;
            if (testValue == null) throw new ArgumentNullException();
            if (testValue.Length != lineLength) throw new ArgumentException(@"The supplied string is improperly formatted.");

            int rowPos = 0;
            int colPos = 0;
            while (true)
            {
                int currentChar = colPos / characterWidth;
                int subCharPos = colPos % characterWidth + rowPos * characterWidth;
                int stringPos = colPos + rowPos * blockLength + Environment.NewLine.Length * rowPos;

                var characterWieght = testValue[stringPos] != ' ' ? _weights[subCharPos] : 1;
                charValues[currentChar] *= characterWieght;

                colPos++;
                if (colPos == blockLength)
                {
                    rowPos++;
                    colPos = 0;
                }

                if (rowPos == blockHeight - 1) break;
            }

            return charValues.Reverse().ToArray();
        }

        private static int ConvertPrimeValueToDigit(int value)
        {
            switch (value)
            {
                case PrimeOne: return 1;
                case PrimeTwo: return 2;
                case PrimeThree: return 3;
                case PrimeFour: return 4;
                case PrimeFive: return 5;
                case PrimeSix: return 6;
                case PrimeSeven: return 7;
                case PrimeEight: return 8;
                case PrimeNine: return 9;
                case PrimeZero: return 0;
                default: return value;
            }
        }

        private static int[] ConvertPrimeValueToAccountNumber(IList<int> values)
        {
            var result = new int[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                var converted = ConvertPrimeValueToDigit(values[i]);
                result[i] = converted != values[i] ? converted : BadValue;
            }

            return result;
        }

        public int[] Convert(string testValue)
        {
            var primeResult = ConvertWithPrimes(testValue);
            var finalResult = ConvertPrimeValueToAccountNumber(primeResult);
            return finalResult;
        }

        public bool IsNumberLegible(int[] number)
        {
            if (number.Length != 9) throw new ArgumentException("Account number has an invalid length");

            var invalidCharCount = number.Count(c => c == BadValue);
            return invalidCharCount == 0;
        }

        public string CreateStringValue(int[] number, char illegibleChar)
        {
            if (number.Length != 9) throw new ArgumentException("Account number has an invalid length");

            var result = new StringBuilder(); 
            for (int i = 0; i < number.Length; i++)
            {
                result.Append(number[i] != BadValue ? number[i] : illegibleChar);
            }

            return result.ToString();
        }

        private List<int> FindPossibleBadNumberFixes(int number)
        {
            var possibilites = new List<int>();
            for (int i = 0; i < _weights.Length; i++)
            {
                if (_weights[i] == 1) continue;
                var trial = number*_weights[i];
                var converted = ConvertPrimeValueToDigit(trial);
                if (converted != trial)
                {
                    possibilites.Add(converted);
                }

                //Divide out for missing pipes?
            }

            return possibilites;
        }

        private class IndexPair
        {
            public int Key { get; set; }
            public int Index { get; set; }
        }

        public List<int[]> TryFixIllegibleNumber(string testValue)
        {
            var primeResult = ConvertWithPrimes(testValue);
            var converted = ConvertPrimeValueToAccountNumber(primeResult);

            var fixes = FindPossibleFixes(primeResult, converted);
            var possibilities = CreatePossibilities(fixes, converted);

            return possibilities;
        }

        private static List<int[]> CreatePossibilities( Dictionary<int, List<int>> fixes, int[] converted)
        {
            var possibilities = new List<int[]>();
            var indicies = fixes.Keys.Select(key => new IndexPair { Key = key, Index = 0 }).ToList();

            //There's probably something better in Knuth #3 for this
            while (true)
            {
                var possibility = new int[9];
                converted.CopyTo(possibility, 0);
                //Replace the bad digit with the lookup using the current indicies;
                for (int badPos = 0; badPos < fixes.Keys.Count; badPos++)
                {
                    var keyValue = indicies[badPos].Key;
                    var indexValue = indicies[badPos].Index;
                    var value = fixes[keyValue][indexValue];
                    possibility[keyValue] = value;
                }

                possibilities.Add(possibility);

                var indexToUpdate = indicies.Count - 1;
                indicies[indexToUpdate].Index++;
                if (indicies[indexToUpdate].Index == fixes[indicies[indexToUpdate].Key].Count)
                {
                    indicies[indexToUpdate].Index = 0;
                    while (--indexToUpdate >= 0)
                    {
                        indicies[indexToUpdate].Index++;
                        if (indicies[indexToUpdate].Index != fixes[indicies[indexToUpdate].Key].Count) break;
                        indicies[indexToUpdate].Index = 0;
                    }

                    if (indexToUpdate < 0) break;
                }
            }

            return possibilities;
        }

        private Dictionary<int, List<int>> FindPossibleFixes(IList<int> primeResult, IList<int> converted)
        {
            var fixes = new Dictionary<int, List<int>>();
            for (int i = 0; i < primeResult.Count; i++)
            {
                if (converted[i] == BadValue)
                {
                    var matches = FindPossibleBadNumberFixes(primeResult[i]);
                    fixes[i] = matches;
                }
            }
            return fixes;
        }
    }
}
