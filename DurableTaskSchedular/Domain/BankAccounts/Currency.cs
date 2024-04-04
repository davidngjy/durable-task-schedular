using Domain.Abstractions;

namespace Domain.BankAccounts;

public abstract class Currency : Enumeration<Currency>
{
    public static Currency USD => new Usd();
    public static Currency AUD => new Aud();
    public static Currency NZD => new Nzd();

    private class Usd : Currency
    {
        public override string Code => "USD";
    }

    private class Aud : Currency
    {
        public override string Code => "AUD";
    }

    private class Nzd : Currency
    {
        public override string Code => "NZD";
    }
}
