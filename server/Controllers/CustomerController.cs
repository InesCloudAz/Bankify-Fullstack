using Bankify.Repository.DTO;
using Bankify.Repository.Entities;
using Bankify.Repository.Interfaces;
using Bankify.Repository.Repos;
using Bankify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bankify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly CustomerService _customerService;

        public CustomerController(ICustomerRepo customerRepo, CustomerService customerService)
        {
            _customerRepo = customerRepo;
            _customerService = customerService;
        }

        [HttpPost("login")]
        public IActionResult LoginCustomer([FromBody] LoginCustomerDTO loginDTO)
        {
            var result = _customerRepo.LoginCustomer(loginDTO.Username, loginDTO.Password);

            if (result.CustomerID != null && result.Role == "Customer")
            {
                var user = new User { Username = loginDTO.Username, Password = loginDTO.Password, Role = result.Role, CustomerID = result.CustomerID };
                var token = _customerService.GenerateToken(user);

                return Ok(new
                {
                    Message = "Inloggning lyckades som kund!",
                    Token = token,
                    CustomerID = result.CustomerID
                });
            }
            else
            {
                return Unauthorized("Fel användarnamn eller lösenord, eller du är inte en kund.");
            }
        }

        [HttpPost("create-account")]
        [Authorize(Roles = "Customer")]
        public IActionResult CreateAccount([FromBody] CreateAccountDto dto)
        {
            var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CustomerID")?.Value;

            if (customerIdClaim == null)
            {
                return Unauthorized("Kund-ID saknas i token.");
            }

            int customerId = Convert.ToInt32(customerIdClaim);
            int accountId = _customerRepo.CreateAccount(customerId, dto.TypeName, dto.InitialDeposit, dto.Frequency);

            return Ok(new { Message = "Konto skapat!", AccountID = accountId });
        }

        [HttpPost("transfer-between-accounts")]
        [Authorize(Roles = "Customer")]
        public IActionResult TransferBetweenAccounts([FromBody] TransferDto dto)
        {
            var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CustomerID")?.Value;

            if (customerIdClaim == null)
            {
                return Unauthorized("Kund-ID saknas i token.");
            }

            int customerId = Convert.ToInt32(customerIdClaim);


            bool success = _customerRepo.TransferBetweenAccounts(dto.FromAccountNumber, dto.ToAccountNumber, dto.Amount, customerId);

            //if (!success)
            //{
            //    return BadRequest("Överföringen misslyckades. Kontrollera saldot och att kontona tillhör dig.");
            //}

            return Ok("Överföringen lyckades.");
        }

        [HttpPost("transfer-to-another-customer")]
        [Authorize(Roles = "Customer")]
        public IActionResult TransferToAnotherCustomer([FromBody] TransferDto dto)
        {
            var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CustomerID")?.Value;

            if (customerIdClaim == null)
            {
                return Unauthorized("Kund-ID saknas i token.");
            }

            int customerId = Convert.ToInt32(customerIdClaim);
            bool success = _customerRepo.TransferToAnotherCustomer(dto.FromAccountNumber, dto.ToAccountNumber, dto.Amount, customerId);

            //if (!success)
            //{
            //    return BadRequest("Överföringen misslyckades. Kontrollera saldot och att du har angett rätt kontonummer.");
            //}

            return Ok("Överföringen lyckades.");
        }

        [HttpGet("transactions/by-account-number/{accountNumber}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAccountTransactions(string accountNumber)
        {
            var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CustomerID")?.Value;

            if (customerIdClaim == null)
            {
                return Unauthorized("Kund-ID saknas i token.");
            }

            int customerId = Convert.ToInt32(customerIdClaim);
            var transactions = _customerRepo.GetAccountTransactions(accountNumber, customerId);

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("Inga transaktioner hittades eller kontot tillhör inte dig.");
            }

            return Ok(transactions);
        }

        [HttpGet("accounts")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetCustomerAccounts()
        {
            var customerIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CustomerID")?.Value;

            if (customerIdClaim == null)
            {
                return Unauthorized("Kund-ID saknas i token.");
            }

            int customerId = Convert.ToInt32(customerIdClaim);
            var accounts = _customerRepo.GetCustomerAccounts(customerId);

            if (accounts == null || accounts.Count == 0)
            {
                return NotFound("Inga konton hittades.");
            }

            var accountResponse = accounts.Select(a => new AccountResponseDTO
    {
        TypeName = a.TypeName,
        Balance = a.Balance,
        Created = a.Created,
        Frequency = a.Frequency,
        AccountNumber = a.AccountNumber
    }).ToList();


            return Ok(accountResponse);
        }


    }

}

