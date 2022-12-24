using Domain.Entities;
using Domain.Repositories;

namespace Domain.UseCases;

public class ReferenceForAnalysisInteractor
{
    private readonly IReferenceForAnalysisRepository _referenceForAnalysisRepository;

    public ReferenceForAnalysisInteractor(IReferenceForAnalysisRepository referenceForAnalysisRepository)
    {
        _referenceForAnalysisRepository = referenceForAnalysisRepository;
    }

    public IEnumerable<ReferenceForAnalysis> Read()
    {
        return _referenceForAnalysisRepository.Read();
    }

    public IEnumerable<ReferenceForAnalysis> Read(string analysisId)
    {
        return _referenceForAnalysisRepository.Read().Where(analysis => analysis.Analysis.Id.Equals(analysisId))
            .ToList();
    }

    public void Add(ReferenceForAnalysis referenceForAnalysis)
    {
        if (Read().Any(analysis => analysis.Analysis.Equals(referenceForAnalysis.Analysis) &&
                                   analysis.AnalysisTime.Equals(referenceForAnalysis.AnalysisTime)))
            throw new ReferenceForAnalysisInteractorException("В это время уже сдают анализы");
        _referenceForAnalysisRepository.Add(referenceForAnalysis);
    }
}

public class ReferenceForAnalysisInteractorException : Exception
{
    public ReferenceForAnalysisInteractorException(string message) : base(message)
    {
    }
}