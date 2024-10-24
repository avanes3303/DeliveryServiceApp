using DeliveryService.Filters;
using DeliveryService.Loggers;
using DeliveryService.Orders;
using DeliveryService.Parsers;
using DeliveryService.Readers;
using DeliveryService.Results;
using DeliveryService.Writers;

class Program
{
    static void Main(string[] args)
    {
        var parser = new Parser();

        if (!parser.TryParse(args))
        {
            return;
        }

        IReader reader = new FileReader(parser.InputFilePath);
        IWriter writer = new FileWriter(parser.OutputFilePath);
        ILogger logger = new FileLogger(parser.LogFilePath);
        var filter = new Filter();

        ICollection<IOrder> orders = new List<IOrder>();
        var loadResult = reader.ReadOrders(out orders);
        
        if (loadResult is OperationResult.Success)
        {
            logger.Log("Orders loaded successfully.");
            var filterResult = filter.FilterOrders(orders,
                parser.District,
                parser.FirstDeliveryTime,
                out var filteredOrders);
            
            if (filterResult is OperationResult.Success)
            {
                writer.WriteOrders(filteredOrders);
                logger.Log($"Filtered orders written to {parser.OutputFilePath}.");
            }
            else
            {
                logger.Log("No orders found for the given criteria.");
            }
        }
        else
        {
            logger.Log("Failed to load orders.");
        }
    }
}