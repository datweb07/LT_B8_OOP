**Tôi thêm README này để ghi nhớ Aggregation giữa `SuperMarket` và `Warehouse`** 

## Components
1. **`products` (Danh sách sản phẩm trong cửa hàng)**
   - Nơi **trực tiếp bán hàng** cho khách
   - Sản phẩm được thêm vào đây để bán
   - Ban đầu có đầy đủ số lượng

2. **`Warehouse` (Kho dự trữ)**
   - Nơi **lưu trữ khi xóa đi cửa hàng**
   - Ban đầu **TRỐNG**
   - Chỉ nhận hàng khi cửa hàng đóng cửa (Dispose)

## Activity Flow

```
┌─────────────────────────────────────────────────────────┐
│ 1. Thêm sản phẩm vào cửa hàng                           │
│    AddProduct() → products: 100 | Warehouse: 0          │
└─────────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────────┐
│ 2. Bán hàng (trừ trực tiếp từ products)                 │
│    Bán 52 → products: 48 | Warehouse: 0                 │
└─────────────────────────────────────────────────────────┘
                        ↓
┌─────────────────────────────────────────────────────────┐
│ 3. Hủy cửa hàng (Dispose)                               │
│    Đẩy 48 còn lại về kho → products: 0 | Warehouse: 48  │
└─────────────────────────────────────────────────────────┘
```
