using Bankify.Repository.DTO;
using Bankify.Repository.Entities;

namespace Bankify.Repository.Interfaces
{
    public interface IUserRepo
    {
        NewCustomerResult InsertNewCustomer(Customer customer, string accountTypeName); User GetUserByCredentials(string userName, string password);
        (int? UserID, int? CustomerID, string Role) LoginUser(string userName, string password);
        void InsertLoan(LoanDto loan);
    }
}
