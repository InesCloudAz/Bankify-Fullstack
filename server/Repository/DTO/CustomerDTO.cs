namespace Bankify.Repository.DTO
{
    public class CustomerDTO
    {

        public string Givenname { get; set; }
        public string Firstname { get; set; }
        public string Gender { get; set; }
        public string Streetaddress { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? Birthday { get; set; }
        public string TelephoneCountryCode { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
