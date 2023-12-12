using Domain.Entities.Common;

namespace Data.Models.Common;

public sealed class DBModelAddress : IDBConverter<Address, DBModelAddress>
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }

    public uint ID { get; }

    public DBModelAddress(string street, string city, string state, string zipCode, string country, uint id)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        ID = id;
    }

    public DBModelAddress()
    {
    }

    public Address ConvertFromStorageEntity(DBModelAddress entity)
    {
        return new Address(entity.Street, entity.City, entity.State, entity.ZipCode, entity.Country, entity.ID);
    }

    public Address ConvertFromEntity()
    {
        return new Address(Street, City, State, ZipCode, Country, ID);
    }

    public ICollection<Address> ConvertFromStorageEntity(IEnumerable<DBModelAddress> entities)
    {
        var collection = new Address[entities.Count()];
        foreach (var entity in entities)
        {
            collection.Append(ConvertFromStorageEntity(entity));
        }
        return collection;
    }

    public DBModelAddress ConvertFromEntity(Address entity)
    {
        return new DBModelAddress(entity.Street, entity.City, entity.State, entity.ZipCode, entity.Country, entity.ID);
    }

    public ICollection<DBModelAddress> ConvertFromEntity(IEnumerable<Address> entities)
    {
        var collection = new DBModelAddress[entities.Count()];
        foreach (var entity in entities)
        {
            collection.Append(ConvertFromEntity(entity));
        }
        return collection;
    }
}
