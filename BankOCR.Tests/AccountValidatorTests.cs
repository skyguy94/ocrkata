using System.Linq;
using NUnit.Framework;

namespace BankOCR.Tests
{
    public class AccountValidatorTests
    {
        [Test]
        public void CalculatesProperChecksum()
        {
            var validator = new AccountValidator();
            
            var invalidResult = validator.CalculateChecksum(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1});
            var validResult = validator.CalculateChecksum(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 0 });
            var complicatedButValidResult = validator.CalculateChecksum(new[] {9, 8, 7, 6, 5, 4, 0, 2, 0});
           
            Assert.That(invalidResult, Is.EqualTo(1));
            Assert.That(validResult, Is.EqualTo(0));
            Assert.That(complicatedButValidResult, Is.EqualTo(0));
        }

        [Test]
        public void ValidatesChecksums()
        {
            var validator = new AccountValidator();

            var invalidResult = validator.ValidateNumber(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            var validResult = validator.ValidateNumber(new[] { 1, 1, 1, 1, 1, 1, 1, 1, 0 });
            var complicatedButValidResult = validator.ValidateNumber(new[] { 9, 8, 7, 6, 5, 4, 0, 2, 0 });

            Assert.That(invalidResult, Is.False);
            Assert.That(validResult, Is.True);
            Assert.That(complicatedButValidResult, Is.True);
        }

        [Test]
        public void FixesInvalidOnesNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(1, 9).ToArray());
           Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidSevensNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(7, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalid2kNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(new[] { 2, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidThreesNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(3, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidEightsNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(8, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidFivesNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(5, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidSixesNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(6, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidNinesNumber()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(Enumerable.Repeat(9, 9).ToArray());
            Assert.That(valid.Count, Is.EqualTo(47));
        }

        [Test]
        public void FixesInvalidNumber1()
        {
            var validator = new AccountValidator();

            var valid = validator.TryFixInvalidNumber(new[] { 4, 9, 0, 0, 6, 7, 7, 1, 5 });
            Assert.That(valid.Count, Is.EqualTo(47));
        }
    }
}
