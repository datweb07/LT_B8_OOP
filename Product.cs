namespace LT_B8_OOP
{
    public abstract class Product
    {
        private string productId;
        private string name;
        private string origin;
        private decimal price;
        private int quantity;
        public string ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public string Origin { get => origin; set => origin = value; }
        public decimal Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public Product(string productId,string name, string origin, decimal price, int quantity)
        {
            this.productId = productId;
            this.name = name;
            this.origin = origin;
            this.price = price;
            this.quantity = quantity;
        }
        public abstract void DisplayInfo();
        public abstract string ProductType();
    }
}