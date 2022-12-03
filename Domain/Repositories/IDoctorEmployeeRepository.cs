
using Domain.Entities.People;

namespace Domain.Repositories;

public interface IDoctorEmployeeRepository
{
    void Update(DoctorEmployee newDoctorEmployee);
    void Delete(DoctorEmployee oldDoctorEmployee);
    void Add(DoctorEmployee newDoctorEmployee);
    IEnumerable<DoctorEmployee> Read();
    IObservable<DoctorEmployee> ObserveByLogin(string login);
}

