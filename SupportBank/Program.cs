using System;
using System.Collections.Generic;

namespace SupportBank
{
    class ReadFile
    {
        public List<string> lines = new List<string>();
        public ReadFile(string fname)
        {
            string[] allLines = System.IO.File.ReadAllLines(fname);
            foreach (var line in allLines)
            {
                lines.Add(line);
            }
        } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Work\Training\SupportBank\Transactions2014.csv";
            ReadFile myFile = new ReadFile(path);
            foreach (var line in myFile.lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}