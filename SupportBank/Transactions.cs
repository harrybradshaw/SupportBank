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
        public List<Transaction> All = new List<Transaction>();
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

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
                Logger.Error("File not found");
                throw;
            }
        }

        public void FromJson(string fname)
        {
            string s = File.ReadAllText(fname);
            All = JsonConvert.DeserializeObject<List<Transaction>>(s);
        }

        public void FromXml(string path)
        {
            TransactionListXml transList = new TransactionListXml();
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TransactionListXml));
                transList = (TransactionListXml) serializer.Deserialize(reader);
            }

            foreach (var item in transList.AllTransactionXml)
            {
                Transaction temp = new Transaction();
                temp.GenFromTransactionXml(item);
                All.Add(temp);
            }
        }

        public void GenerateTransactions(string fname)
        {
            if (Path.GetExtension(fname) == ".json")
            { 
                FromJson(fname);
            } else if (Path.GetExtension(fname) == ".csv")
            {
                FromCsv(fname);
            } else if (Path.GetExtension(fname) == ".xml")
            {
                FromXml(fname);
            }
            
        }

        public void GenerateJsonFile(string outname)
        {
            string jsonString = JsonConvert.SerializeObject(All);
            using (StreamWriter sw = new StreamWriter(outname))
            {
                sw.Write(jsonString);
            }
        }

        public void GenerateXmlFile(string outname)
        {
            TransactionListXml transactionListXml = new TransactionListXml();
            transactionListXml.GenerateFromTransactions(All);

            XmlSerializer serializer = new XmlSerializer(typeof(TransactionListXml));
            using (StreamWriter sw = new StreamWriter(outname))
            {
                serializer.Serialize(sw,transactionListXml);
            }
            

        }

        public void GenerateFile(string outname)
        {
            if (Path.GetExtension(outname) == ".json")
            {
                GenerateJsonFile(outname);
            } else if (Path.GetExtension(outname) == ".xml")
            {
                GenerateXmlFile(outname);
            }
        }
    }
}