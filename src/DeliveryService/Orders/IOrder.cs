namespace DeliveryService.Orders;

public interface IOrder
{
    int OrderId { get; }
    
    double Weight { get; }
    
    string District { get; }
    
    DateTime DeliveryTime { get; }
}