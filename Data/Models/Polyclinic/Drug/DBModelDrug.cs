namespace Data.Models.Polyclinic.Drug;

public class DBModelDrug
{
    public string Name { get; }
    public string Description { get; }
    public bool Recipe { get; }

    public uint ID { get; }
    public DBModelDrug(string name, string description, bool recipe, uint id)
    {
        Name = name;
        Description = description;
        Recipe = recipe;
        ID = id;
    }
}