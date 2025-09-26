namespace LT_B8_OOP
{
    public class ColdProduct : Product
    {
        private double temperature;
        public double Temperature { get => temperature; set => temperature = value; }
        public ColdProduct(string productId,string name, string origin, decimal price, int quantity, double temperature) : base(productId,name, origin, price, quantity)
        {
            this.temperature = temperature;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Cold Product: {ProductId} - {Name}, Origin: {Origin}, Price: {Price}, Quantity: {Quantity}, Temperature: {Temperature}");
        }
        public override string ProductType()
        {
            return "Cold";
        }
    }
}