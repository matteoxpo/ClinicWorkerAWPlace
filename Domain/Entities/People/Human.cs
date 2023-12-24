using Domain.Entities.People.Attribute;

using Domain.Entities.Common;

namespace Domain.Entities.People;

public class Human
{
    public Human(string name,
                 string surname,
                 string patronymicName,
                 Address address,
                 DateTime dateOfBirth,
                 Sex sex,
                 int id,
                 MedicalPolicy policy,
                 ICollection<Contact> contacts,
                 ICollection<Education>? education,
                 ICollection<Benefit>? benefits)
    {
        DateOfBirth = dateOfBirth;
        PatronymicName = new string(patronymicName) ?? throw new NullReferenceException("PatronymicName is null");
        Name = new string(name) ?? throw new NullReferenceException("Name is null");
        Surname = new string(surname) ?? throw new NullReferenceException("Surname is null");
        Sex = sex;
        Address = address ?? throw new NullReferenceException("Address is null");
        ID = id;
        Policy = policy ?? throw new NullReferenceException("MedPolicy is null");

        Contacts = contacts ?? new List<Contact>();
        Education = education ?? new List<Education>();
        Benefits = benefits ?? new List<Benefit>();
    }

    public void AddContact(ICollection<Contact> contacts)
    {
        foreach (var contact in contacts)
        {
            if (Contacts.Contains(contact))
            {
                throw new HumanException("This contact is already in contacts");
            }
        }
        Contacts.Concat(contacts);
    }

    public void AddContact(Contact contact)
    {
        if (Contacts.Contains(contact))
        {
            throw new HumanException("This contact is already in contacts");
        }
        Contacts.Add(contact);
    }

    public void AddEducation(Education education)
    {
        if (Education.Contains(education))
        {
            throw new HumanException("This education is already in contacts");
        }
        Education.Add(education);
    }
    public void AddBenefit(Benefit benefit)
    {
        if (Benefits.Contains(benefit))
        {
            throw new HumanException("This education is already in contacts");
        }
        Benefits.Add(benefit);
    }

    public int ID { get; }
    public ICollection<Contact> Contacts { get; set; }
    public ICollection<Education> Education { get; set; }
    public ICollection<Benefit> Benefits { get; set; }

    public MedicalPolicy Policy { get; set; }
    public Sex Sex { get; set; }

    public string Name { get; }
    public string Surname { get; }
    public string PatronymicName { get; }

    public DateTime DateOfBirth { get; }

    public Address Address { get; set; }

    public class HumanException : Exception
    {
        public HumanException(string message) : base(message) { }
    }

}