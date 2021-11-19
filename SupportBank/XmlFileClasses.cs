using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportBank
{
    public class TransactionXml
    {
        //Attribute
        [XmlAttribute("Date")]
        public string Date;
        //Elements
        public float Value;
        public string Description;
        public TransProps Parties;
    }

    [XmlRootAttribute("TransactionList")]
    public class TransactionListXml
    {
        [XmlElement("SupportTransaction")]
        public List<TransactionXml> AllTransactionXml = new List<TransactionXml>();

        public void GenerateFromTransactions(List<Transaction> transactions)
        {
            foreach (var t in transactions)
            {
                TransactionXml temp = new TransactionXml();
                TransProps tempTransProps = new TransProps();

                tempTransProps.From = t.FromAccount;
                tempTransProps.To = t.ToAccount;
                temp.Parties = tempTransProps;
                temp.Description = t.Narrative;
                temp.Date = t.Date.ToString("MM/dd/yyyy");
                temp.Value = t.Amount;
                
                AllTransactionXml.Add(temp);
            }
        }
        
    }

    public class TransProps
    {
        public string To;
        public string From;
    }
    
}
    