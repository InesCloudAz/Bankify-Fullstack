using Bankify.Controllers;
using Bankify.Repository.DTO;
using Bankify.Repository.Entities;
using Bankify.Repository.Interfaces;
using Bankify.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

public class CustomerControllerTests
{
    private readonly Mock<ICustomerRepo> _mockRepo;
    private readonly CustomerService _customerService;
    private readonly CustomerController _controller;

    public CustomerControllerTests()
    {
        _mockRepo = new Mock<ICustomerRepo>();
        _customerService = new CustomerService();
        _controller = new CustomerController(_mockRepo.Object, _customerService);
    }

    [Fact]
    public void LoginCustomer_ValidCredentials_ReturnsOkResult()
    {
        // Arrange
        var loginDto = new LoginCustomerDTO { Username = "testUser", Password = "testPass" };

        // Mocka upp resultatet från LoginCustomer-metoden
        _mockRepo.Setup(repo => repo.LoginCustomer(loginDto.Username, loginDto.Password))
            .Returns((1, "Customer"));

        // Act
        var result = _controller.LoginCustomer(loginDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);  // Kontrollera att resultatet inte är null
        Assert.Equal(200, result.StatusCode);  // Kontrollera att statuskoden är 200
    }

    [Fact]
    public void CreateAccount_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var createAccountDto = new CreateAccountDto { TypeName = "Savings", InitialDeposit = 100, Frequency = "Monthly" };
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim("CustomerID", "1")
        }));

        _mockRepo.Setup(repo => repo.CreateAccount(1, createAccountDto.TypeName, createAccountDto.InitialDeposit, createAccountDto.Frequency))
            .Returns(1);

        // Act
        var result = _controller.CreateAccount(createAccountDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void TransferBetweenAccounts_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var transferDto = new TransferDto { FromAccountNumber = "123", ToAccountNumber = "456", Amount = 50 };
        _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim("CustomerID", "1")
        }));

        _mockRepo.Setup(repo => repo.TransferBetweenAccounts(transferDto.FromAccountNumber, transferDto.ToAccountNumber, transferDto.Amount, 1))
            .Returns(true);

        // Act
        var result = _controller.TransferBetweenAccounts(transferDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}