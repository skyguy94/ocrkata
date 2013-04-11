using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BankOCR.Tests
{

    [TestFixture]
    public class OCRConversionTests
    {
        [Test]
        public void ThrowsIfAccountStringIsNull()
        {
            const string testValue = null;

            var converter = new OCRConverter();
            Assert.Throws<ArgumentNullException>(() => converter.Convert(testValue));
        }

        [Test]
        public void ThrowsIfAccountStringIsTooShort()
        {
            const string testValue = "_";
            
            var converter = new OCRConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(testValue));
        }

        [Test]
        public void ThrowsIfAccountStringIsTooLong()
        {
            string testValue = Enumerable.Repeat("_", 117).ToString();

            var converter = new OCRConverter();
            Assert.Throws<ArgumentException>(() => converter.Convert(testValue));
        }

        [Test]
        public void AllZeroesTest()
        {
            string testValue =
            " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
            "| || || || || || || || || |" + Environment.NewLine +
            "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
            "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(000000000));
        }

        [Test]
        public void AllOnesTest()
        {
            string testValue =
            "                           " + Environment.NewLine +
            "  |  |  |  |  |  |  |  |  |" + Environment.NewLine +
            "  |  |  |  |  |  |  |  |  |" + Environment.NewLine +
            "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(111111111));
        }
        [Test]
        public void AllTwosTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                " _| _| _| _| _| _| _| _| _|" + Environment.NewLine +
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ " + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(222222222));
        }
        [Test]
        public void AllThreesTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                " _| _| _| _| _| _| _| _| _|" + Environment.NewLine +
                " _| _| _| _| _| _| _| _| _|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(333333333));
        }
        [Test]
        public void AllFoursTest()
        {
            string testValue =
                "                           " + Environment.NewLine +
                "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
                "  |  |  |  |  |  |  |  |  |" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(444444444));
        }
        [Test]
        public void AllFivesTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ " + Environment.NewLine +
                " _| _| _| _| _| _| _| _| _|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(555555555));
        }
        [Test]
        public void AllSixesTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                "|_ |_ |_ |_ |_ |_ |_ |_ |_ " + Environment.NewLine +
                "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(666666666));
        }
        [Test]
        public void AllSevensTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                "  |  |  |  |  |  |  |  |  |" + Environment.NewLine +
                "  |  |  |  |  |  |  |  |  |" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(777777777));
        }

        [Test]
        public void AllEightsTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
                "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(888888888));
        }

        [Test]
        public void AllNinesTest()
        {
            string testValue =
                " _  _  _  _  _  _  _  _  _ " + Environment.NewLine +
                "|_||_||_||_||_||_||_||_||_|" + Environment.NewLine +
                " _| _| _| _| _| _| _| _| _|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(999999999));
        }

        [Test]
        public void NumberOrderTest()
        {
            string testValue =
                "    _  _     _  _  _  _  _ " + Environment.NewLine +
                "  | _| _||_||_ |_   ||_||_|" + Environment.NewLine +
                "  ||_  _|  | _||_|  ||_| _|" + Environment.NewLine +
                "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result.ToValue(), Is.EqualTo(123456789));
        }

        [Test]
        public void MarksBadFiveThree()
        {
            string testValue =
                    "    _  _  _  _  _  _     _ " + Environment.NewLine +
                    "|_||_|| || ||_   |  |  | _ " + Environment.NewLine +
                    "  | _||_||_||_|  |  |  | _|" + Environment.NewLine +
                    "                           " + Environment.NewLine;

            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result[0], Is.EqualTo(OCRConverter.BadValue));
            Assert.That(result.Count(c => c == OCRConverter.BadValue), Is.EqualTo(1));
        }

        [Test]
        public void MarksBadFiveThreeAndNine()
        {
            string testValue =
                    "    _  _     _  _  _  _  _ " + Environment.NewLine +
                    "  | _| _||_| _ |_   ||_||_|" + Environment.NewLine +
                    "  ||_  _|  | _||_|  ||_| _ " + Environment.NewLine +
                    "                           " + Environment.NewLine;
            var converter = new OCRConverter();
            var result = converter.Convert(testValue);
            Assert.That(result[0], Is.EqualTo(OCRConverter.BadValue));
            Assert.That(result[4], Is.EqualTo(OCRConverter.BadValue));
            Assert.That(result.Count(c => c == OCRConverter.BadValue), Is.EqualTo(2));
        }

        [Test]
        public void CanFixBadFiveThree()
        {
            string testValue =
                    "    _  _  _  _  _  _     _ " + Environment.NewLine +
                    "|_||_|| || ||_   |  |  | _ " + Environment.NewLine +
                    "  | _||_||_||_|  |  |  | _|" + Environment.NewLine +
                    "                           " + Environment.NewLine;
            var converter = new OCRConverter();
            List<int[]> possibilities = converter.TryFixIllegibleNumber(testValue);

            Assert.That(possibilities.Count, Is.EqualTo(2));
            Assert.That(possibilities[0].ToValue(), Is.EqualTo(490067715));
            Assert.That(possibilities[1].ToValue(), Is.EqualTo(490067713));
        }

        [Test]
        public void CanFixBadFiveThreeAndNine()
        {
            string testValue =
                    "    _  _     _  _  _  _  _ " + Environment.NewLine +
                    "  | _| _||_| _ |_   ||_||_|" + Environment.NewLine +
                    "  ||_  _|  | _||_|  ||_| _ " + Environment.NewLine +
                    "                           " + Environment.NewLine;
            var converter = new OCRConverter();
            List<int[]> possibilities = converter.TryFixIllegibleNumber(testValue);

            Assert.That(possibilities.Count, Is.EqualTo(2));
            Assert.That(possibilities[0].ToValue(), Is.EqualTo(123456789));
            Assert.That(possibilities[1].ToValue(), Is.EqualTo(123436789));
        }

        [Test]
        public void DetectsLegibleNumber()
        {
            var converter = new OCRConverter();

            var result = converter.IsNumberLegible(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            Assert.That(result, Is.True);
        }

        [Test]
        public void DetectsIllegibleNumber()
        {
            var converter = new OCRConverter();

            var result = converter.IsNumberLegible(new[] { 1, 2, 3, 4, 5, 6, 7, 8, OCRConverter.BadValue });

            Assert.That(result, Is.False);
        }

        [Test]
        public void CanFixReallyBadCase()
        {
            string testValue =
                    "    _  _     _  _  _  _  _ " + Environment.NewLine +
                    "  | _| _||_| _ |_   ||_||_|" + Environment.NewLine +
                    "  | _  _     _| _|  || | _ " + Environment.NewLine +
                    "                           " + Environment.NewLine;
            var converter = new OCRConverter();
            List<int[]> possibilities = converter.TryFixIllegibleNumber(testValue);

            possibilities.ForEach(s => Console.WriteLine(s.ToValue()));
            Assert.That(possibilities.Count, Is.EqualTo(8));
            Assert.That(possibilities[0].ToValue(), Is.EqualTo(122455789));
            Assert.That(possibilities[1].ToValue(), Is.EqualTo(132455789));
            Assert.That(possibilities[2].ToValue(), Is.EqualTo(123455789));
            Assert.That(possibilities[3].ToValue(), Is.EqualTo(133455789));
            Assert.That(possibilities[4].ToValue(), Is.EqualTo(122435789));
            Assert.That(possibilities[5].ToValue(), Is.EqualTo(132435789));
            Assert.That(possibilities[6].ToValue(), Is.EqualTo(123435789));
            Assert.That(possibilities[7].ToValue(), Is.EqualTo(133435789));
        }
    }

    public static class ArrayExtension
    {
        public static int ToValue(this int[] array)
        {
            var result = array[0];
            for (int i = 1; i <= array.Length - 1; i++)
            {
                result += array[i]*(int)Math.Pow(10,i);
            }
            return result;
        }
    }
}

