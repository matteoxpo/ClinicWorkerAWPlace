using System;

namespace Domain.Common;

public class RefForAnalysis
{
    public Analysis Analysis;

    public DateTime AnalysisTime;
    public RefForAnalysis(Analysis analysis, DateTime analysisTime)
    {
        Analysis = analysis;
        AnalysisTime = analysisTime;
    }

}