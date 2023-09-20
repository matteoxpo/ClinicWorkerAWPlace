namespace Domain.Entities;

public class EducationalInstitution
{
    public EducationalInstitution(string institutionName, DateTime year, uint id)
    {
        InstitutionName = institutionName;
        Year = year;
        ID = id;
    }

    public string InstitutionName { get; private set; }
    public DateTime Year { get; private set; }
    public uint ID { get; private set; }
}