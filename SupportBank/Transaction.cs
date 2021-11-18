using System;
using NLog;

namespace SupportBank
{
    public class Transaction
    {
        public DateTime Date;
        public string ToAccount;
        public string FromAccount;
        public float Amount;
        public string Narrative;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void GenFromTransactionXml(TransactionXML trxml)
        {
            Narrative = trxml.Description;
            Amount = trxml.Value;
            ToAccount = trxml.Parties.To;
            FromAccount = trxml.Parties.From;
            Date = DateTime.Parse("01/01/2000");
        }
        public void GenFromLine(string line)
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
            
                FromAccount = items[1];
                ToAccount = items[2];
                Narrative = items[3];
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