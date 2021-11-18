using System;
using System.Collections.Generic;

namespace SupportBank
{
    public class Account
    {
        public string Name;
        public float Balance;
        public int Id;
        private static int _id;

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
                if (t.ToAccount == Name)
                {
                    tType = "Credit";
                }
                else
                {
                    tType = "Debit";
                }
                Console.WriteLine($"{t.Date}: {t.Narrative} ({tType} £{t.Amount})");
            }
            Console.WriteLine($"Closing Balance: £{Balance}" );
        }
    }
}