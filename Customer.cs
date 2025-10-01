namespace LT_B8_OOP
{
    public class Customer
    {
        private string customerId;
        private string name;
        private string phone;
        private string address;
        public List<Order> orders { get; set; } // aggregation: khách hàng có thể có nhiều đơn hàng
        public List<OrderDetail> orderDetails { get; set; }

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
            orderDetails = new List<OrderDetail>();
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Customer: {CustomerId} - {Name}, Phone: {Phone}, Address: {Address}");
        }

        // aggregation support: cho phép gỡ đơn hàng khỏi khách hàng mà không xóa đơn hàng
        //public void RemoveOrder(Order order)
        //{
        //    if (orders.Remove(order))
        //    {
        //        Console.WriteLine($"Đã gỡ đơn hàng {order.OrderId} khỏi khách hàng {Name} (order vẫn tồn tại).");
        //        // trả order về cửa hàng để quản lý tập trung
        //        SuperMarket.GetInstance.AcceptOrderIntoStore(order);
        //    }
        //    orderDetails.Clear();
        //}

    }
}