using DeliveryService.Orders;
using DeliveryService.Results;

namespace DeliveryService.Writers;

public interface IWriter
{
    OperationResult WriteOrders(ICollection<IOrder> orders);
}