namespace LT_B8_OOP
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } // composition: một đơn hàng có nhiều chi tiết đơn hàng

        public Order(string orderId, DateTime orderDate)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            OrderDetails = new List<OrderDetail>();
        }

        // dependency: phụ thuộc vào Product
        public void AddProduct(Product product, int quantity)
        {
            if (product.Quantity >= quantity)
            {
                OrderDetail orderDetail = new OrderDetail(product, quantity);
                OrderDetails.Add(orderDetail);
                // giảm tồn kho khi thêm vào đơn hàng
                product.Quantity -= quantity;
                Console.WriteLine($"Đã thêm {quantity} của {product.Name} vào đơn hàng {OrderId}");
            }
            else
            {
                Console.WriteLine($"Không đủ số lượng của {product.Name} để thêm vào đơn hàng {OrderId}");
            }
        }

        public decimal Total()
        {
            decimal total = 0;
            for (int i = 0; i < OrderDetails.Count; i++)
            {
                total += OrderDetails[i].TotalPrice();
            }
            return total;
        }
        public void DisplayOrder()
        {
            Console.WriteLine($"OrderId: {OrderId}, OrderDate: {OrderDate}");
            Console.WriteLine("Order Details:");
            foreach (OrderDetail orderDetail in OrderDetails)
            {
                orderDetail.DisplayOrderDetail();
            }
            Console.WriteLine($"Total Amount: {Total()}");
        }
    }
}