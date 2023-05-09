using System;
using System.Collections.Generic;

// Product, Order, OrderItem, Customer, SalesManager, and Statistics classes
// ...

public class Sort
{
    //Sap xep san pham theo ten
    public void SortProductsByName(List<Product> products, bool ascending = true)
    {
        products.Sort((p1, p2) => ascending ? string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase) : string.Compare(p2.Name, p1.Name, StringComparison.OrdinalIgnoreCase));
    }

    //Sap xep san pham theo tien
    public void SortProductsByPrice(List<Product> products, bool ascending = true)
    {
        products.Sort((p1, p2) => ascending ? p1.Price.CompareTo(p2.Price) : p2.Price.CompareTo(p1.Price));
    }

    //Sap xep khach hang theo ten
    public void SortCustomersByName(List<Customer> customers, bool ascending = true)
    {
        customers.Sort((c1, c2) => ascending ? string.Compare(c1.Name, c2.Name, StringComparison.OrdinalIgnoreCase) : string.Compare(c2.Name, c1.Name, StringComparison.OrdinalIgnoreCase));
    }

    //Sap xep khach hang theo email
    public void SortCustomersByEmail(List<Customer> customers, bool ascending = true)
    {
        customers.Sort((c1, c2) => ascending ? string.Compare(c1.Email, c2.Email, StringComparison.OrdinalIgnoreCase) : string.Compare(c2.Email, c1.Email, StringComparison.OrdinalIgnoreCase));
    }

    //Sap xep don hang theo ngay
    public void SortOrdersByDate(List<Order> orders, bool ascending = true)
    {
        orders.Sort((o1, o2) => ascending ? o1.OrderDate.CompareTo(o2.OrderDate) : o2.OrderDate.CompareTo(o1.OrderDate));
    }


    public void SortOrdersByTotalAmount(List<Order> orders, bool ascending = true)
    {
        orders.Sort((o1, o2) => ascending ? o1.TotalAmount.CompareTo(o2.TotalAmount) : o2.TotalAmount.CompareTo(o1.TotalAmount));
    }
}
