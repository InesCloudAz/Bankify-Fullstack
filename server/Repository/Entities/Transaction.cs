namespace Bankify.Repository.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public string? Type { get; set; }
        public string? Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
       










    }
}
