using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            
            string path = @"C:\Work\Training\SupportBank\Transactions2013.json";
            logger.Debug($"Attempting to open file: {path}");
            ReadFile myFile = new ReadFile(path);
            Bank bank = new Bank();
            foreach (var line in myFile.lines)
            {
                try
                {
                    Transaction temp = new Transaction();
                    temp.TransactionFromCsv(line);
                    bank.ProcessTransaction(temp);
                }
                catch
                {
                    Console.WriteLine("Found at-least one incompatible transaction.");
                }
               
            }

            if (args[0].ToLower() == "list")
            {
                if (args[1].ToLower() == "all")
                {
                    bank.ListAll();
                }
                else
                {
                    if (bank.AccountExists(args[1]))
                    {
                        bank.List(args[1]);
                    }
                    else
                    {
                        logger.Error("Attempt to access account that does not exist.");
                        Console.WriteLine("Account doesn't exist!");
                    }
                }
            }
        }
    }
}