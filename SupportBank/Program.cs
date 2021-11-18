using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Work\Training\SupportBank\Transactions2014.csv";
            ReadFile myFile = new ReadFile(path);
            Bank bank = new Bank();
            foreach (var line in myFile.lines)
            {
                Transaction temp = new Transaction(line);
                bank.ProcessTransaction(temp);
            }

            if (args[0].ToLower() == "list")
            {
                if (args[1].ToLower() == "all")
                {
                    bank.ListAll();
                }
                else
                {
                    if (bank.CheckAccountExists(args[1]))
                    {
                        bank.List(args[1]);
                    }
                    else
                    {
                        Console.WriteLine("Account doesn't exist!");
                    }
                }
            }
            
            //bank.List("Tim L");
            
        }
    }
}