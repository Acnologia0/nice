using System;
using System.Collections.Generic;
using System.Linq;

// Product, Order, OrderItem, Customer, SalesManager, and Statistics classes
// ...

public class Search
{
    //Tim san pham bang ten
    public List<Product> SearchProductsByName(List<Product> products, string searchTerm)
    {
        return products.Where(p => p.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    //Tim san pham bang loai
    public List<Product> SearchProductsByCategory(List<Product> products, string searchTerm)
    {
        return products.Where(p => p.Category.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    //Tim khach hang bang ten
    public List<Customer> SearchCustomersByName(List<Customer> customers, string searchTerm)
    {
        return customers.Where(c => c.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    //Tim khach hang bang email
    public List<Customer> SearchCustomersByEmail(List<Customer> customers, string searchTerm)
    {
        return customers.Where(c => c.Email.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    //Tim don hang bang ten khach hang
    public List<Order> SearchOrdersByCustomerName(List<Order> orders, string searchTerm)
    {
        return orders.Where(o => o.Customer.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
    }

    //Tim don hang bang san pham
    public List<Order> SearchOrdersByProductName(List<Order> orders, string searchTerm)
    {
        return orders.Where(o => o.OrderItems.Any(oi => oi.Product.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
    }

    //Tim don hang theo khoang thoi gian
    public List<Order> FilterOrdersByDateRange(List<Order> orders, DateTime startDate, DateTime endDate)
    {
        return orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
    }

    //Tim san pham theo khoang tien
    public List<Product> FilterProductsByPriceRange(List<Product> products, decimal minPrice, decimal maxPrice)
    {
        return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
    }

    //Tim don hang theo khach hang id
    public List<Order> FilterOrdersByCustomer(List<Order> orders, int customerId)
    {
        return orders.Where(o => o.Customer.CustomerId == customerId).ToList();
    }
}
