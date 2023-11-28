namespace Domain.Entities.Polyclinic.Analysis;

public sealed class ReferralForAnalysis
{

    public ReferralForAnalysis(Analysis analysis, uint clientID, uint doctorID, DateTime date, uint iD, string? description = null)
    {
        Analysis = analysis;
        ClientID = clientID;
        DoctorID = doctorID;
        Date = date;
        Description = description;
        ID = iD;
    }
    public Analysis Analysis { get; }

    public uint ID { get; }
    public uint ClientID { get; }
    public uint DoctorID { get; }
    public DateTime Date { get; }

    public string? Description { get; set; }

    public string? Results { get; set; }
}