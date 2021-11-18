using System;
using System.Collections.Generic;

namespace SupportBank
{
    public class Bank
    {
        private List<Account> allAccounts = new List<Account>();
        private List<Transaction> allTransactions = new List<Transaction>();

        private void AddAccount(string name)
        {
            if (!CheckAccountExists(name))
            {
                allAccounts.Add(new Account(name));
            }
            
        }
        
        public void ProcessTransaction(Transaction t)
        {
            UpdateAccount(t.To,t.Amount);
            UpdateAccount(t.From,(-1)*t.Amount);
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
                if (acc.Name == t.From || acc.Name == t.To)
                {
                    acc.AddTransaction(t);
                }
            }
        }
        
        public bool CheckAccountExists(string name)
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