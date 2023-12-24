
namespace Domain.Entities.Common;

public enum ContactType
{
    Email,
    PhoneNumber,
}

public sealed class Contact
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int ID { get; }

    public Contact(string? phoneNumber, string? email, int id)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        ID = id;
    }
}

public class ContactException : Exception
{
    public ContactException(string message) : base(message) { }
}