using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SupportBank
{
    class ReadFile
    {
        public List<string> lines = new List<string>();
        public ReadFile(string fname)
        {
            foreach (string line in File.ReadLines(fname).Skip(1))
            {
                lines.Add(line);
            }
        } 
    }

    class Account
    {
        public string Name;
        public float Balance;
        public int Id;
        static int id = 0;
        public List<Transaction> AssociatedTransactions = new List<Transaction>();
        public Account(string name)
        {
            Name = name;
            Balance = 0;
            Id = id;
            id++;
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
                Console.WriteLine(t.Date + " " + t.Message + " " + tType + " £" + t.Amount);
            }
            Console.WriteLine("Closing Balance: £" + Balance);
        }
        
    }

    class Bank
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
                Console.WriteLine("("+ acc.Id + ") " + acc.Name + ": £" + acc.Balance);
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
    class Transaction
    {
        public string Date;
        public string To;
        public string From;
        public float Amount;
        public string Message;
        public Transaction(string line)
        {
            string[] items = line.Split(",");
            Date = items[0];
            From = items[1];
            To = items[2];
            Message = items[3];
            Amount = float.Parse(items[4]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Work\Training\SupportBank\Transactions2014.csv";
            ReadFile myFile = new ReadFile(path);
            Bank bank = new Bank();
            foreach (var line in myFile.lines)
            {
                Transaction temp = new Transaction(line);
                bank.ProcessTransaction(temp);
            }

            if (args[0].ToLower() == "list")
            {
                if (args[1].ToLower() == "all")
                {
                    bank.ListAll();
                }
                else
                {
                    if (bank.CheckAccountExists(args[1]))
                    {
                        bank.List(args[1]);
                    }
                    else
                    {
                        Console.WriteLine("Account doesn't exist!");
                    }
                }
            }
            
            //bank.List("Tim L");
            
        }
    }
}