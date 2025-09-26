namespace LT_B8_OOP
{
    public class SuperMarket
    {
        private static SuperMarket? instance;
        private static readonly object _lock = new object();
        private List<Product> products; // composition: supermarket chứa các sản phẩm

        private List<Customer> customers; // aggregation: supermarket có nhiều khách hàng

        private IProductFactory productFactory; // association: sử dụng factory để tạo sản phẩm

        public BehavioralTransaction behavioralTransaction { get; set; } // association
        private SuperMarket()
        {
            products = new List<Product>();
            customers = new List<Customer>();
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


        // Association
        public void CustomerPurchaseProduct(Customer customer, Product product)
        {
            behavioralTransaction.AddCustomerProductAssociation(customer, product);
        }

        // take(B obj)
        // public void ProcessOrder(Order order)
        // {
        //     Console.WriteLine($"Đang xử lý đơn hàng {order.OrderId} cho khách hàng {order.Customer.Name}");
        //     foreach (OrderDetail detail in order.OrderDetails)
        //     {
        //         Product product = detail.Product;
        //         if (product.Quantity >= detail.Quantity)
        //         {
        //             product.Quantity -= detail.Quantity;
        //             Console.WriteLine($"Đã bán {detail.Quantity} của {product.Name}");
        //         }
        //         else
        //         {
        //             Console.WriteLine($"Không đủ số lượng của {product.Name} để bán");
        //         }
        //     }
        //     Console.WriteLine($"Tổng tiền đơn hàng {order.OrderId}: {order.Total()}");
        // }

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