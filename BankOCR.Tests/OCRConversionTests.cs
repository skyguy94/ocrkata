using System;
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
        public void MarksBadFive()
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
        public void MarksBadFiveAndNine()
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

/*

use case 4
                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           
=&gt; 711111111
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |
                           
=&gt; 777777177
 _  _  _  _  _  _  _  _  _ 
 _|| || || || || || || || |
|_ |_||_||_||_||_||_||_||_|
                           
=&gt; 200800000
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|
                           
=&gt; 333393333 
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|
                           
=&gt; 888888888 AMB ['888886888', '888888880', '888888988']
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|
                           
=&gt; 555555555 AMB ['555655555', '559555555']
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|
                           
=&gt; 666666666 AMB ['666566666', '686666666']
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|
                           
=&gt; 999999999 AMB ['899999999', '993999999', '999959999']
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  ||_ 
  | _||_||_||_|  |  |  | _|
                           
=&gt; 490067715 AMB ['490067115', '490067719', '490867715']
    _  _     _  _  _  _  _ 
 _| _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _| 
                           
=&gt; 123456789
 _     _  _  _  _  _  _    
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |
                           
=&gt; 000000051
    _  _  _  _  _  _     _ 
|_||_|| ||_||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|
                           
=&gt; 490867715  */
