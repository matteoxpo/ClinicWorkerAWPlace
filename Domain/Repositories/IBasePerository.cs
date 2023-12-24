namespace Domain.Repositories;

public interface IBaseRepository<T> : IReadale<T>, IUpdatable<T>, IDeletable<T>, IAddable<T> { }

public interface IReadale<T>
{
    Task<T> ReadAsync(int id);
    async Task<ICollection<T>> ReadAsync(ICollection<int> ids)
    {
        var resList = new List<T>();
        foreach (var id in ids)
        {
            resList.Add(await ReadAsync(id));
        }
        return resList;
    }
    T Read(int id)
    {
        return ReadAsync(id).GetAwaiter().GetResult() ?? throw ReadDataException.CantFindDataById<T>(id);
    }

}

public class ReadDataException : Exception
{
    public ReadDataException(string message) : base(message) { }
    public static ReadDataException CantFindDataById<T>(int id)
    {
        return new ReadDataException($"Cant find Data {nameof(T)} with id {id}");
    }
}

public interface IAddable<T>
{
    Task AddAsync(T entity);
    async Task AddAsync(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity);
        }
    }
    void Add(T entity)
    {
        AddAsync(entity).GetAwaiter().GetResult();
    }
    void Add(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            Add(entity);
        }
    }
}

public interface IReadaleAll<T> : IReadale<T>
{
    ICollection<T> ReadAll()
    {
        return ReadAllAsync().GetAwaiter().GetResult();
    }
    Task<ICollection<T>> ReadAllAsync();
}

public interface IUpdatable<T>
{
    Task UpdateAsync(T nextEntity);
    async Task UpdateAsync(ICollection<T> nextEntities)
    {
        foreach (var entity in nextEntities)
        {
            await UpdateAsync(entity);
        }
    }
    void Update(T nextEntity)
    {
        UpdateAsync(nextEntity).GetAwaiter().GetResult();
    }
    void Update(ICollection<T> nextEntities)
    {
        foreach (var entity in nextEntities)
        {
            Update(entity);
        }
    }
}

public interface IDeletable<T>
{
    Task DeleteAsync(T oldEntity);
    async Task DeleteAsync(ICollection<T> oldEntities)
    {
        foreach (var entity in oldEntities)
        {
            await DeleteAsync(entity);
        }
    }
    void Delete(T oldEntity)
    {
        DeleteAsync(oldEntity).GetAwaiter().GetResult();
    }

    void Delete(ICollection<T> oldEntities)
    {
        foreach (var entity in oldEntities)
        {
            Delete(entity);
        }
    }
}
