
using System.Runtime.CompilerServices;
using Domain.Entities.Common;

namespace Data.Models.Common;

public sealed class DBModelContact : IDBConverter<Contact, DBModelContact>
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    private uint ID { get; set; }

    public DBModelContact(string? phoneNumber, string? email, uint id)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        ID = id;
    }

    public DBModelContact()
    {
    }

    public Contact ConvertFromStorageEntity(DBModelContact entity)
    {
        return new Contact(entity.PhoneNumber, entity.Email, entity.ID);
    }

    public ICollection<Contact> ConvertFromStorageEntity(IEnumerable<DBModelContact> entities)
    {
        var contacts = new Contact[entities.Count()];
        foreach (var entity in entities)
        {
            contacts.Append(ConvertFromStorageEntity(entity));
        }
        return contacts;
    }

    public Contact ConvertFromEntity()
    {
        return new Contact(PhoneNumber, Email, ID);
    }

    public DBModelContact ConvertFromEntity(Contact entity)
    {
        return new DBModelContact(entity.PhoneNumber, entity.Email, entity.ID);
    }

    public ICollection<DBModelContact> ConvertFromEntity(IEnumerable<Contact> entities)
    {
        var contacts = new DBModelContact[entities.Count()];
        foreach (var entity in entities)
        {
            contacts.Append(ConvertFromEntity(entity));
        }
        return contacts;
    }
}