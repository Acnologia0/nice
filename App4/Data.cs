using App4;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage.Pickers;

namespace WindowsFormsApp1
{
    internal class Data
    {
        public static SalesManager salesmanager = new SalesManager();
        public static string productsPath = "";
        public static string customersPath = "";
        public static string ordersPath = "";
        public static string GetFilePath()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            // Retrieve the window handle (HWND) of the current WinUI 3 window.
            var window = WindowHelper.ActiveWindows.FirstOrDefault();
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Initialize the file picker with the window handle (HWND).
            WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

            // Set options for your file picker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.FileTypeFilter.Add("*");

            // Open the picker for the user to pick a file
            var file = openPicker.PickSingleFileAsync().GetAwaiter().GetResult();
            if (file != null)
            {
                return Path.Combine(file.Name, file.Path);
            }

            return "";
        }
        public static void Load()
        {           
            if (!string.IsNullOrEmpty(productsPath) &&
                !string.IsNullOrEmpty(customersPath) &&
                !string.IsNullOrEmpty(ordersPath))
            {
                LoadDataFromFile(productsPath, customersPath, ordersPath);
            }
        }
        public static void LoadDataFromFile(string productsPath, string customersPath, string ordersPath)
        {
            // Read products
            foreach (var line in File.ReadLines(productsPath))
            {
                var parts = line.Split('|');
                var product = new Product
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Category = parts[2],
                    Price = decimal.Parse(parts[3]),
                    Stock = int.Parse(parts[4]),
                    Image = parts[5]
                };
                salesmanager.AddProduct(product);
            }

            // Read customers
            foreach (var line in File.ReadLines(customersPath))
            {
                var parts = line.Split('|');
                var customer = new Customer
                {
                    CustomerId = int.Parse(parts[0]),
                    Name = parts[1],
                    Email = parts[2],
                    PhoneNumber = parts[3],
                    Image = parts[4],
                    Orders = new List<Order>()
                };
                salesmanager.AddCustomer(customer);
            }

            // Read orders
            foreach (var line in File.ReadLines(ordersPath))
            {
                var parts = line.Split('|');

                var customer = salesmanager.Customers.Find(c => c.CustomerId == int.Parse(parts[1]));
                var product = salesmanager.Products.Find(p => p.Id == int.Parse(parts[3]));

                var orderItem = new OrderItem
                {
                    OrderItemId = int.Parse(parts[3]),
                    Product = product,
                    Quantity = int.Parse(parts[4])
                };

                var order = new Order
                {
                    OrderId = int.Parse(parts[0]),
                    Customer = customer,
                    OrderDate = DateTime.Parse(parts[2]),
                    OrderItems = new List<OrderItem> { orderItem }
                };

                if (customer != null)
                    customer.Orders.Add(order);

                salesmanager.CreateOrder(order);
            }
        }

        public string ConvertProductsToCSV(List<Product> products)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Id|Name|Category|Price|Stock|Image");

            foreach (var product in products)
            {
                sb.AppendLine($"{product.Id},{product.Name},{product.Category},{product.Price},{product.Stock},{product.Image}");
            }

            return sb.ToString();
        }

        public string ConvertCustomersToCSV(List<Customer> customers)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CustomerId|Name|Email|PhoneNumber|Image");

            foreach (var customer in customers)
            {
                sb.AppendLine($"{customer.CustomerId},{customer.Name},{customer.Email},{customer.PhoneNumber},{customer.Image}");
            }

            return sb.ToString();
        }

        public string ConvertOrdersToCSV(List<Order> orders)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("OrderId|CustomerId|OrderDate|OrderItemId|ProductID|Quantity");

            foreach (var order in orders)
            {
                foreach (var item in order.OrderItems)
                {
                    sb.AppendLine($"{order.OrderId},{order.Customer.CustomerId},{order.OrderDate},{item.OrderItemId},{item.Product.Id},{item.Quantity}");
                }
            }

            return sb.ToString();
        }
        public void SaveDataToFiles(string productsPath, string customersPath, string ordersPath)
        {
            string productsData = ConvertProductsToCSV(salesmanager.Products);
            File.WriteAllText(productsPath, productsData);

            string customersData = ConvertCustomersToCSV(salesmanager.Customers);
            File.WriteAllText(customersPath, customersData);

            string ordersData = ConvertOrdersToCSV(salesmanager.Orders);
            File.WriteAllText(ordersPath, ordersData);
        }

    }
}
