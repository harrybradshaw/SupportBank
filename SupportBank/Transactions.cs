using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NLog;

namespace SupportBank
{
    public class Transactions
    {
        [XmlElement("SupportTransaction")]
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

        public void FromXml(string fname)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Transactions));
            TextReader reader = new StreamReader(fname);
            object obj = deserializer.Deserialize(reader);
        }

        public void GenerateTransactions(string fname)
        {
            if (Path.GetExtension(fname) == ".json")
            { 
                FromJson(fname);
            } else if (Path.GetExtension(fname) == ".csv")
            {
                FromCsv(fname);
            }
            
        }
    }
}