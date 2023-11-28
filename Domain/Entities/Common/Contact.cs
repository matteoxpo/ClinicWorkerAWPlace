
namespace Domain.Common;

public sealed class Contact
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    private uint ID { get; set; }

    public Contact(string? phoneNumber, string? email, uint id)
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