namespace Domain.Repositories;

public interface IBaseRepository<T, ID> : IReadale<T, ID>, IUpdatable<T>, IDeletable<T>, IComparable<T> { }

public interface IReadale<T, Key>
{
    T? Read(Key key);
}

public interface IReadaleAll<T, Key> : IReadale<T, Key>
{
    ICollection<T>? ReadAll();
}

public interface IUpdatable<T>
{
    void Update(T nextEntity);
}

public interface IDeletable<T>
{
    void Delete(T oldEntity);
}

public interface IComparable<T>
{
    bool CompareEntities(T entity1, T entity2);
}

