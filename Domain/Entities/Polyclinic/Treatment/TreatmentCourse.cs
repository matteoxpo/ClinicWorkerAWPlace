namespace Domain.Entities.Polyclinic.Treatment;

public class TreatmentCourse
{
    public ICollection<TreatmentStage> TreatmentStages { get; set; }

    public (ICollection<Disease.Disease>, DateTime) LastDiagnoses()
    {
        return (TreatmentStages.Last().Diagnosis, TreatmentStages.Last().Date);
    }

    public int ID { get; }
    public TreatmentCourse(ICollection<TreatmentStage> treatmentStages, int iD)
    {
        foreach (var stage_1 in treatmentStages)
        {
            foreach (var stage_2 in treatmentStages)
            {
                if (stage_1.ClientId != stage_2.ClientId)
                {
                    throw new ArgumentException($"Different clients treatment stages: stages with id's: {stage_1.ClientId}, {stage_2.ClientId}");
                }
            }
        }
        TreatmentStages = treatmentStages;
        ID = iD;
    }

    public int ClientID { get => TreatmentStages.First().ClientId; }

    public ICollection<Disease.Disease> GetDiseases()
    {
        var diseases = new List<Disease.Disease>();
        foreach (var stage in TreatmentStages)
        {
            foreach (var disease in stage.Diagnosis)
            {
                if (!diseases.Contains(disease))
                {
                    diseases.Add(disease);
                }
            }
        }

        return diseases;
    }
}