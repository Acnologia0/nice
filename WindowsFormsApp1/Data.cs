using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Data
    {
        public static string productsPath = "";
        public static string customersPath = "";
        public static string ordersPath = "";
        public static string GetFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    return openFileDialog.FileName;
                }
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
            else
            {
                MessageBox.Show("Please select all files");
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
                Program.salesManager.AddProduct(product);
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
                Program.salesManager.AddCustomer(customer);
            }

            // Read orders
            foreach (var line in File.ReadLines(ordersPath))
            {
                var parts = line.Split('|');

                var customer = Program.salesManager.Customers.Find(c => c.CustomerId == int.Parse(parts[1]));
                var product = Program.salesManager.Products.Find(p => p.Id == int.Parse(parts[3]));

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

                Program.salesManager.CreateOrder(order);
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
            string productsData = ConvertProductsToCSV(Program.salesManager.Products);
            File.WriteAllText(productsPath, productsData);

            string customersData = ConvertCustomersToCSV(Program.salesManager.Customers);
            File.WriteAllText(customersPath, customersData);

            string ordersData = ConvertOrdersToCSV(Program.salesManager.Orders);
            File.WriteAllText(ordersPath, ordersData);
        }

    }
}
