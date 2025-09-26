namespace LT_B8_OOP
{
    public class OrderDetail
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderDetail(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            UnitPrice = product.Price;
        }
        public decimal TotalPrice()
        {
            return UnitPrice * Quantity;
        }

        public void DisplayOrderDetail()
        {
            Console.WriteLine($"Product: {Product.Name}, Quantity: {Quantity}, Unit Price: {UnitPrice}, Total Price: {TotalPrice()}");
        }

    }
}