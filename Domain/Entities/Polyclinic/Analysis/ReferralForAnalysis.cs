namespace Domain.Entities.Polyclinic.Analysis;

public sealed class ReferralForAnalysis
{
    public ReferralForAnalysis(Analysis analysis, int clientID, int doctorID, DateTime date, string description, int id, string? results)
    {
        Analysis = analysis;
        ClientID = clientID;
        DoctorID = doctorID;
        Date = date;
        Description = description;
        ID = id;
        Results = results;
    }
    public Analysis Analysis { get; }
    public int ID { get; }
    public int ClientID { get; }
    public int DoctorID { get; }
    public DateTime Date { get; }
    public string Description { get; set; }
    public string? Results { get; set; }
}