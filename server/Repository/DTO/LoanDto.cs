namespace Bankify.Repository.DTO
{
    public class LoanDto
    {
        public int AccountID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal Payments { get; set; }
        public string? Status { get; set; }
        public string TypeName { get; set; } = "Standard transaction account";
    }
}
