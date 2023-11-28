using Domain.Entities.People.Attribute;

using Domain.Common;

namespace Domain.Entities.People;

public class Human
{
    public Human(string name,
                 string surname,
                 string patronymicName,
                 Address address,
                 DateTime dateOfBirth,
                 Sex sex,
                 uint id,
                 MedicalPolicy policy,
                 ICollection<Contact>? contact = null,
                 ICollection<Education>? education = null,
                 ICollection<Benefit>? benefits = null)
    {
        DateOfBirth = dateOfBirth;
        PatronymicName = new string(patronymicName) ?? throw new NullReferenceException("PatronymicName is null");
        Name = new string(name) ?? throw new NullReferenceException("Name is null");
        Surname = new string(surname) ?? throw new NullReferenceException("Surname is null");
        Sex = sex;
        Address = address ?? throw new NullReferenceException("Address is null");
        ID = id;
        Policy = policy ?? throw new NullReferenceException("MedPolicy is null");

        Contact = contact ?? new List<Contact>();
        Education = education ?? new List<Education>();
        Benefits = benefits ?? new List<Benefit>();
    }

    public void AddContact(ICollection<Contact> contacts)
    {
        foreach (var contact in contacts)
        {
            if (Contact.Contains(contact))
            {
                throw new ContactException("This contact is already in contacts");
            }
        }
        Contact.Concat(contacts);
    }

    public void AddContact(Contact contact)
    {
        if (Contact.Contains(contact))
        {
            throw new ContactException("This contact is already in contacts");
        }
        Contact.Add(contact);
    }

    public uint ID { get; }
    public ICollection<Contact> Contact { get; set; }
    public ICollection<Education> Education { get; set; }
    public ICollection<Benefit>? Benefits { get; set; }

    public MedicalPolicy Policy { get; set; }
    public Sex Sex { get; set; }

    public string Name { get; }
    public string Surname { get; }
    public string PatronymicName { get; }

    public DateTime DateOfBirth { get; }

    public Address Address { get; set; }

}