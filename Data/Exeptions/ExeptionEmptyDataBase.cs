namespace Data.Exeptions;

internal class ExeptionEmptyDataBase : Exception
{
    public ExeptionEmptyDataBase()
    {
    }

    public ExeptionEmptyDataBase(string message)
        : base(message)
    {
    }

    public ExeptionEmptyDataBase(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}