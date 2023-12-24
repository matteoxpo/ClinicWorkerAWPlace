using Domain.Entities.App.Role;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Entities.Polyclinic.Analysis;
using Domain.Entities.Polyclinic.Appointment;
using Domain.Repositories.App;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.App.Role;

public class ClientRepository //: IUserRepository<Client>
{
    public IBenefitRepository BenefitRepository { get; }

    public IContactRepository ContactRepository { get; }

    public IEducationRepository EducationRepository { get; }

    public IMedicalPolicyRepository MedicalPolicyRepository { get; }

    public void Add(Client entity)
    {
        throw new NotImplementedException();
    }

    public void Add(IEnumerable<Client> entities)
    {
        throw new NotImplementedException();
    }

    public void AddContact(string login, string password, Contact education)
    {
        throw new NotImplementedException();
    }

    public void AddEducation(string login, string password, Education education)
    {
        throw new NotImplementedException();
    }

    public void AddMedicalPolicy(string login, string password, MedicalPolicy policy)
    {
        throw new NotImplementedException();
    }

    public bool CompareEntities(Client entity1, Client entity2)
    {
        throw new NotImplementedException();
    }

    public void Delete(string login, string password)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id, string password)
    {
        throw new NotImplementedException();
    }

    public void Delete(Client oldEntity)
    {
        throw new NotImplementedException();
    }

    public void Delete(ICollection<Client> oldEntities)
    {
        throw new NotImplementedException();
    }

    public Client Read(string login, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Client?> ReadAsync(int key)
    {
        throw new NotImplementedException();
    }

    public void ResetPassword(string loging, string password, string newPassword)
    {
        throw new NotImplementedException();
    }

    public void ResetPassword(int id, string password, string newPassword)
    {
        throw new NotImplementedException();
    }

    public void Update(Client nextEntity)
    {
        throw new NotImplementedException();
    }

    public void Update(ICollection<Client> nextEntities)
    {
        throw new NotImplementedException();
    }
}