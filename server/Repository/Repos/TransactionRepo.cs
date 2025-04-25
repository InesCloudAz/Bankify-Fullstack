using Bankify.Interfaces;
using Bankify.Repository.Interfaces;
using Dapper;
using System.Data;
using System.Transactions;

namespace Bankify.Repository.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly IBankifyContext _context;

        public TransactionRepo(IBankifyContext context)
        {
            _context = context;
        }

       
        public void TransferFunds(int fromAccountId, int toAccountId, decimal amount)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@FromAccountId", fromAccountId, DbType.Int32);
                parameters.Add("@ToAccountId", toAccountId, DbType.Int32);
                parameters.Add("@Amount", amount, DbType.Decimal);

                try
                {
                    db.Execute("TransferFunds",
                               parameters,
                               commandType: CommandType.StoredProcedure);
                }
                catch 
                (Exception ex)
                {
                    throw new Exception("An error occurred while transferring funds.", ex);
                }
            }
        }

    }
}

