using Bankify.Repository.DTO;
using Bankify.Repository.Entities;
using Bankify.Repository.Interfaces;
using Bankify.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bankify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UserController : ControllerBase
    {

        private readonly IUserRepo _userRepo;
        private readonly UserService _userService;

        public UserController(IUserRepo userRepo, UserService userService)
        {
            _userRepo = userRepo;
            _userService = userService;
        }




        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] LoginUserDTO loginDTO)
        {
            var result = _userRepo.LoginUser(loginDTO.UserName, loginDTO.Password);

            if (result.Role != null)
            {
                if (result.Role == "Admin")
                {
                   
                    var token = _userService.GenerateToken(new User
                    {
                        UserID = result.UserID.Value,
                        Username = loginDTO.UserName,
                        Role = result.Role
                    });

                    return Ok(new
                    {
                        Message = "Inloggning lyckades som admin!",
                        Token = token,
                        Role = result.Role,
                        UserID = result.UserID
                    });
                }
                else if (result.Role == "User")
                {
                    // Skapa token för kund
                    var token = _userService.GenerateToken(new User
                    {
                        UserID = result.CustomerID.Value,
                        Username = loginDTO.UserName,
                        Role = result.Role
                    });

                    return Ok(new
                    {
                        Message = "Inloggning lyckades som kund!",
                        Token = token,
                        Role = result.Role,
                        CustomerID = result.CustomerID
                    });
                }
            }

            return Unauthorized("Fel användarnamn eller lösenord.");
        }




        [HttpPost("NewCustomer")]
        [Authorize]
        public IActionResult InsertNewCustomer(Customer customer, [FromQuery] string accountTypeName = "Standard transaction account")
        {

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("Token innehåller inte nödvändig användarinformation.");
            }

            var result = _userRepo.InsertNewCustomer(customer, accountTypeName);

            return Ok(new
            {
                Message = "Ny kund, bankkonto och användare skapades.",
                CustomerID = result.CustomerId,
                //UserID = result.NewUserId,
                Username = result.Username,
                Password = result.Password,
                AccountNumber = result.AccountNumber
            });


        }


        [HttpPost("NewLoan")]
        [Authorize]
        public IActionResult InsertLoan([FromBody] LoanDto loan)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("Token innehåller inte nödvändig användarinformation.");
            }


            _userRepo.InsertLoan(loan);
            return Ok(new
            {
                Message = "Nytt lån skapades och beloppet har satts in på kundens konto.",
                AccountID = loan.AccountID,
                CustomerID = loan.CustomerID,
                Amount = loan.Amount,
                TypeName = loan.TypeName
            });

        }
    }
}





