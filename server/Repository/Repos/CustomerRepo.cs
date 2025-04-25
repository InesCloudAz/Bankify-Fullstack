using Bankify.Interfaces;
using Bankify.Repository.DTO;
using Bankify.Repository.Entities;
using Bankify.Repository.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bankify.Repository.Repos
{
    public class CustomerRepo : ICustomerRepo
    {

        private readonly IBankifyContext _context;

        public CustomerRepo(IBankifyContext context)
        {
            _context = context;
        }

        public CustomerRepo()
        {
        }

        public (int? CustomerID, string Role) LoginCustomer(string userName, string password)
        {
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("LoginCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int? customerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : (int?)null;
                            string? role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null;

                            return (customerID, role);
                        }
                    }
                }
            }

            return (null, null);
        }

        // Skapa ett nytt bankkonto för kunden
        public int CreateAccount(int customerId, string typeName, decimal initialDeposit, string frequency)
        {
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("CreateAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@TypeName", typeName);
                    command.Parameters.AddWithValue("@InitialDeposit", initialDeposit);
                    command.Parameters.AddWithValue("@Frequency", frequency);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar()); // Returnerar det nya kontots ID
                }
            }
        }

        // Överföring mellan egna konton
        public bool TransferBetweenAccounts(string fromAccountNumber, string toAccountNumber, decimal amount, int customerId)
        {


            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("TransferBetweenAccounts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromAccountNumber", fromAccountNumber);
                    command.Parameters.AddWithValue("@ToAccountNumber", toAccountNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool TransferToAnotherCustomer(string fromAccountNumber, string toAccountNumber, decimal amount, int customerId)
        {

            //int fromAccountId = GetAccountIdByAccountNumber(fromAccountNumber, customerId);
            //int toAccountId = GetAccountIdByAccountNumber(toAccountNumber, customerId);

            //if (fromAccountId == -1 || toAccountId == -1)
            //{
            //    return false;
            //}
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("TransferToAnotherCustomer", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FromAccountNumber", SqlDbType.NVarChar, 20) { Value = fromAccountNumber });
                    command.Parameters.Add(new SqlParameter("@ToAccountNumber", SqlDbType.NVarChar, 20) { Value = toAccountNumber });
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<TransactionDto> GetAccountTransactions(string accountNumber, int customerId)
        {
            var transactions = new List<TransactionDto>();

            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("GetAccountTransactionsByNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50).Value = accountNumber;
                    command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactions.Add(new TransactionDto
                            {
                                TransactionId = Convert.ToInt32(reader["TransactionId"]),
                                AccountId = Convert.ToInt32(reader["AccountId"]),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Type = reader["Type"].ToString(),
                                Operation = reader["Operation"].ToString(),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                Balance = Convert.ToDecimal(reader["Balance"]),
                                Symbol = reader["Symbol"] != DBNull.Value ? reader["Symbol"].ToString() : null
                            });
                        }
                    }
                }
            }

            return transactions;
        }

        public List<AccountDTO> GetCustomerAccounts(int customerId)
        {
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("GetCustomerAccounts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerId", customerId);

                    connection.Open();
                    List<AccountDTO> accounts = new List<AccountDTO>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new AccountDTO
                            {
                                AccountID = Convert.ToInt32(reader["AccountID"]),
                                TypeName = reader["TypeName"].ToString(),
                                Balance = Convert.ToDecimal(reader["Balance"]),
                                Created = Convert.ToDateTime(reader["Created"]), 
                                AccountTypesID = Convert.ToInt32(reader["AccountTypesId"]),
                                Frequency = reader["Frequency"].ToString(),
                                AccountNumber = reader["AccountNumber"].ToString()

                            });
                        }
                    }
                    return accounts;
                }
            }

        }

       
    }
}

