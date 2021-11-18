using System;
using NLog;

namespace SupportBank
{
    public class Transaction
    {
        public string Date;
        public string To;
        public string From;
        public float Amount;
        public string Message;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void TransactionFromCSV(string line)
        {
            logger.Debug($"Attempting to create transation from line: {line}");
            try
            {
                string[] items = line.Split(",");
                Date = items[0];
                From = items[1];
                To = items[2];
                Message = items[3];
                Amount = float.Parse(items[4]);
            }
            catch (IndexOutOfRangeException e)
            {
                logger.Error("Not enough items in Transaction.");
                logger.Error(line);
                throw;
            }
            catch (FormatException ex)
            {
                logger.Error("Could not parse transaction value!");
            }
        }
    }
}