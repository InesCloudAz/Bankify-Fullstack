namespace Bankify.Repository.DTO
{
    public class NewCustomerResult
    {
        public int CustomerId { get; set; }
        //public int NewUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountNumber { get; set; }
    }
}
