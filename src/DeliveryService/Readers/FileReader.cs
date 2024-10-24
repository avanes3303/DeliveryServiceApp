using System.Globalization;
using DeliveryService.Orders;
using DeliveryService.Results;

namespace DeliveryService.Readers;

public class FileReader : IReader
{
    private readonly string _filePath;

    public FileReader(string filePath)
    {
        _filePath = filePath;
    }
    public OperationResult ReadOrders(out ICollection<IOrder> orders)
    {
        orders = new List<IOrder>();
        if (!File.Exists(_filePath))
        {
            return new OperationResult.InvalidData();
        }
        
        foreach (var line in File.ReadAllLines(_filePath))
        {
            var parts = line.Split(',');
            if (parts.Length == 4 && 
                int.TryParse(parts[0], out var id) &&
                double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var weight) &&
                DateTime.TryParse(parts[3], out var deliveryTime))
            {
                orders.Add(new Order(id, weight, parts[2], deliveryTime));
            }

            else
            {
                return new OperationResult.InvalidData();
            }
        }
        return new OperationResult.Success();
    }
}