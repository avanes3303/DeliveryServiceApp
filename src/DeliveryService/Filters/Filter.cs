using DeliveryService.Orders;
using DeliveryService.Readers;
using DeliveryService.Results;
using DeliveryService.Writers;

namespace DeliveryService.Filters;

public class Filter
{
    public OperationResult FilterOrders(ICollection<IOrder> orders,
        string district,
        DateTime firstDeliveryTime,
        out ICollection<IOrder> filteredOrders)
    {
        filteredOrders = new List<IOrder>();

        foreach (var order in orders)
        {
            if (order.District.Equals(district, StringComparison.OrdinalIgnoreCase) &&
                order.DeliveryTime >= firstDeliveryTime &&
                order.DeliveryTime < firstDeliveryTime.AddMinutes(30))
            {
                filteredOrders.Add(order);
            }
        }

        return filteredOrders.Any()
            ? new OperationResult.Success()
            : new OperationResult.NotFound();
    }
}