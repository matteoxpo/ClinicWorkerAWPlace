
namespace Domain.Common;

public class Analysis
{
    public string Title;
    public uint TimeForPrepearing;


    public Analysis(string title, uint timeForPrepearing)
    {
        Title = title;
        TimeForPrepearing = timeForPrepearing;
    }

}