namespace LT_B8_OOP
{
    public interface IProductFactory
    {
        Product CreateProduct(string type, string productId,string name, string origin, decimal price, int quantity, params object[] parameters);
    }

    public class ProductFactory : IProductFactory
    {
        public Product CreateProduct(string type, string productId,string name, string origin, decimal price, int quantity, params object[] parameters)
        {
            switch (type.ToLower())
            {
                case "fresh":
                    if (parameters.Length == 1 && parameters[0] is int maxDays)
                    {
                        return new FreshProduct(productId ,name, origin, price, quantity, maxDays);
                    }
                    throw new ArgumentException("Invalid parameters for FreshProduct");
                case "cold":
                    if (parameters.Length == 1 && parameters[0] is double temperature)
                    {
                        return new ColdProduct(productId,name, origin, price, quantity, temperature);
                    }
                    throw new ArgumentException("Invalid parameters for ColdProduct");
                case "dried":
                    if (parameters.Length == 1 && parameters[0] is string storageCondition)
                    {
                        return new DriedProduct(productId,name, origin, price, quantity, storageCondition);
                    }
                    throw new ArgumentException("Invalid parameters for DryProduct");
                default:
                    throw new ArgumentException("Invalid product type");
            }
        }
    }
}