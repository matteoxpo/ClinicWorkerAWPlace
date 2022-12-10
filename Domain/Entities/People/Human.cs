namespace Domain.Entities.People;

[Serializable]
public abstract class Human
{
    public string Name { get; set; }
    public string Surname { get ; set; }
    public  DateTime DateOfBirth { get; set; }
    public Human(string name, string surname, DateTime dateTime)
    {
        Name = new string(name);
        Surname = new string(surname);
        DateOfBirth = dateTime;
    }

    public Human()
    {
        Name = new string("name");
        Surname = new string("surname");
        DateOfBirth = new DateTime(0);
    }

}
