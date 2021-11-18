namespace SupportBank
{
    public class Transaction
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
}