namespace LT_B8_OOP
{
    public class SuperMarket
    {
        private static SuperMarket? instance;
        private static readonly object _lock = new object();
        private List<Product> products; // composition: supermarket chứa các sản phẩm

        private List<Customer> customers; // aggregation: supermarket có nhiều khách hàng

        private List<Order> orders; // manage orders centrally to allow single-parameter removal

        private IProductFactory productFactory; // association: sử dụng factory để tạo sản phẩm

        public BehavioralTransaction behavioralTransaction { get; set; } // association
        private SuperMarket()
        {
            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();
            productFactory = new ProductFactory();
            behavioralTransaction = new BehavioralTransaction();
        }

        private static readonly object padlock = new object();
        public static SuperMarket GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SuperMarket();
                    }
                    return instance;
                }
            }
        }

        // factory
        public void AddProduct(string type, string productId, string name, string origin, decimal price, int quantity, params object[] parameters)
        {
            Product product = productFactory.CreateProduct(type, productId, name, origin, price, quantity, parameters);
            products.Add(product);
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            customers.Remove(customer);
        }

        // Manage orders centrally: delete by order only, and cascade to its details
        public void RemoveOrder(Order order)
        {
            // cascade delete order details (composition)
            order.Delete();
            orders.Remove(order);

            // // remove from supermarket's order registry if present
            // if (orders.Contains(order))
            // {
            //     orders.Remove(order);
            // }

            // // also remove from the owning customer, if any
            // Customer? owner = null;
            // foreach (Customer c in customers)
            // {
            //     if (c.orders.Contains(order))
            //     {
            //         owner = c;
            //         c.orders.Remove(order);
            //         break;
            //     }
            // }

            // if (owner != null)
            // {
            //     Console.WriteLine($"Đã xóa đơn hàng {order.OrderId} của khách hàng {owner.Name}");
            // }
            // else
            // {
            //     Console.WriteLine($"Đã xóa đơn hàng {order.OrderId}");
            // }
        }


        // Association
        public void CustomerPurchaseProduct(Customer customer, Product product)
        {
            behavioralTransaction.AddCustomerProductAssociation(customer, product);
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Danh sách sản phẩm trong siêu thị:");
            foreach (Product product in products)
            {
                product.DisplayInfo();
            }
        }

        public void DisplayCustomers()
        {
            Console.WriteLine("Danh sách khách hàng:");
            foreach (Customer customer in customers)
            {
                customer.DisplayInfo();
            }
        }

        public Product FindProductById(string productId)
        {
            foreach (Product product in products)
            {
                if (product.ProductId == productId)
                {
                    return product;
                }
            }
            throw new InvalidOperationException("Product not found.");
        }

        public Customer FindCustomerById(string customerId)
        {
            foreach (Customer customer in customers)
            {
                if (customer.CustomerId == customerId)
                {
                    return customer;
                }
            }
            throw new InvalidOperationException("Customer not found.");
        }
    }
}