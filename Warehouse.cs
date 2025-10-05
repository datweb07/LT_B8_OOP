namespace LT_B8_OOP
{
    public class Warehouse
    {
        private List<Product> inventory;

        public Warehouse()
        {
            inventory = new List<Product>();
        }

        public IReadOnlyList<Product> Inventory => inventory;

        // nhập kho
        public void Import(Product product, int quantity)
        {
            Product? exist = FindProductId(product.ProductId);
            if (exist == null)
            {
                Product newRecord = CloneProductWithQuantity(product, quantity);
                inventory.Add(newRecord);
            }
            else
            {
                exist.Quantity += quantity;
            }
        }

        // xuất kho
        public bool Export(Product product, int quantity)
        {
            Product? exist = FindProductId(product.ProductId);
            if (exist == null || exist.Quantity < quantity)
            {
                return false;
            }
            exist.Quantity -= quantity;
            return true;
        }

        // trả về kho
        public void Return(Product product, int quantity)
        {
            Import(product, quantity);
        }


        public Product? FindProductId(string productId)
        {
            foreach (Product p in inventory)
            {
                if (p.ProductId == productId)
                {
                    return p;
                }
            }
            return null;
        }

        private Product CloneProductWithQuantity(Product product, int quantity)
        {
            // sao chép theo từng loại sản phẩm
            if (product is FreshProduct fresh)
            {
                return new FreshProduct(fresh.ProductId, fresh.Name, fresh.Origin, fresh.Price, quantity, fresh.MaxDays);
            }
            if (product is ColdProduct cold)
            {
                return new ColdProduct(cold.ProductId, cold.Name, cold.Origin, cold.Price, quantity, cold.Temperature);
            }
            if (product is DriedProduct dried)
            {
                return new DriedProduct(dried.ProductId, dried.Name, dried.Origin, dried.Price, quantity, dried.StorageCondition);
            }
            return product;
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Tồn kho trong Warehouse:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Không có sản phẩm tổn kho");
            }
            else
            {
                foreach (Product product in Inventory)
                {
                    Console.WriteLine($"- {product.Name} (ID: {product.ProductId}): {product.Quantity} sản phẩm");
                }
            }
        }
    }
}


