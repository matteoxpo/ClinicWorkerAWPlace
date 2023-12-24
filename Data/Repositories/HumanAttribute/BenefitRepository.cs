using Domain.Entities.People.Attribute;
using Domain.Repositories.HumanAttribute;

namespace Data.Repositories.HumanAttribute;

public class BenefitRepository : BaseSQLiteRepository<Benefit>, IBenefitRepository
{
    public BenefitRepository(string connectionString, string tableName) : base(connectionString, tableName)
    {
    }

    public override async Task<Benefit> ReadAsync(int id)
    {
        return new Benefit(
            await ReadPremitiveAsync<string>("Type", id),
            await ReadPremitiveAsync<string>("Description", id),
            await ReadPremitiveAsync<double>("Discount", id),
            await ReadPremitiveAsync<int>("RetirementAge", id),
            id
        );
    }
}