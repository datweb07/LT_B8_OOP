namespace LT_B8_OOP
{
    public class Customer
    {
        private string customerId;
        private string name;
        private string phone;
        private string address;
        public List<Order> orders { get; set; } // aggregation: khách hàng có thể có nhiều đơn hàng

        public string CustomerId { get => customerId; set => customerId = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public Customer(string customerId, string name, string phone, string address)
        {
            this.customerId = customerId;
            this.name = name;
            this.phone = phone;
            this.address = address;
            orders = new List<Order>();
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Customer: {CustomerId} - {Name}, Phone: {Phone}, Address: {Address}");
        }

        // dependency customer phụ thuộc vào order
        public void AddOrder(Order order)
        {
            orders.Add(order);
            Console.WriteLine($"Khách hàng {Name} đã đặt đơn hàng {order.OrderId} vào ngày {order.OrderDate}");
        }

        // aggregation
        public void RemoveOrder(Order order)
        {
            orders.Remove(order);
            Console.WriteLine($"Khách hàng {Name} đã hủy đơn hàng {order.OrderId}");
        }
    }
}