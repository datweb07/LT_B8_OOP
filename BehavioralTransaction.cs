namespace LT_B8_OOP
{
    public class BehavioralTransaction
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        private Dictionary<Customer, List<Product>> CustomerProducts; // 1 customer có nhiều product

        public BehavioralTransaction()
        {
            Customers = new List<Customer>();
            Products = new List<Product>();
            CustomerProducts = new Dictionary<Customer, List<Product>>();
        }

        // association 
        public void AddCustomerProductAssociation(List<Customer> customers, List<Product> products)
        {
            foreach (Customer customer in customers)
            {
                if (!Customers.Contains(customer))
                    Customers.Add(customer);

                if (!CustomerProducts.ContainsKey(customer))
                    CustomerProducts[customer] = new List<Product>();

                foreach (Product product in products)
                {
                    if (!Products.Contains(product))
                        Products.Add(product);

                    CustomerProducts[customer].Add(product);
                }
            }
        }

        // Hiển thị khách hàng mua nhiều sản phẩm
        public void DisplayCustomerProducts(Customer customer)
        {
            if (CustomerProducts.ContainsKey(customer))
            {
                Console.WriteLine($"\nKhách hàng '{customer.Name}' đã mua:");
                foreach (Product product in CustomerProducts[customer])
                {
                    Console.WriteLine(product.Name);
                }
            }
        }

        // Hiển thị một sản phẩm được nhiều khách hàng mua
        public void DisplayProductCustomers(Product product)
        {
            Console.WriteLine($"\nSản phẩm '{product.Name}' được mua bởi:");
            foreach (KeyValuePair<Customer, List<Product>> entry in CustomerProducts)
            {
                if (entry.Value.Contains(product))
                {
                    Console.WriteLine($"- {entry.Key.Name}");
                }
            }
        }
    }
}