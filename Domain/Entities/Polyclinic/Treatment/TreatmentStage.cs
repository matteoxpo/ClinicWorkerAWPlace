using System.Transactions;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.Polyclinic.Treatment;

public class TreatmentStage
{

    public Appointment.Appointment CurrentAppointment { get; set; }



    public TreatmentStage(Appointment.Appointment currentAppointment,
                          ICollection<Drug.Drug> drugs,
                          string description,
                          ICollection<Disease.Disease> diagnosis,
                          DateTime date,
                          ICollection<ReferralForAnalysis> analyses,
                          Appointment.Appointment? nextAppointment = null
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
    public ICollection<Disease.Disease> Diagnosis { get; }

    public ICollection<ReferralForAnalysis> Analyses { get; }

    public ICollection<Drug.Drug> Drug { get; }
    public void AddDrug(Drug.Drug drug)
    {
        this.Drug.Add(drug);
    }

    public string Description { get; set; }
    public Appointment.Appointment? NextAppointment { get; set; }
}