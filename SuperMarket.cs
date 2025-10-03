namespace LT_B8_OOP
{
    public class SuperMarket
    {
        private static SuperMarket? instance;
        private List<Product> products; // composition: supermarket chứa các sản phẩm

        private List<Customer> customers; // aggregation: supermarket có nhiều khách hàng

        private List<Order> orders; // manage orders centrally to allow single-parameter removal

        private IProductFactory productFactory; // association: sử dụng factory để tạo sản phẩm

        public BehavioralTransaction behavioralTransaction { get; set; } // association
        public Warehouse Warehouse { get; private set; }
        private SuperMarket()
        {
            products = new List<Product>();
            customers = new List<Customer>();
            orders = new List<Order>();
            productFactory = new ProductFactory();
            behavioralTransaction = new BehavioralTransaction();
            Warehouse = new Warehouse();
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
            // nhập kho số lượng ban đầu
            Warehouse.Import(product, quantity);
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }


        public void RemoveOrder(Order order)
        {
            orders.Remove(order);
            // trả hàng: cộng lại tồn kho cho từng sản phẩm trong OrderDetails
            foreach (OrderDetail detail in order.OrderDetails)
            {
                // hoàn trả về kho và đồng bộ số lượng hiển thị
                Warehouse.Return(detail.Product, detail.Quantity);
                detail.Product.Quantity += detail.Quantity;
            }
            order.OrderDetails.Clear();

            // also detach from any customer lists
            foreach (Customer c in customers)
            {
                c.orders.Remove(order);
            }
        }


        // Association 
        public void CustomersPurchaseProducts(List<Customer> customers, List<Product> products)
        {
            behavioralTransaction.AddCustomerProductAssociation(customers, products);
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
            throw new InvalidOperationException("Không tìm thấy sản phẩm.");
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
            throw new InvalidOperationException("Không tìm thấy khách hàng.");
        }

        public void RemoveCustomer(Customer customer)
        {
            // Aggregation: khi xóa customer, các order của họ vẫn tồn tại
            // Chỉ xóa customer khỏi danh sách, không xóa các order
            customers.Remove(customer);
            Console.WriteLine($"Đã xóa khách hàng {customer.Name} khỏi danh sách. Các đơn hàng của khách hàng vẫn được giữ lại.");
        }

        // Aggregation Customer ↔ Order
        public Order CreateOrder(string orderId, DateTime orderDate)
        {
            Order order = new Order(orderId, orderDate);
            orders.Add(order);
            return order;
        }

        // Thêm sản phẩm vào order qua kho: xuất kho nếu đủ hàng, sau đó thêm chi tiết
        public bool AddProductToOrder(Order order, Product product, int quantity)
        {
            if (Warehouse.Export(product, quantity))
            {
                OrderDetail orderDetail = new OrderDetail(product, quantity);
                order.OrderDetails.Add(orderDetail);
                // đồng bộ quantity hiển thị của sản phẩm trong danh sách cửa hàng
                product.Quantity -= quantity;
                Console.WriteLine($"Đã thêm {quantity} của {product.Name} vào đơn hàng {order.OrderId} (xuất kho thành công)");
                return true;
            }
            Console.WriteLine($"Không đủ số lượng của {product.Name} trong kho để thêm vào đơn hàng {order.OrderId}");
            return false;
        }

        public void AttachOrderToCustomer(Customer customer, Order order)
        {
            if (!orders.Contains(order))
            {
                orders.Add(order);
            }
            if (!customer.orders.Contains(order))
            {
                customer.orders.Add(order);
            }
            Console.WriteLine($"Đính kèm order {order.OrderId} cho khách hàng {customer.Name}.");
        }


        public void DisplayAllOrders()
        {
            Console.WriteLine("Danh sách tất cả order trong hệ thống:");
            foreach (Order order in orders)
            {
                order.DisplayOrder();
                Console.WriteLine();
            }
        }


    }
}