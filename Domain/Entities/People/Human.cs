namespace Domain.Entities.People;
public abstract class Human
{
    protected string _name;
    public string Name { get => _name; set => _name = value; }
    public string Surname { get => _surname; set => _surname = value; }
    public  DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
    protected string _surname;
    protected DateTime _dateOfBirth;
    public Human(string name, string surname, DateTime dateTime)
    {
        _name = new string(name);
        _surname = new string(surname);
        _dateOfBirth = dateTime;
    }

    public Human()
    {
        _name = new string("name");
        _surname = new string("surname");
        _dateOfBirth = new DateTime(0);
    }

}
