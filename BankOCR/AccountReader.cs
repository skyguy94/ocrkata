using System.IO;

namespace BankOCR
{
    public class AccountReader
    {
        private const int accountLength = 116;
        public void ParseAccounts(FileInfo source, FileInfo destination)
        {
            var converter = new OCRConverter();
            var validator = new AccountValidator();

            var buffer = new char[accountLength];
            using (var reader = new StreamReader(File.Open(source.FullName, FileMode.Open)))
            {
                using (var writer = new StreamWriter(File.Open(Path.ChangeExtension(source.FullName, "processed.txt"), FileMode.Truncate)))
                {
                    reader.ReadBlock(buffer, 0, accountLength);
                    var number = converter.Convert(buffer.ToString());

                    var value = converter.CreateStringValue(number, '?');
                    writer.Write(number);

                    bool isValid = converter.IsNumberLegible(number);
                    if (!isValid)
                    {
                        var possibilites = converter.TryFixIllegibleNumber(buffer.ToString());
                        //Finish me.
                    }
                }
            }
        }
    }
}
