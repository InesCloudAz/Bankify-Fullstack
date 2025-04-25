namespace Bankify.Repository.Entities
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int AccountID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string? Status { get; set; }
        public string? TypeName { get; set; }






    }
}
