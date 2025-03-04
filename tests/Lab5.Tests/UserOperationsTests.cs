using Application.Abstraction;
using Application.Models.Models;
using Application.Models.ResultType;
using Application.Services;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class UserOperationsTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserOperationsTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void Withdraw_WithSufficientBalance_ShouldUpdateBalanceAndReturnSuccess()
    {
        var user = new User(1, "test", 100);
        int withdrawalAmount = 50;

        _userRepositoryMock.Setup(repo => repo.UpdateBalance(user, TransactionType.Withdrawal));

        ResultTypeOperation result = _userService.Withdraw(user, withdrawalAmount);

        Assert.IsType<ResultTypeOperation.Success>(result);
        Assert.Equal(50, user.Balance);
        _userRepositoryMock.Verify(repo => repo.UpdateBalance(user, TransactionType.Withdrawal), Times.Once);
    }

    [Fact]
    public void Withdraw_WithInsufficientBalance_ShouldReturnFailure()
    {
        var user = new User(1, "test", 30);
        int withdrawalAmount = 50;

        ResultTypeOperation result = _userService.Withdraw(user, withdrawalAmount);

        Assert.IsType<ResultTypeOperation.Failure>(result);
        var failureResult = result as ResultTypeOperation.Failure;
        Assert.NotNull(failureResult);
        Assert.Equal("Insufficient funds", failureResult?.Message);
        Assert.Equal(30, user.Balance);
        _userRepositoryMock.Verify(repo => repo.UpdateBalance(It.IsAny<User>(), TransactionType.Withdrawal), Times.Never);
    }

    [Fact]
    public void Deposit_WithValidAmount_ShouldUpdateBalanceAndReturnSuccess()
    {
        var user = new User(1, "test", 100);
        int depositAmount = 50;

        _userRepositoryMock.Setup(repo => repo.UpdateBalance(user, TransactionType.Deposit));

        ResultTypeOperation result = _userService.Deposit(user, depositAmount);

        Assert.IsType<ResultTypeOperation.Success>(result);
        Assert.Equal(150, user.Balance);
        _userRepositoryMock.Verify(repo => repo.UpdateBalance(user, TransactionType.Deposit), Times.Once);
    }

    [Fact]
    public void Deposit_WithInvalidAmount_ShouldReturnFailure()
    {
        var user = new User(1, "test", 100);
        int depositAmount = -50;

        ResultTypeOperation result = _userService.Deposit(user, depositAmount);

        Assert.IsType<ResultTypeOperation.Failure>(result);
        var failureResult = result as ResultTypeOperation.Failure;
        Assert.Equal("Amount must be greater than zero", failureResult?.Message);
        Assert.Equal(100, user.Balance);
        _userRepositoryMock.Verify(repo => repo.UpdateBalance(It.IsAny<User>(), TransactionType.Deposit), Times.Never);
    }
}
