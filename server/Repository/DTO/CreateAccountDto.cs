namespace Bankify.Repository.DTO
{
    public class CreateAccountDto
    {
        public string? TypeName { get; set; }
        public decimal InitialDeposit { get; set; }
        public string? Frequency { get; set; }
    }
}
