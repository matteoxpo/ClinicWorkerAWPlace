namespace Domain.Entities.Polyclinic.Building;

public sealed class Cabinet
{
    public Cabinet(string number, string description, int id)
    {
        Number = number ?? throw new NullReferenceException("Number is null");
        Description = description ?? throw new NullReferenceException("Description is null");
        ID = id;
    }

    public string Number { get; set; }

    public string Description { get; set; }
    public int ID { get; }
}