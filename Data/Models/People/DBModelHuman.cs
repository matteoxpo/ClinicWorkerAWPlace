using Data.Models.People.Attribute;

using Data.Models.Common;
using Domain.Entities.People;
using Domain.Entities.People.Attribute;
// using Domain.Entities.People.Attribute;

namespace Data.Models.People;

public class DBModelHuman : IDBConverter<Human, DBModelHuman>
{
    public Human ConvertFromStorageEntity(DBModelHuman entity)
    {
        // return new Human(entity.Name, entity.Surname, entity.PatronymicName, entity.)
        throw new NotImplementedException();
    }

    public ICollection<Human> ConvertFromStorageEntity(IEnumerable<DBModelHuman> entities)
    {
        throw new NotImplementedException();
    }

    public DBModelHuman ConvertFromEntity(Human entity)
    {
        throw new NotImplementedException();
    }

    public ICollection<DBModelHuman> ConvertFromEntity(IEnumerable<Human> entities)
    {
        throw new NotImplementedException();
    }

    public Human ConvertFromEntity()
    {
        throw new NotImplementedException();
    }

    public DBModelHuman(string name,
                        string surname,
                        string patronymicName,
                        DBModelAddress address,
                        string dateOfBirth,
                        Sex sex,
                        uint id,
                        DBModelMedicalPolicy policy,
                        ICollection<DBModelContact>? contacts,
                        ICollection<DBModelEducation>? education,
                        ICollection<DBModelBenefit>? benefits)
    {
        DateOfBirth = DateTime.Parse(dateOfBirth);

        PatronymicName = new string(patronymicName);
        Name = new string(name);
        Surname = new string(surname);
        Sex = sex;
        Address = address;
        ID = id;
        Policy = policy;

        Contact = contacts ?? new List<DBModelContact>();
        Education = education ?? new List<DBModelEducation>();
        Benefits = benefits ?? new List<DBModelBenefit>();
    }

    public uint ID { get; set; }
    public ICollection<DBModelContact> Contact { get; set; }
    public ICollection<DBModelEducation> Education { get; set; }
    public ICollection<DBModelBenefit>? Benefits { get; set; }

    public DBModelMedicalPolicy Policy { get; set; }
    public Domain.Entities.People.Attribute.Sex Sex { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string PatronymicName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DBModelAddress Address { get; set; }


}