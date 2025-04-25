using Bankify.Interfaces;
using Bankify.Repository.DTO;
using Bankify.Repository.Entities;
using Bankify.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bankify.Repository.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly IBankifyContext _context;

        public UserRepo(IBankifyContext context)
        {
            _context = context;
        }

        public (int? UserID, int? CustomerID, string Role) LoginUser(string userName, string password)
        {
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("LoginUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int? userID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : (int?)null;
                            int? customerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : (int?)null;
                            string role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null;
                            return (userID, customerID, role);

                        }
                    }
                }
            }
            return (null, null, null);

        }

        public NewCustomerResult InsertNewCustomer(Customer customer, string accountTypeName)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Gender", customer.Gender);
                parameters.Add("@Givenname", customer.Givenname);
                parameters.Add("@Surname", customer.Surname);
                parameters.Add("@Streetaddress", customer.Streetaddress);
                parameters.Add("@City", customer.City);
                parameters.Add("@Zipcode", customer.Zipcode);
                parameters.Add("@Country", customer.Country);
                parameters.Add("@CountryCode", customer.CountryCode);
                parameters.Add("@Birthday", customer.Birthday);
                parameters.Add("@Telephonecountrycode", customer.Telephonecountrycode);
                parameters.Add("@Telephonenumber", customer.Telephonenumber);
                parameters.Add("@Emailaddress", customer.Emailaddress);
                parameters.Add("@AccountTypeName", accountTypeName);

                var result = db.QuerySingle<NewCustomerResult>(
                    "InsertNewCustomer",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }


        public void InsertLoan(LoanDto loan)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@AccountId", loan.AccountID);
                parameters.Add("@Date", loan.Date);
                parameters.Add("@Amount", loan.Amount);
                parameters.Add("@Duration", loan.Duration);
                parameters.Add("@Payments", loan.Payments);
                parameters.Add("@Status", loan.Status);
                parameters.Add("@TypeName", loan.TypeName);

                db.Execute("InsertLoan",
                          parameters,
                          commandType: CommandType.StoredProcedure);


            }
        }




        public void InsertNewCustomer(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByCredentials(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}







                