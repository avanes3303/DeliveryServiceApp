using System.Globalization;

namespace DeliveryService.Parsers
{
    public class Parser
    {
        public string InputFilePath { get; private set; }
        
        public string OutputFilePath { get; private set; }
        
        public string LogFilePath { get; private set; }
        
        public string District { get; private set; }
        
        public DateTime FirstDeliveryTime { get; private set; }

        public bool TryParse(string[] args)
        {
            if (args.Length != 10)
            {
                Console.WriteLine("Invalid number of arguments. Expected format: -i <input> -o <output> -l <logfile> -d <district> -t <datetime>");
                return false;
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                        InputFilePath = args[++i];
                        break;
                    case "-o":
                        OutputFilePath = args[++i];
                        break;
                    case "-l":
                        LogFilePath = args[++i];
                        break;
                    case "-d":
                        District = args[++i];
                        break;
                    case "-t":
                        if (!DateTime.TryParseExact(args[++i], "yyyy-MM-dd HH:mm:ss",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None,
                                out var parsedDateTime))
                        {
                            Console.WriteLine("Invalid date format. Use 'yyyy-MM-dd HH:mm:ss'");
                            return false;
                        }
                        FirstDeliveryTime = parsedDateTime;
                        break;
                    default:
                        Console.WriteLine($"Unknown argument {args[i]}");
                        return false;
                }
            }

            return true;
        }
    }
}
