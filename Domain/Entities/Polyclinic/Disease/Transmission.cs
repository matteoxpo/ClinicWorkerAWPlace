namespace Domain.Entities.Polyclinic.Disease;

public enum Transmission
{
    Contact,
    Airborne,
    FecalOral,
    VectorBorne,
    Perinatal,
    Sexual,
    Bloodborne,
    Other,
}

public static class TransmissionMapper
{
    public static Transmission FromString(string transmission)
    {
        foreach (Transmission tr in Enum.GetValues(typeof(Transmission)))
        {
            if (transmission.ToLower() == tr.ToString().ToLower())
            {
                return tr;
            }
        }
        return Transmission.Other;
    }
}