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
        
    }

    public class TransProps
    {
        public string To;
        public string From;
    }
    
}
    