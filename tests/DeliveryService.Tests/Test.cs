using DeliveryService.Filters;
using DeliveryService.Orders;
using DeliveryService.Readers;
using DeliveryService.Results;
using DeliveryService.Writers;

namespace DeliveryService.Tests;

public class Tests
{
    [Fact]
    public void ValidOrdersFile_Success()
    {
        string inputFile = "valid_orders.txt";
        var reader = new FileReader(inputFile);
        ICollection<IOrder> orders = new List<IOrder>();
        
        string[] orderData = new[]
        {
            "1,2.5,Primorskiy,2024-10-25 14:00:00",
            "2,5,Kupchino,2024-10-25 15:00:00",
            "3,10.0,Parnas,2024-10-25 16:00:00"
        };

        File.WriteAllLines(inputFile, orderData);

        var result = reader.ReadOrders(out orders);

        Assert.IsType<OperationResult.Success>(result);
        Assert.NotEmpty(orders);

        if (File.Exists(inputFile))
        {
            File.Delete(inputFile);
        }
    }
    
    [Fact]
    public void InvalidOrdersFile_InvalidArgument()
    {
        string inputFile = "invalid_orders.txt";
        var reader = new FileReader(inputFile);
        ICollection<IOrder> orders = new List<IOrder>();
        
        string[] orderData = new[]
        {
            "1  2.5  Primorskiy 2024-10-25",
            "2,5 Kupchino 2024-10-25 15:00:00",
            "3 10,0 Parnas,2024-10-25 16:00:00"
        };

        File.WriteAllLines(inputFile, orderData);

        var result = reader.ReadOrders(out orders);

        Assert.IsType<OperationResult.InvalidData>(result);

        if (File.Exists(inputFile))
        {
            File.Delete(inputFile);
        }
    }

    [Fact]
    public void ValidDistrictAndTime_Success()
    {
        var orders = new List<IOrder>
        {
            new Order(1,2.5, "Primorskiy",DateTime.Parse("2024-10-25 14:00:00")),
            new Order(2,5.0, "Kupchino",DateTime.Parse("2024-10-25 14:30:00")),
            new Order(3,10, "Primorskiy",DateTime.Parse("2024-10-25 15:29:00"))
        };
        var filter = new Filter();
        string district = "Primorskiy";
        DateTime firstDeliveryTime = DateTime.Parse("2024-10-25 15:00:00");

        var result = filter.FilterOrders(orders, district, firstDeliveryTime, out var filteredOrders);

        Assert.IsType<OperationResult.Success>(result);
        Assert.NotEmpty(filteredOrders);
        Assert.Equal(1, filteredOrders.Count);
    }

    [Fact]
    public void NoMatchingOrders_NotFound()
    {
        var orders = new List<IOrder>
        {
            new Order(1,2.5, "Primorskiy",DateTime.Parse("2024-10-25 15:01:00")),
            new Order(2,5.0, "Kupchino",DateTime.Parse("2024-10-25 14:30:00")),
            new Order(3,10, "Parnas",DateTime.Parse("2024-10-25 15:30:00"))
        };
        var filter = new Filter();
        string district = "Petrogradskiy";
        DateTime firstDeliveryTime = DateTime.Parse("2024-10-25 16:00:00");

        var result = filter.FilterOrders(orders, district, firstDeliveryTime, out var filteredOrders);

        Assert.IsType<OperationResult.NotFound>(result);
        Assert.Empty(filteredOrders);
    }
    
    [Fact]
    public void InvalidOutputPath_WritingFailure()
    {
        var filePath = "test_orders.txt";
        var writer = new FileWriter(filePath);
        var orders = new List<IOrder>();

        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            var result = writer.WriteOrders(orders);

            Assert.IsType<OperationResult.WritingFailure>(result);
        }
    }
}

