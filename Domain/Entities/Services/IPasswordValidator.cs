
using System.Text.RegularExpressions;

namespace Domain.Services;

public interface IPasswordValidator
{
    public bool Validate(string password) { return PasswordRegex.IsMatch(password); }

    public string Reason(string invalidPassword);
    public Regex PasswordRegex { get; set; }
}

public class PasswordValidatingException : Exception
{
    public PasswordValidatingException(string message) : base(message) { }
}