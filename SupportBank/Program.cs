using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            
            string path = @"C:\Work\Training\SupportBank\Transactions2014.xml";

            Bank bank = new Bank();
            bank.InitialiseFromFile(path);
            
            if (args[0].ToLower() == "list")
            {
                {
                    bank.List(args[1]);
                }
            }
            
            bank.ExportFile(@"C:\Work\Training\SupportBank\Transactions2014.xml");
        }
    }
}