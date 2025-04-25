namespace Bankify.Repository.DTO
{
    public class AccountResponseDTO
    {
        public string? TypeName { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }
        public string? Frequency { get; set; }
        public string? AccountNumber { get; set; }
    }
}
