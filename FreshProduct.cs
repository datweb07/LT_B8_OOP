namespace LT_B8_OOP
{
    public class FreshProduct : Product
    {
        private int maxDays;
        public int MaxDays { get => maxDays; set => maxDays = value; }
        public FreshProduct(string productId, string name, string origin, decimal price, int quantity, int maxDays) : base(productId,name, origin, price, quantity)
        {
            this.maxDays = maxDays;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Fresh Product: {ProductId} - {Name}, Origin: {Origin}, Price: {Price}, Quantity: {Quantity}, Max Days: {MaxDays}");
        }
        public override string ProductType()
        {
            return "Fresh";
        }
    }
}