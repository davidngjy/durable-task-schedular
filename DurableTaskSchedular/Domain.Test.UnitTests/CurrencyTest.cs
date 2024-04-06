using Domain.BankAccounts;

namespace Domain.Test.UnitTests;

public class CurrencyTest
{
    [Fact]
    public void TestEqual()
    {
        var x = Currency.FromCode("AUD");
        var y = Currency.AUD;

        Assert.Equal(x ,y);
    }

    [Fact]
    public void TestNotEqual()
    {
        var x = Currency.FromCode("AUD");
        var y = Currency.NZD;

        Assert.NotEqual(x ,y);
    }
}
