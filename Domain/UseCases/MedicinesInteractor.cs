using Domain.Entities;
using Domain.Entities.Role.Doctor;
using Domain.Repositories;

namespace Domain.UseCases;

public class MedicinesInteractor
{
    private readonly IMedicinesRepository _medicinesRepository;

    public MedicinesInteractor(IMedicinesRepository medicinesRepository)
    {
        _medicinesRepository = medicinesRepository;
    }

    public IEnumerable<Medicines> Get()
    {
        return _medicinesRepository.Read();
    }
}