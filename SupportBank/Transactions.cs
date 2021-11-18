using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using NLog;

namespace SupportBank
{
    public class Transactions
    {
        public List<Transaction> All = new List<Transaction>();
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void FromCsv(string fname)
        {
            try
            {
                foreach (string line in File.ReadLines(fname).Skip(1))
                {
                    Transaction temp = new Transaction();
                    temp.GenFromLine(line);
                    All.Add(temp);
                }
            }
            catch (FileNotFoundException)
            {
                logger.Error("File not found");
                throw;
            }
        }

        public void FromJson(string fname)
        {
            string s = File.ReadAllText(fname);
            All = JsonConvert.DeserializeObject<List<Transaction>>(s);
        }
    }
}