using Domain.Entities.Polyclinic.Analysis;

namespace Data.Models.Polyclinic.Treatment;
public class DBModelTreatmentStage
{
    public Appointment.DBModelAppointment CurrentAppointment { get; set; }

    public DBModelTreatmentStage(Appointment.DBModelAppointment currentAppointment,
                          ICollection<Drug.DBModelDrug> drugs,
                          string description,
                          ICollection<Disease.DBModelDisease> diagnosis,
                          DateTime date,
                          ICollection<ReferralForAnalysis> analyses,
                          Appointment.DBModelAppointment? nextAppointment = null
                          )
    {
        CurrentAppointment = currentAppointment;
        Drug = drugs;
        Description = description;
        Diagnosis = diagnosis;
        NextAppointment = nextAppointment;
        Date = date;
        Analyses = analyses;
    }

    public uint ID { get; }

    public uint DoctorID { get => CurrentAppointment.DoctorID; }
    public uint ClientID { get => CurrentAppointment.ClientID; }

    public DateTime Date { get; set; }
    public ICollection<Disease.DBModelDisease> Diagnosis { get; }

    public ICollection<ReferralForAnalysis> Analyses { get; }

    public ICollection<Drug.DBModelDrug> Drug { get; }
    public void AddDrug(Drug.DBModelDrug drug)
    {
        this.Drug.Add(drug);
    }

    public string Description { get; set; }
    public Appointment.DBModelAppointment? NextAppointment { get; set; }
}