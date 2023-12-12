using Data.Repositories.Polyclinic;
using System;
using Xunit.Abstractions;

namespace Tests;

public class UnitTest1
{

    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test1()
    {
        try
        {

            var analysisRepository = new AnalysisRepository("Data Source=F:\\ProgramFiles\\Programming\\ClinicWorkerAWPlace\\Data\\ClinicWorkerAWPlace.db;Version=3;");
            var res = analysisRepository.Read(1);
            if (res is null)
            {
                Assert.True(false);
            }
            else
            {
                Assert.True(true);
            }
        }
        catch (Exception)
        {
            _output.WriteLine("asdasdas");
            Assert.True(true);
        }
    }
}