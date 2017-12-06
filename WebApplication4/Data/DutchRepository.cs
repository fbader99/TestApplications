using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;
        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts() was called");
                return _ctx.Products.OrderBy(p => p.Title).ToList();
            }
            catch(Exception exp)
            {
                _logger.LogError("Failed to get All producctsion : " + exp.ToString());
            }
            return null;
        }
        public IEnumerable<Order> GetAllOrders()
        {
            _logger.LogInformation("GetAllOrders() was called");
            return _ctx.Orders.Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Product> GetProductByCategory(string Category)
        {
            return _ctx.Products.Where(p => p.Category == Category).ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders.Include(o => o.Items)
              .ThenInclude(i => i.Product)
              .Where (o => o.Id == id)
              .FirstOrDefault();
        }
    }
}
