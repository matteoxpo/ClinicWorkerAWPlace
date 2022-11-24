using System;
using Domain.Entities;

namespace Data.Models;

public class RefForAnalysisDataStore
{
    public AnalysisDataStore AnalysisDataStore;

    public DateTime AnalysisTime;
    public RefForAnalysisDataStore(AnalysisDataStore analysisDataStore, DateTime analysisTime)
    {
        AnalysisDataStore = analysisDataStore;
        AnalysisTime = analysisTime;
    }


    public RefForAnalysis MapToRefForAnalyzis()
    {
        return new RefForAnalysis(AnalysisDataStore.MapToAnalyzis(), AnalysisTime);
    }

}