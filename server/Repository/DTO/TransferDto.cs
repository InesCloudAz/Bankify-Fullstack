namespace Bankify.Repository.DTO
{
    public class TransferDto
    {
        public string FromAccountNumber { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
