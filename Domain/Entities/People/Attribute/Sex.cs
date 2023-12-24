namespace Domain.Entities.People.Attribute;

public enum Sex
{
    Female = 0,
    Male = 1,
}

public static class SexMapper
{
    public static Sex FromString(string sex)
    {
        if (sex.ToLower() == nameof(Sex.Female).ToLower())
        {
            return Sex.Female;
        }
        if (sex.ToLower() == nameof(Sex.Male).ToLower())
        {
            return Sex.Male;
        }
        throw new SexMapperException($"Error: can't convet {sex} to Sex: male/frmale");
    }

    public class SexMapperException : Exception
    {
        public SexMapperException(string message) : base(message) { }
    }
}