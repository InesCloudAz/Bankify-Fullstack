namespace Bankify.Repository.Interfaces
{
    public interface ITransactionRepo
    {

        void TransferFunds(int fromAccountId, int toAccountId, decimal amount);
    }
}
