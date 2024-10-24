using DeliveryService.Orders;
using DeliveryService.Results;

namespace DeliveryService.Readers;

public interface IReader
{
    OperationResult ReadOrders(out ICollection<IOrder> orders);
}