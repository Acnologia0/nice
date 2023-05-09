using System;
using System.Collections.Generic;

public class Product
{
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string Category
    {
        get; set;
    }
    public decimal Price
    {
        get; set;
    }
    public int Stock
    {
        get; set;
    }
    //De o dang base 64
    public string Image
    {
        get; set;
    }
}

public class Order
{
    public int OrderId
    {
        get; set;
    }
    public Customer Customer
    {
        get; set;
    }
    public DateTime OrderDate
    {
        get; set;
    }
    public List<OrderItem> OrderItems
    {
        get; set;
    }

    public decimal TotalAmount
    {
        get
        {
            decimal totalAmount = 0;

            if (OrderItems != null)
            {
                foreach (var orderItem in OrderItems)
                {
                    totalAmount += orderItem.Product.Price * orderItem.Quantity;
                }
            }

            return totalAmount;
        }
    }
}

public class OrderItem
{
    public int OrderItemId
    {
        get; set;
    }
    public Product Product
    {
        get; set;
    }
    public int Quantity
    {
        get; set;
    }
}

public class Customer
{
    public int CustomerId
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string Email
    {
        get; set;
    }
    public string PhoneNumber
    {
        get; set;
    }
    public List<Order> Orders
    {
        get; set;
    }
    //De o dang base 64
    public string Image
    {
        get; set;
    }
}

public class SalesManager
{
    public List<Product> Products
    {
        get; set;
    }
    public List<Customer> Customers
    {
        get; set;
    }
    public List<Order> Orders
    {
        get; set;
    }

    public SalesManager()
    {
        Products = new List<Product>();
        Customers = new List<Customer>();
        Orders = new List<Order>();
    }

    // Them san pham
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    //Xoa san pham
    public void RemoveProduct(int productId)
    {
        Products.RemoveAll(p => p.Id == productId);
    }

    //Cap nhat san pham
    public void UpdateProduct(Product updatedProduct)
    {
        var index = Products.FindIndex(p => p.Id == updatedProduct.Id);

        if (index != -1)
        {
            Products[index] = updatedProduct;
        }
    }

    // Them khach hang
    public void AddCustomer(Customer customer)
    {
        Customers.Add(customer);
    }

    //Xoa khach hang
    public void RemoveCustomer(int customerId)
    {
        Customers.RemoveAll(c => c.CustomerId == customerId);
    }

    //Cap nhat khach hang
    public void UpdateCustomer(Customer updatedCustomer)
    {
        var index = Customers.FindIndex(c => c.CustomerId == updatedCustomer.CustomerId);

        if (index != -1)
        {
            Customers[index] = updatedCustomer;
        }
    }

    // Tao don hang
    public void CreateOrder(Order order)
    {
        Orders.Add(order);
    }

    //Huy don hang
    public void CancelOrder(int orderId)
    {
        Orders.RemoveAll(o => o.OrderId == orderId);
    }

    //Cap nhat don hang
    public void UpdateOrder(Order updatedOrder)
    {
        var index = Orders.FindIndex(o => o.OrderId == updatedOrder.OrderId);

        if (index != -1)
        {
            Orders[index] = updatedOrder;
        }
    }
}

public class Statistics
{
    public SalesManager SalesManager
    {
        get; set;
    }

    public Statistics(SalesManager salesManager)
    {
        SalesManager = salesManager;
    }

    //Tinh tong doanh thu tao ra tu cac don hang
    public decimal CalculateTotalRevenue()
    {
        decimal totalRevenue = 0;

        foreach (var order in SalesManager.Orders)
        {
            totalRevenue += order.TotalAmount;
        }

        return totalRevenue;
    }

    //Tinh gia tri don hang trung binh
    public decimal CalculateAverageOrderValue()
    {
        if (SalesManager.Orders.Count == 0)
        {
            return 0;
        }

        var totalRevenue = CalculateTotalRevenue();
        return totalRevenue / SalesManager.Orders.Count;
    }

    //Tong so luong san pham
    public Dictionary<Product, int> CalculateProductSales()
    {
        Dictionary<Product, int> productSales = new Dictionary<Product, int>();

        foreach (var order in SalesManager.Orders)
        {
            foreach (var orderItem in order.OrderItems)
            {
                if (productSales.ContainsKey(orderItem.Product))
                {
                    productSales[orderItem.Product] += orderItem.Quantity;
                }
                else
                {
                    productSales[orderItem.Product] = orderItem.Quantity;
                }
            }
        }

        return productSales;
    }

    //Danh sach san pham ban chay
    public List<Product> GetTopSellingProducts(int numberOfTopProducts)
    {
        Dictionary<Product, int> productSales = CalculateProductSales();
        List<Product> topSellingProducts = new List<Product>();

        for (var i = 0; i < numberOfTopProducts; i++)
        {
            Product topProduct = null;
            var maxSales = 0;

            foreach (var product in productSales.Keys)
            {
                if (!topSellingProducts.Contains(product) && productSales[product] > maxSales)
                {
                    topProduct = product;
                    maxSales = productSales[product];
                }
            }

            if (topProduct != null)
            {
                topSellingProducts.Add(topProduct);
            }
        }

        return topSellingProducts;
    }

    //Tong doanh thu tu 1 san pham
    public decimal CalculateRevenueForProduct(Product product)
    {
        decimal productRevenue = 0;

        foreach (var order in SalesManager.Orders)
        {
            foreach (var orderItem in order.OrderItems)
            {
                if (orderItem.Product == product)
                {
                    productRevenue += orderItem.Product.Price * orderItem.Quantity;
                }
            }
        }

        return productRevenue;
    }

    //Tong doanh thu cho moi muc san pham
    public Dictionary<string, decimal> CalculateRevenuePerCategory()
    {
        Dictionary<string, decimal> categoryRevenue = new Dictionary<string, decimal>();

        foreach (var order in SalesManager.Orders)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var category = orderItem.Product.Category;

                if (categoryRevenue.ContainsKey(category))
                {
                    categoryRevenue[category] += orderItem.Product.Price * orderItem.Quantity;
                }
                else
                {
                    categoryRevenue[category] = orderItem.Product.Price * orderItem.Quantity;
                }
            }
        }

        return categoryRevenue;
    }
}
