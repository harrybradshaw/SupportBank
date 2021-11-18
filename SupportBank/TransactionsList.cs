using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupportBank
{
    public class TransactionXML
    {
        //Attribute
        [XmlAttribute]
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
        public List<TransactionXML> AllTransactionXml = new List<TransactionXML>();
        
    }

    public class TransProps
    {
        public string To;
        public string From;
    }
    
}
    