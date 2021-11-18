using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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

            Bank bank = new Bank();
            Transactions transactions = new Transactions();
            transactions.GenerateTransactions(path);
            bank.ProcessAll(transactions);
            
            if (args[0].ToLower() == "list")
            {
                {
                    bank.List(args[1]);
                }
            }
        }
    }
}