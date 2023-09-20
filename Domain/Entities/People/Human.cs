namespace Domain.Entities.People;

public class Human
{
    public Human(string name, string surname,  string patronymicName, DateTime dateOfBirth, Sex sex)
    {
        DateOfBirth = dateOfBirth;
        PatronymicName = new string(patronymicName);
        Name = new string(name);
        Surname = new string(surname);
        Sex = sex;
    }

    public Sex Sex { get; private set; }
    
    public string Name { get; }
    public string Surname { get; }
    public string PatronymicName { get; }
    
    public DateTime DateOfBirth { get; }

    public Address Address { get; set; }

}