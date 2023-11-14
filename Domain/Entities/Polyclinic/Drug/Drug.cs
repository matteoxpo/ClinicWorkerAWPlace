namespace Domain.Entities.Polyclinic.Drug;


public class Drug
{
    public string Name { get; }
    public string Description { get; }
    public bool Recipe { get; }

    public uint ID { get; }
    public Drug(string name, string description, bool recipe, uint id)
    {
        Name = name;
        Description = description;
        Recipe = recipe;
        ID = id;
    }
}