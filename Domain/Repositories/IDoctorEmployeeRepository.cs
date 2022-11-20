
using Domain.Entities.People;
using Domain.Entities;
namespace Domain.Repositories;

public interface IDoctorEmployeeRepository
{
    void Update(DoctorEmployee newDoctorEmployee);
    void Delete(DoctorEmployee newDoctorEmployee);
    void Add(DoctorEmployee newDoctorEmployee);
    IEnumerable<DoctorEmployee> Read(DoctorEmployee newDoctorEmployee);

    IObservable<DoctorEmployee> ObserveByLogin(string login);
}

