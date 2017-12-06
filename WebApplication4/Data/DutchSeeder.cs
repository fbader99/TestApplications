using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        public DutchSeeder(DutchContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }
        public void Seed()
        {
            //ensure the database has already been created
            _ctx.Database.EnsureCreated();

            if(!_ctx.Products.Any())
            {
                //create sample data
                var _filePath = Path.Combine(_hosting.ContentRootPath + "/Data/art.json");
                var _json = File.ReadAllText(_filePath);
                var _products = JsonConvert.DeserializeObject<IEnumerable<Product>>(_json);
                _ctx.Products.AddRange(_products);

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = _products.First(),
                            Quantity = 5,
                            UnitPrice = _products.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}
