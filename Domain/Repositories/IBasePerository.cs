namespace Domain.Repositories;

public interface IBasePerository<T>
{
    void Update(T nextEntity);
    void Delete(T oldEntity);
    void Add(T newEntity);
    IEnumerable<T> Read();
    bool CompareEntities(T entity1, T entity2);
}