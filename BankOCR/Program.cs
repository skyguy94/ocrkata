using System.IO;

namespace BankOCR
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new AccountReader();
            reader.ParseAccounts(new FileInfo(args[0]), new FileInfo(args[1]));
        }
    }
}
