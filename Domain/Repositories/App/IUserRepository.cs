using Domain.Entities.App;
using Domain.Entities.Common;
using Domain.Entities.People.Attribute;
using Domain.Repositories.Common;
using Domain.Repositories.HumanAttribute;

namespace Domain.Repositories.App;


public interface IUserRepository<UserType> : IBaseRepository<UserType> where UserType : User
{
    public void ResetPassword(int id, string oldPassword, string newPassword)
    {
        var user = Read(id);
        if (user.Password != oldPassword)
        {
            throw UserRepositoryException.PasswordError(oldPassword, id);
        }
        user.Password = newPassword;
        Update(user);
    }
    public Type GetUserSessionType() => typeof(UserType);
    public void AddEducation(int id, Education education)
    {
        var user = Read(id);
        user.AddEducation(education);
        EducationRepository.Add(education);
        Update(user);
    }
    public void AddContact(int id, Contact contact)
    {
        var user = Read(id);
        user.AddContact(contact);
        ContactRepository.Add(contact);
        Update(user);
    }
    public void UpdateMedicalPolicy(int id, MedicalPolicy policy)
    {
        var user = Read(id);
        user.Policy = policy;
        MedicalPolicyRepository.Update(policy);
        Update(user);
    }

    IBenefitRepository BenefitRepository { get; }
    IContactRepository ContactRepository { get; }
    IEducationRepository EducationRepository { get; }
    IMedicalPolicyRepository MedicalPolicyRepository { get; }
    IAddressRepository AddressRepository { get; }

}

public class UserRepositoryException : Exception
{
    public UserRepositoryException(string message) : base(message) { }
    public static UserRepositoryException PasswordError(string oldPassword, int id)
    {
        return new UserRepositoryException($"User-password with id {id} dosn't match with {oldPassword}");
    }
}