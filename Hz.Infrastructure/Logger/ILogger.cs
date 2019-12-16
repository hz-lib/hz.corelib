namespace Hz.Infrastructure.Logger
{
    public interface ILogger
    {
         void Error(string message);
         void Info(string message);
    }
}