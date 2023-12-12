namespace Data.Models.Polyclinic.Treatment;

public class DBModelTreatmentCourse
{
    ICollection<DBModelTreatmentStage> TreatmentStages { get; set; }

    public (ICollection<Disease.DBModelDisease>, DateTime) LastDiagnoses()
    {
        return (TreatmentStages.Last().Diagnosis, TreatmentStages.Last().Date);
    }

    public uint ID { get; }
    public DBModelTreatmentCourse(ICollection<DBModelTreatmentStage> treatmentStages, uint iD)
    {
        foreach (var stage_1 in treatmentStages)
        {
            foreach (var stage_2 in treatmentStages)
            {
                if (stage_1.ClientID != stage_2.ClientID)
                {
                    throw new ArgumentException($"Different clients treatment stages: stages with id's: {stage_1.ClientID}, {stage_2.ClientID}");
                }
            }
        }
        TreatmentStages = treatmentStages;
        ID = iD;
    }

    public uint ClientID { get => TreatmentStages.First().ClientID; }

    public ICollection<Disease.DBModelDisease> GetDiseases()
    {
        var diseases = new List<Disease.DBModelDisease>();
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