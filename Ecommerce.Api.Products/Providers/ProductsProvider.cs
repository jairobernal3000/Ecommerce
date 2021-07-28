using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Product = Ecommerce.Api.Products.Models.Product;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider: IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (dbContext.Products.Any()) return;
            dbContext.Products.Add(new Db.Product() {Id = 1, Name = "Keyboard", Price = 20, Inventory = 100});
            dbContext.Products.Add(new Db.Product() {Id = 2, Name = "Mouse", Price = 10, Inventory = 50});
            dbContext.Products.Add(new Db.Product() {Id = 3, Name = "tv", Price = 100, Inventory = 30});
            dbContext.Products.Add(new Db.Product() {Id = 4, Name = "display", Price = 150, Inventory = 20});
            dbContext.Products.Add(new Db.Product() {Id = 5, Name = "CPU", Price = 200, Inventory = 45});
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products == null || !products.Any()) return (false, null, "Not Found");
                
                var result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                return (true, result, null);
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
            
            
        }
    }
}