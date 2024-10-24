namespace DeliveryService.Orders;

public class Order : IOrder
{
    public int OrderId { get; }
    
    public double Weight { get; }
    
    public string District { get; }
    
    public DateTime DeliveryTime { get; }

    public Order(int orderId, double weight, string district, DateTime deliveryTime)
    {
        if (orderId <= 0)
        {
            throw new ArgumentException("Order id cannot be less than zero", nameof(orderId));
        }
        
        if (weight <= 0)
        {
            throw new ArgumentException("Weight cannot be less than zero", nameof(weight));
        }

        if (district is null)
        {
            throw new ArgumentNullException(nameof(district));
        }

        if (deliveryTime < DateTime.Now)
        {
            throw new ArgumentException("Delivery time cannot be less than current time", nameof(deliveryTime));
        }
        
        OrderId = orderId;
        Weight = weight;
        District = district;
        DeliveryTime = deliveryTime;
    }
}