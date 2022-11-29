
using Domain.Entities.People;
using Domain.Entities;
namespace Domain.Repositories;

public interface IDoctorEmployeeRepository
{
    void Update(DoctorEmployee newDoctorEmployee);
    void Delete(DoctorEmployee oldDoctorEmployee);
    void Add(DoctorEmployee newDoctorEmployee);
    List<DoctorEmployee> Read();
    IObservable<DoctorEmployee> ObserveByLogin(string login);
}

