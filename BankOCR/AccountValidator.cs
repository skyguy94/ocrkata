using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOCR
{
    public class AccountValidator
    {
        public int CalculateChecksum(int[] number)
        {
            if (number.Length != 9) throw new ArgumentException("Account number has an invalid length");

            var checksum = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                if (number[i] == OCRConverter.BadValue) continue;

                var multiplier = number.Length - i;
                checksum += number[i]*multiplier;
            }
            return checksum % 11;
        }

        public bool ValidateNumber(int[] number)
        {
            var result = CalculateChecksum(number);
            return result == 0;
        }

        private readonly int[][] _matchTable =
            {
                new[] {0, 8},
                new[] {1, 7},
                new[] {2},
                new[] {3,9},
                new[] {4},
                new[] {5, 6, 9},
                new[] {5, 6, 8},
                new[] {1, 7},
                new[] {0, 6, 8, 9},
                new[] { 3, 5, 8}
            };

        private Dictionary<int, int[]> FindPossibleFixes(int[] number)
        {
            var fixes = new Dictionary<int, int[]>();
            for (int i = 0; i < number.Length; i++)
            {
                var digit = number[i];
                fixes.Add(i, _matchTable[digit]);
            }
            return fixes;
        }

        public IList<int[]> TryFixInvalidNumber(int[] number)
        {
            if (number.Length != 9) throw new ArgumentException("Account number has an invalid length");

            var fixes = FindPossibleFixes(number);
            var possibilities = CreatePossibilities(fixes, number);
            var valid = possibilities.Where(ValidateNumber).ToList();

            return valid;
        }

        private class IndexPair
        {
            public int Key { get; set; }
            public int Index { get; set; }
        }

        private static IEnumerable<int[]> CreatePossibilities(Dictionary<int, int[]> fixes, int[] number)
        {
            var possibilities = new List<int[]>();
            var indicies = fixes.Keys.Select(key => new IndexPair { Key = key, Index = 0 }).ToList();

            //There's probably something better in Knuth #3 for this
            while (true)
            {
                var possibility = new int[9];
                number.CopyTo(possibility, 0);
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
                if (indicies[indexToUpdate].Index == fixes[indicies[indexToUpdate].Key].Length)
                {
                    indicies[indexToUpdate].Index = 0;
                    while (--indexToUpdate >= 0)
                    {
                        indicies[indexToUpdate].Index++;
                        if (indicies[indexToUpdate].Index != fixes[indicies[indexToUpdate].Key].Length) break;
                        indicies[indexToUpdate].Index = 0;
                    }

                    if (indexToUpdate < 0) break;
                }
            }

            return possibilities;
        }
    }
}
