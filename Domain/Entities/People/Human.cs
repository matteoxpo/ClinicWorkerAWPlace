namespace Domain.Entities.People;
[Serializable]
public abstract class Human
{
    protected string _name;
    public string Name { get => _name; set => _name = value; }
    public string Surname { get => _surname; set => _surname = value; }
    protected string _surname;


    public Human(string name, string surname)
    {
        _name = new string(name);
        _surname = new string(surname);
    }

    public Human()
    {
        _name = new string("name");
        _surname = new string("surname");
    }

}
