using System.Text.Json.Serialization;

namespace Bankify.Repository.Entities
{
    public class Account
    {
        [JsonIgnore]
        public int AccountID { get; set; }
        public string? Frequency { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; }
        public decimal Balance { get; set; }

        [JsonIgnore]
        public int? AccountTypesID { get; set; }

        [JsonIgnore]
        public int CustomerID { get; set; }
        public string? TypeName { get; set; }





    }
}
