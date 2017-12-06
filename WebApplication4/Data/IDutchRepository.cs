using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace WebApplication4.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByCategory(string Category);
        IEnumerable<Order> GetAllOrders();
        bool SaveAll();
        Order GetOrderById(int id);
    }
}