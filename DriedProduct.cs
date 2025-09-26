namespace LT_B8_OOP
{
    public class DriedProduct : Product
    {
        private string storageCondition;
        public string StorageCondition { get => storageCondition; set => storageCondition = value; }
        public DriedProduct(string productId,string name, string origin, decimal price, int quantity, string storageCondition) : base(productId,name, origin, price, quantity)
        {
            this.storageCondition = storageCondition;
        }
        public override void DisplayInfo()
        {
            Console.WriteLine($"Dried Product: {ProductId} - {Name}, Origin: {Origin}, Price: {Price}, Quantity: {Quantity}, Storage Condition: {StorageCondition}");
        }
        public override string ProductType()
        {
            return "Dried";
        }
    }
}