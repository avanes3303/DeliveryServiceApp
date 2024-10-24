using DeliveryService.Orders;
using DeliveryService.Results;

namespace DeliveryService.Writers
{
    public class FileWriter : IWriter
    {
        private readonly string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }
        
        public OperationResult WriteOrders(ICollection<IOrder> orders)
        {
            if (!File.Exists(_filePath))
            {
                return new OperationResult.InvalidData();
            }
            try
            {
                using (var writer = new StreamWriter(_filePath))
                {
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.OrderId},{order.Weight},{order.District},{order.DeliveryTime}");
                    }
                }
                return new OperationResult.Success();
            }
            catch (Exception ex)
            {
                return new OperationResult.WritingFailure($"Error writing to file: {ex.Message}");
            }
        }
    }
}
