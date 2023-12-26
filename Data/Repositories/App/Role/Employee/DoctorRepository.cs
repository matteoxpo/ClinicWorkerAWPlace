using System.Data.SQLite;
using Domain.Entities.App.Role;
using Domain.Entities.App.Role.Employees;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.App.Role.Employee;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;
using Domain.Repositories.Polyclinic;

namespace Data.Repositories.App.Role.Employee;

public class DoctorRepository : BaseSQLiteRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(SQLiteConnection dbConnection,
                            string tableName,
                            IBenefitRepository benefitRepository,
                            IEducationRepository educationRepository,
                            IMedicalPolicyRepository medicalPolicyRepository,
                            IContactRepository contactRepository,
                            IAppoinmentRepository appoinmentRepository) : base(dbConnection, tableName)
    {
        BenefitRepository = benefitRepository;
        EducationRepository = educationRepository;
        MedicalPolicyRepository = medicalPolicyRepository;
        ContactRepository = contactRepository;
        AppoinmentRepository = appoinmentRepository;
    }

    public IBenefitRepository BenefitRepository { get; }
    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }


    public IContactRepository ContactRepository { get; }

    public IAppoinmentRepository AppoinmentRepository { get; }

    public IAddressRepository AddressRepository { get; }

    public Task AddAsync(Doctor entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Doctor oldEntity)
    {
        throw new NotImplementedException();
    }

    public override Task<Doctor> ReadAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Doctor nextEntity)
    {
        throw new NotImplementedException();
    }

    public class DoctorRepositoryException : Exception
    {
        public DoctorRepositoryException(string message) : base(message) { }
    }
}