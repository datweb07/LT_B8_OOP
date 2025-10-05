namespace LT_B8_OOP
{
    public class SuperMarket : IDisposable
    {
        private static SuperMarket? instance;
        private List<Product> products; // sản phẩm ĐANG BÁN trong cửa hàng
        private List<Customer> customers;
        private List<Order> orders;
        private IProductFactory productFactory;
        private bool disposed = false;

        public BehavioralTransaction behavioralTransaction { get; set; }
        public Warehouse Warehouse { get; private set; } // kho dự trữ, ban đầu TRỐNG

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

        // Thêm sản phẩm vào cửa hàng (KHÔNG nhập kho)
        public void AddProduct(string type, string productId, string name, string origin, decimal price, int quantity, params object[] parameters)
        {
            Product product = productFactory.CreateProduct(type, productId, name, origin, price, quantity, parameters);
            products.Add(product);
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void CustomersPurchaseProducts(List<Customer> customers, List<Product> products)
        {
            behavioralTransaction.AddCustomerProductAssociation(customers, products);
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Danh sách sản phẩm trong siêu thị:");
            if (products.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm trong siêu thị");
            }
            else 
            {
                foreach (Product product in products)
                {
                    product.DisplayInfo();
                }
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
            customers.Remove(customer);
            Console.WriteLine($"Đã xóa khách hàng {customer.Name} khỏi danh sách. Các đơn hàng của khách hàng vẫn được giữ lại.");
        }

        public Order CreateOrder(string orderId, DateTime orderDate)
        {
            Order order = new Order(orderId, orderDate);
            orders.Add(order);
            return order;
        }

        // Bán hàng
        public bool AddProductToOrder(Order order, Product product, int quantity)
        {
            if (product.Quantity < quantity)
            {
                Console.WriteLine($"Không đủ số lượng của {product.Name} trong cửa hàng để thêm vào đơn hàng {order.OrderId}");
                return false;
            }

            // Trừ trực tiếp từ sản phẩm trong cửa hàng
            product.Quantity -= quantity;
            OrderDetail orderDetail = new OrderDetail(product, quantity);
            order.OrderDetails.Add(orderDetail);
            Console.WriteLine($"Đã thêm {quantity} của {product.Name} vào đơn hàng {order.OrderId}");
            return true;
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

        // IDisposable implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("=== BẮT ĐẦU HỦY CỬA HÀNG ===");

                    // Đẩy tất cả sản phẩm còn lại trong cửa hàng về kho dự trữ
                    foreach (Product product in products)
                    {
                        if (product.Quantity > 0)
                        {
                            Warehouse.Import(product, product.Quantity);
                            Console.WriteLine($"Đã trả {product.Quantity} {product.Name} về kho");
                        }
                    }

                    // Xóa danh sách sản phẩm trong cửa hàng
                    products.Clear();
                    Console.WriteLine("Đã xóa tất cả sản phẩm khỏi cửa hàng");

                    // Xóa khách hàng (Aggregation - orders vẫn tồn tại)
                    customers.Clear();
                    Console.WriteLine("Đã xóa tất cả khách hàng");

                    // Hiển thị trạng thái kho sau khi hủy
                    Console.WriteLine("\n=== TRẠNG THÁI KHO SAU KHI HỦY CỬA HÀNG ===");
                    DisplayWarehouseInventory();

                    Console.WriteLine("\n=== ĐÃ HỦY CỬA HÀNG THÀNH CÔNG ===");
                }

                disposed = true;
            }
        }

        public void DisplayWarehouseInventory()
        {
            Console.WriteLine("Tồn kho trong Warehouse:");
            if (Warehouse.Inventory.Count == 0)
            {
                Console.WriteLine("Kho trống");
            }
            else
            {
                foreach (Product product in Warehouse.Inventory)
                {
                    Console.WriteLine($"- {product.Name} (ID: {product.ProductId}): {product.Quantity} sản phẩm");
                }
            }
        }

        ~SuperMarket()
        {
            Dispose(false);
        }
    }
}