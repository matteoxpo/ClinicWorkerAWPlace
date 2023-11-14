using System.Globalization;
using System.Reactive;
using Domain.Entities;
using Domain.Entities.Role.Doctor;
using ReactiveUI;

namespace Presentation.ViewModels.WorkPlace.Default;

public class AnalysisResultViewModel : ViewModelBase
{
    // public ReferenceForAnalysis ReferenceForAnalysis { get; }

    public string AnalysisResult { get; }
    public string AnalysisName { get; }
    public string AnalysisTakingTime { get; }
    public string AnalysisPrepearingTime { get; }

    public AnalysisResultViewModel(ReferenceForAnalysis referenceForAnalysis)
    {
        // ReferenceForAnalysis = referenceForAnalysis;
        AnalysisName = referenceForAnalysis.Analysis.Title;
        AnalysisTakingTime = referenceForAnalysis.AnalysisTime.ToString(CultureInfo.InvariantCulture);
        AnalysisResult = referenceForAnalysis.Result ?? "Результата анализа еще нет";
        AnalysisPrepearingTime = referenceForAnalysis.Analysis.TimeForPrepearing.ToString();
    }
}