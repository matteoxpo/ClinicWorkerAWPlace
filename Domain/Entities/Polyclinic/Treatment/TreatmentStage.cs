using System.Transactions;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.Polyclinic.Treatment;

public class TreatmentStage
{

    public TreatmentStage(ICollection<Drug.Drug> drugs,
                          string description,
                          ICollection<Disease.Disease> diagnosis,
                          DateTime date,
                          ICollection<ReferralForAnalysis> analyses,
                          int doctorId,
                          int clientId,
                          int id
                          )
    {
        Drug = drugs;
        Description = description;
        Diagnosis = diagnosis;
        Date = date;
        Analyses = analyses;
        DoctorId = doctorId;
        ClientId = clientId;
        ID = id;
    }

    public int ID { get; }

    public int DoctorId { get; }
    public int ClientId { get; }

    public DateTime Date { get; set; }
    public ICollection<Disease.Disease> Diagnosis { get; }

    public ICollection<ReferralForAnalysis> Analyses { get; }

    public ICollection<Drug.Drug> Drug { get; }
    public void AddDrug(Drug.Drug drug)
    {
        Drug.Add(drug);
    }

    public string Description { get; set; }
    public Appointment.Appointment? NextAppointment { get; set; }
}