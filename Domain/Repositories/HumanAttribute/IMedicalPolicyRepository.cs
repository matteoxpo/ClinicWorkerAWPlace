using Domain.Entities.People.Attribute;

namespace Domain.Repositories.HumanAttribute;

public interface IMedicalPolicyRepository : IReadaleAll<MedicalPolicy>, IUpdatable<MedicalPolicy>, IAddable<MedicalPolicy> { }