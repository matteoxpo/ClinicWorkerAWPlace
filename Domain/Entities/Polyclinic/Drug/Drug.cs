namespace Domain.Entities.Polyclinic.Drug;


public class Drug
{
    public string Name { get; }
    public string Description { get; }
    public bool Recipe { get; }

    public int ID { get; }
    public Drug(string name, string description, bool recipe, int id)
    {
        Name = name;
        Description = description;
        Recipe = recipe;
        ID = id;
    }
}