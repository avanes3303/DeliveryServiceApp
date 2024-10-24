namespace DeliveryService.Loggers;


public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath)
    {
        _filePath = filePath;
    }

    public void Log(string message)
    {
        try
        {
            using (var writer = new StreamWriter(_filePath))
            {
                writer.WriteLine($"{message} - {DateTime.Now}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log message to file: {ex.Message}");
        }
    }
}
