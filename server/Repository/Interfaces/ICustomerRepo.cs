using Bankify.Repository.DTO;
using Bankify.Repository.Entities;

namespace Bankify.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        (int? CustomerID, string Role) LoginCustomer(string userName, string password);
        List<AccountDTO> GetCustomerAccounts(int customerId);
        List<TransactionDto> GetAccountTransactions(string accountNumber, int customerId);
        int CreateAccount(int customerId, string typeName, decimal initialDeposit, string frequency);
        bool TransferBetweenAccounts(string fromAccountNumber, string toAccountNumber, decimal amount, int customerId);
        bool TransferToAnotherCustomer(string fromAccountNumber, string toAccountNumber, decimal amount, int customerId);
    }
}
