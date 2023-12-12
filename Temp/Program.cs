using Data.Repositories;
internal class Program
{
    private static void Main(string[] args)
    {
        try
        {

            var analysisRepository = new AnalysisRepository("Data Source=F:\\ProgramFiles\\Programming\\ClinicWorkerAWPlace\\Data\\ClinicWorkerAWPlace.db;Version=3;");
            var res = analysisRepository.ReadAll();
            foreach (var an in res)
            {
                Console.WriteLine("dasd");
            }
            Console.WriteLine("Hello, World!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}