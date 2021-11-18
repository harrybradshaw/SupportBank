using System;
using System.Collections.Generic;
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

    class Person
    {
        public Person(string name, double balance)
        {
            
        }
    }
    class Transaction
    {
        public string date;
        public string to;
        public string from;
        public double ammount;
        public string message;
        public Transaction(string line)
        {
            string[] items = line.Split(",");
            date = items[0];
            from = items[1];
            to = items[2];
            message = items[3];
            ammount = Convert.ToDouble(items[4]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Work\Training\SupportBank\Transactions2014.csv";
            ReadFile myFile = new ReadFile(path);
            //List<Transaction> transactions = new List<Transaction>();
            foreach (var line in myFile.lines)
            {
                Transaction temp = new Transaction(line);
                Console.WriteLine(temp.date + ": " + temp.to);
            }
            
        }
    }
}