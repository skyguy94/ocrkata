using System;
using System.Collections.Generic;
using System.Linq;

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
                default: return 19;
            }
        }

        private static int[] ConvertPrimeValueToAccountNumber(IList<int> values)
        {
            var result = new int[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                result[i] = ConvertPrimeValueToDigit(values[i]);
            }

            return result;
        }

        public int[] Convert(string testValue)
        {
            var primeResult = ConvertWithPrimes(testValue);
            var finalResult = ConvertPrimeValueToAccountNumber(primeResult);
            return finalResult;
        }
    }
}
