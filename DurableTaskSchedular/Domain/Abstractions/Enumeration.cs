using System.Reflection;

namespace Domain.Abstractions;

public abstract class Enumeration<T> : IEquatable<T> where T : class
{
    public abstract string Code { get; }

    public static T? FromCode(string code) =>
        Enums.GetValueOrDefault(code.ToUpper());

    private static readonly Dictionary<string, T> Enums = typeof(T)
        .GetProperties(BindingFlags.Static | BindingFlags.Public)
        .ToDictionary(
            e => e.Name.ToUpper(),
            e => (T)e.GetValue(null)!);

    private bool Equals(Enumeration<T> other) => Code == other.Code;

    public bool Equals(T? other) => other is not null && Equals(this, other);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((Enumeration<T>)obj);
    }

    public override int GetHashCode() => Code.GetHashCode();
}
