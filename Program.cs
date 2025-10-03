using System.Text;

namespace LT_B8_OOP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();

            // Singleton instance
            SuperMarket superMarket = SuperMarket.GetInstance;

            // Factory usage
            superMarket.AddProduct("fresh", "F001", "Cá thu tươi", "Việt Nam", 120000, 50, 2);
            superMarket.AddProduct("dried", "D001", "Gạo lứt", "Thái Lan", 25000, 100, "Nơi khô ráo");
            superMarket.AddProduct("cold", "C001", "Thịt bò đông lạnh", "Mỹ", 350000, 30, -18.0);

            // Adding customers
            Customer customer1 = new Customer("KH001", "Nguyễn Văn A", "0123456789", "123 Đường A, Quận B");
            Customer customer2 = new Customer("KH002", "Trần Thị B", "0987654321", "456 Đường C, Quận D");
            superMarket.AddCustomer(customer1);
            superMarket.AddCustomer(customer2);

            // Displaying products and customers
            superMarket.DisplayProducts();
            Console.WriteLine();
            superMarket.DisplayCustomers();
            Console.WriteLine();


            // 1. ASSOCIATION: Customer ↔ Product

            superMarket.CustomersPurchaseProducts(
            new List<Customer> { customer1 },
            new List<Product> { superMarket.FindProductById("F001"), superMarket.FindProductById("D001"), superMarket.FindProductById("C001") });

            superMarket.CustomersPurchaseProducts(
            new List<Customer> { customer1, customer2 },
            new List<Product> { superMarket.FindProductById("D001") });

            // hiển thị sản phẩm của customer1 và customer2
            superMarket.behavioralTransaction.DisplayCustomerProducts(customer1);
            superMarket.behavioralTransaction.DisplayCustomerProducts(customer2);

            // hiển thị khách hàng đã mua sản phẩm D001
            superMarket.behavioralTransaction.DisplayProductCustomers(superMarket.FindProductById("D001"));
            Console.WriteLine();



            // 2. DEPENDENCY: Order → Product

            // Tạo đơn hàng (qua SuperMarket để theo dõi tồn tại độc lập)
            Order order1 = superMarket.CreateOrder("DH001", DateTime.Now);
            Order order2 = superMarket.CreateOrder("DH002", DateTime.Now);

            Product product1 = superMarket.FindProductById("F001");
            Product product2 = superMarket.FindProductById("D001");
            Product product3 = superMarket.FindProductById("C001");

            // đơn hàng phụ thuộc vào sản phẩm (xuất kho qua Warehouse)
            superMarket.AddProductToOrder(order1, product1, 2);
            superMarket.AddProductToOrder(order1, product2, 2);
            superMarket.AddProductToOrder(order1, product3, 2);
            superMarket.AddProductToOrder(order2, product2, 3);
            Console.WriteLine();


            superMarket.DisplayProducts();

            // 3. AGGREGATION: Customer ↔ Order

            // khách hàng thêm đơn hàng (aggregation)
            superMarket.AttachOrderToCustomer(customer1, order1);
            superMarket.AttachOrderToCustomer(customer2, order2);
            Console.WriteLine();


            superMarket.DisplayAllOrders();


            // 4. COMPOSITION: Order ↔ OrderDetails

            Console.WriteLine("   Trước khi xóa orderDetails DH001:");
            order1.DisplayOrder();
            Console.WriteLine();

            superMarket.RemoveOrder(order1);

            Console.WriteLine("   Sau khi xóa orderDetails DH001:");
            order1.DisplayOrder();
            Console.WriteLine();
            superMarket.DisplayProducts();

            Console.ReadKey();
        }
    }
}
/*
Một cửa hàng bán thực phẩm bán 3 dạng thực phẩm chính: thực phẩm đông lạnh, thực phẩm tươi sống và thực phẩm khô.
Cả ba loại thực phẩm trên đều có các thuộc tính: tên thực phẩm, xuất xứ, giá, số lượng. 
Riêng với thực phẩm đông lạnh có điều kiện bảo quản (nhiệt độ), thực phẩm tươi sống (có số ngày tối đa sử dụng), thực phẩm khô (có điều kiện bảo quản, ví dụ bảo quản nơi không ẩm mốc)
Xây dựng chương trình cho phép:
- Có sử dụng Singleton design pattern
- Có sử dụng Factory design pattern
- thỏa mãn 5 quan hệ trong phân tích hướng đối tượng (dependency, association, aggregation, composition, generalization/inheritance)
*/

