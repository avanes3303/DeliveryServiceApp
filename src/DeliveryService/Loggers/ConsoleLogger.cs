namespace DeliveryService.Loggers;

using System;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"{message} - {DateTime.Now}");
    }
}
