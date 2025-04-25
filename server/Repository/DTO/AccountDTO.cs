namespace Bankify.Repository.DTO
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public string? TypeName { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; } 
        public int AccountTypesID { get; set; } 
        public string? Frequency { get; set; }
        public string? AccountNumber { get; set; }

    }
}
