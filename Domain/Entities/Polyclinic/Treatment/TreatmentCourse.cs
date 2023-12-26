namespace Domain.Entities.Polyclinic.Treatment;

public class TreatmentCourse
{
    public ICollection<TreatmentStage> TreatmentStages { get; set; }

    public int ID { get; }
    public TreatmentCourse(ICollection<TreatmentStage> treatmentStages, int iD)
    {
        TreatmentStages = treatmentStages;
        ID = iD;
    }

}