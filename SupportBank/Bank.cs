using System;
using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class Bank
    {
        private List<Account> allAccounts = new List<Account>();
        private List<Transaction> allTransactions = new List<Transaction>();
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private void AddAccount(string name)
        {
            if (!AccountExists(name))
            {
                allAccounts.Add(new Account(name));
            }
            
        }
        
        public void ProcessTransaction(Transaction t)
        {
            UpdateAccount(t.ToAccount,t.Amount);
            UpdateAccount(t.FromAccount,(-1)*t.Amount);
            UpdateTransactions(t);
            allTransactions.Add(t);
        }

        private void UpdateAccount(string accountName, float balance)
        {
            AddAccount(accountName);
            foreach (var acc in allAccounts)
            {
                if (acc.Name == accountName)
                {
                    acc.UpdateBalance(balance);
                    break;
                }
            }
        }
        private void UpdateTransactions(Transaction t)
        {
            foreach (var acc in allAccounts)
            {
                if (acc.Name == t.FromAccount || acc.Name == t.ToAccount)
                {
                    acc.AddTransaction(t);
                }
            }
        }
        
        public bool AccountExists(string name)
        {
            foreach (var account in allAccounts)
            {
                if (account.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public void ListAll()
        {
            foreach (var acc in allAccounts)
            {
                Console.WriteLine($"({acc.Id}) {acc.Name}: £{acc.Balance}");
            }
        }

        public void List(string accountName)
        {
            foreach (var acc in allAccounts)
            {
                if (acc.Name == accountName)
                {
                    acc.PrintTransactions();
                }
            }
        }
    }
}