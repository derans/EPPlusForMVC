namespace ExportToExcelSample.Core.Infrastructure
{
    public interface IParameterLessQuery<T>
    {
        T Execute();
    }
}