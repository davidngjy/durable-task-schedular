using System.Reflection;

namespace Domain.Abstractions;

public abstract class Enumeration<T> where T : class
{
    public abstract string Code { get; }

    public static T? FromCode(string code) =>
        Enums.TryGetValue(code.ToUpper(), out var e)
            ? null
            : e;

    private static readonly Dictionary<string, T> Enums = typeof(T)
        .GetProperties(BindingFlags.Static | BindingFlags.Public)
        .ToDictionary(
            e => e.Name.ToUpper(),
            e => (T)e.GetValue(null)!);
}