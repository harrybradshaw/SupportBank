using System;
using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class Account
    {
        public string Name;
        public float Balance;
        public int Id;
        private static int _id = 0;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public List<Transaction> AssociatedTransactions = new List<Transaction>();
        public Account(string name)
        {
            Name = name;
            Balance = 0;
            Id = _id;
            _id++;
        }

        public void UpdateBalance(float value)
        {
            Balance += value;
        }

        public void AddTransaction(Transaction t)
        {
            AssociatedTransactions.Add(t);
        }

        public void PrintTransactions()
        {
            foreach (var t in AssociatedTransactions)
            {
                string tType;
                if (t.To == Name)
                {
                    tType = "Credit";
                }
                else
                {
                    tType = "Debit";
                }
                Console.WriteLine($"{t.Date}: {t.Message} ({tType} £{t.Amount})");
            }
            Console.WriteLine($"Closing Balance: £{Balance}" );
        }
    }
}