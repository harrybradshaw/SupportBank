using System;
using NLog;

namespace SupportBank
{
    public class Transaction
    {
        public DateTime Date;
        public string To;
        public string From;
        public float Amount;
        public string Message;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public void TransactionFromCsv(string line)
        {
            Logger.Debug($"Attempting to create transation from line: {line}");
            
            string[] items = line.Split(",");
            try
            {
                try
                {
                    Date = DateTime.Parse(items[0]);
                }
                catch (FormatException)
                {
                    Logger.Error("Couldn't parse DateTime.");
                }
            
                From = items[1];
                To = items[2];
                Message = items[3];
                try
                {
                    Amount = float.Parse(items[4]); 
                }
                catch (FormatException)
                {
                    Logger.Error("Could not parse transaction value!");
                    throw;
                }
                
            }
            
            catch (IndexOutOfRangeException)
            {
                Logger.Error("Not enough items in Transaction.");
                Logger.Error(line);
                throw;
            }
        }
    }
}