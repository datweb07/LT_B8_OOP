namespace LT_B8_OOP
{
    public class BehavioralTransaction
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        private Dictionary<Customer, List<Product>> CustomerProducts; 
        private Dictionary<Product, List<Customer>> ProductCustomers;

        public BehavioralTransaction()
        {
            Customers = new List<Customer>();
            Products = new List<Product>();
            CustomerProducts = new Dictionary<Customer, List<Product>>();
            ProductCustomers = new Dictionary<Product, List<Customer>>();
        }

        // association 
        public void AddCustomerProductAssociation(List<Customer> customers, List<Product> products)
        {
            foreach (Customer customer in customers)
            {
                // tạo list products cho customer 
                if (!CustomerProducts.ContainsKey(customer))
                    CustomerProducts[customer] = new List<Product>();

                foreach (Product product in products)
                {
                    if (!Products.Contains(product))
                        Products.Add(product);

                    // tạo list customers cho product 
                    if (!ProductCustomers.ContainsKey(product))
                        ProductCustomers[product] = new List<Customer>();

                    // customer x roduct
                    if (!CustomerProducts[customer].Contains(product))
                        CustomerProducts[customer].Add(product);

                    // product x customer
                    if (!ProductCustomers[product].Contains(customer))
                        ProductCustomers[product].Add(customer);
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