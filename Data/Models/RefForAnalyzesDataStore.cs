using System;

namespace Domain.Entities;

public class RefForAnalysisDataStore
{
    public AnalysisDataStore AnalysisDataStore;

    public DateTime AnalysisTime;
    public RefForAnalysisDataStore(AnalysisDataStore analysisDataStore, DateTime analysisTime)
    {
        AnalysisDataStore = analysisDataStore;
        AnalysisTime = analysisTime;
    }

}