using System.Transactions;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;

namespace Domain.Entities.Polyclinic.Treatment;

public class TreatmentStage
{

    public TreatmentStage(ICollection<Drug.Drug> drugs,
                          string description,
                          Disease.Disease diagnosis,
                          ICollection<ReferralForAnalysis> analyses,
                          int employeeUserId,
                          int id
                          )
    {
        EmployeeUserId = employeeUserId;
        Drug = drugs;
        Description = description;
        Diagnosis = diagnosis;
        Analyses = analyses;
        ID = id;
    }

    public int ID { get; }
    public Disease.Disease Diagnosis { get; }

    public int EmployeeUserId { get; }
    public ICollection<ReferralForAnalysis> Analyses { get; }

    public ICollection<Drug.Drug> Drug { get; }
    public void AddDrug(Drug.Drug drug)
    {
        Drug.Add(drug);
    }

    public string Description { get; set; }
}