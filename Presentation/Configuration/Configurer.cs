using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using Data.Repositories.App;
using Data.Repositories.App.Role;
using Data.Repositories.App.Role.Employee;
using Data.Repositories.Common;
using Data.Repositories.HumanAttribute;
using Data.Repositories.Polyclinic;
using Domain.Repositories.App;
using Domain.Repositories.App.Role;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;
using Domain.Repositories.Polyclinic;

namespace Presentation.Configuration;


public class RepositoriesConfigurer : IDisposable
{
    private static string PathToDatabase { get; set; } = @"F:\ProgramFiles\Programming\ClinicWorkerAWPlace\Data\data.db";
    private static string ConnectionString { get; } = $"Data Source={PathToDatabase};Version=3;";

    private static RepositoriesConfigurer? repositoriesConfigurer;
    private RepositoriesConfigurer() { }
    public static RepositoriesConfigurer GetRepositoriesConfigurer()
    {
        return repositoriesConfigurer ??= new RepositoriesConfigurer();
    }
    private static SQLiteConnection dbConnection = new SQLiteConnection(ConnectionString);
    private static IAuthRepository? authRepository;
    private static IContactRepository? contactRepository;
    private static IDrugRepository? drugRepository;
    private static IDiseaseRepository? diseaseRepository;
    private static IDoctorRepository? doctorRepository;
    private static IClientRepository? clientRepository;
    private static IRegistrarRepository? registrarRepository;
    private static IBenefitRepository? benefitRepository;
    private static IAppoinmentRepository? appoinmentRepository;
    private static IMedicalPolicyRepository? medicalPolicyRepository;
    private static IEducationRepository? educationRepository;
    private static IMedicineClinicRepository? medicineClinicRepository;
    private static IAddressRepository? addressRepository;
    private static ITreatmentCourseRepositrory? treatmentCourseRepositrory;
    private static IReferralForAnalysisRepository? referralForAnalysisRepository;
    private static IAnalysisRepository? analysisRepository;
    private static ITreatmentStageRepository? treatmentStageRepository;
    private void CheckDbConnection()
    {
        if (dbConnection.State != System.Data.ConnectionState.Open)
        {
            dbConnection.Open();
        }
    }

    public IDiseaseRepository GetDiseaseRepository()
    {
        CheckDbConnection();
        return diseaseRepository ??= new DiseaseRepository(dbConnection, "Disease");
    }
    public IDrugRepository GetDrugRepository()
    {
        CheckDbConnection();
        return drugRepository ??= new DrugRepository(dbConnection, "Drug");
    }
    public IContactRepository GetContactRepository()
    {
        CheckDbConnection();
        return contactRepository ??= new ContactRepository(dbConnection, "Contact");
    }
    public IAuthRepository GetAuthRepository()
    {
        CheckDbConnection();
        return authRepository ??= new AuthRepository(dbConnection, "HumanUser");
    }

    public IBenefitRepository GetBenefitRepository()
    {
        CheckDbConnection();
        return benefitRepository ??= new BenefitRepository(dbConnection, "Benefit");
    }

    public IDoctorRepository GetDoctorRepository()
    {
        CheckDbConnection();
        return doctorRepository ??= new DoctorRepository(dbConnection,
                                                         "HumanUser",
                                                         GetBenefitRepository(),
                                                         GerEducationRepository(),
                                                         GetMedicalPolicyRepository(),
                                                         GetContactRepository(),
                                                         GetAppointmentRepository());
    }


    public IAppoinmentRepository GetAppointmentRepository()
    {
        CheckDbConnection();
        return appoinmentRepository ??= new AppoinmentRepository(dbConnection, "", GetClinicRepository(), GetCabinetRepository());
    }

    public ICabinetRepository GetCabinetRepository()
    {
        CheckDbConnection();
        return new CabinetRepository(dbConnection, "Cabinet");
    }

    public IMedicineClinicRepository GetClinicRepository()
    {
        CheckDbConnection();
        return medicineClinicRepository ??= new MedicineClinicRepository(dbConnection, "Clinic", GetCabinetRepository(), GetAddressRepository(), GetContactRepository());
    }

    public IAddressRepository GetAddressRepository()
    {
        CheckDbConnection();
        return addressRepository ??= new AddressRepository(dbConnection, "Address");
    }

    public IMedicalPolicyRepository GetMedicalPolicyRepository()
    {
        CheckDbConnection();
        return medicalPolicyRepository ??= new MedicalPolicyRepository(dbConnection, "MedicalPolicy");
    }

    public IEducationRepository GerEducationRepository()
    {
        CheckDbConnection();
        return educationRepository ??= new EducationRepository(dbConnection, "Education");
    }

    public IClientRepository GetClientRepository()
    {
        CheckDbConnection();
        return clientRepository ??= new ClientRepository(dbConnection,
                                                         "HumanUser",
                                                         GetBenefitRepository(),
                                                         GetContactRepository(),
                                                         GerEducationRepository(),
                                                         GetMedicalPolicyRepository(),
                                                         GetAddressRepository(),
                                                         GetAppointmentRepository(),
                                                         GetTreatmentCourseRepository());
    }

    public ITreatmentCourseRepositrory GetTreatmentCourseRepository()
    {
        return treatmentCourseRepositrory ??= new TreatmentCourseRepositrory(dbConnection, "TreatmentCourse", GetReferralForAnalysisRepository(), GetTreatmentStageRepository());
    }

    public ITreatmentStageRepository GetTreatmentStageRepository()
    {
        return treatmentStageRepository ??= new TreatmentStageRepository(dbConnection, "TreatmentStage", GetDrugRepository(), GetReferralForAnalysisRepository());
    }

    public IReferralForAnalysisRepository GetReferralForAnalysisRepository()
    {
        return referralForAnalysisRepository ??= new ReferralForAnalysisRepository(dbConnection, "ReferralForAnalysis", GetAnalysisRepository());
    }

    public IAnalysisRepository GetAnalysisRepository()
    {
        return analysisRepository ??= new AnalysisRepository(dbConnection, "Analysis");
    }

    public async void Dispose()
    {
        await dbConnection.CloseAsync();
    }
}