namespace Domain.Entities;

public class EmployeeExperience
{

    public EmployeeExperience(EducationalInstitution education,string specialization, uint experience, uint id, uint doctorId )
    {
        Experience = experience;
        ID = id;
        DoctorID = doctorId;
        Education = education;
        Specialization = specialization;
    }
    public uint ID { get; private set; }
    
    public uint DoctorID { get; private set; }
    public uint Experience { get; private set; }
    public string Specialization { get; private set; }
    
    public EducationalInstitution Education { get; private set; }
    
}