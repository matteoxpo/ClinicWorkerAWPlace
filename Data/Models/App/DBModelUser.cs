

using Data.Models.Common;
using Data.Models.People;
using Data.Models.People.Attribute;
using Domain.Entities.People.Attribute;


namespace Data.Models.App;

public class DBModelUser : DBModelHuman
{
    public byte[]? Photo { get; set; }

    public string Password { get; set; }
    public string Login { get; set; }

    public DBModelUser(string login,
                string password,
                string name,
                string surname,
                string patronymicName,
                DBModelAddress address,
                DateTime dateOfBirth,
                Sex sex,
                uint id,
                DBModelMedicalPolicy policy,
                ICollection<DBModelContact> contacts,
                ICollection<DBModelEducation>? education,
                ICollection<DBModelBenefit>? benefits) : base(name, surname, patronymicName, address, dateOfBirth, sex, id, policy, contacts, education, benefits)
    {
        Login = login ?? throw new NullReferenceException("Login is null");
        Password = password ?? throw new NullReferenceException("Password is null");
    }


}