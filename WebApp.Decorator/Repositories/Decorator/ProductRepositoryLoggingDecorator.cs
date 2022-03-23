using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Decorator.Models;

namespace WebApp.Decorator.Repositories.Decorator
{
    public class ProductRepositoryLoggingDecorator :BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductRepositoryLoggingDecorator> _log;
        public ProductRepositoryLoggingDecorator(IProductRepository productRepository, ILogger<ProductRepositoryLoggingDecorator> log) : base(productRepository)
        {
            //productRepository i base yolluyoruz --> ancak ILogger<...> ise kendimiz burda set ediyoruz
         _log = log; //set ettik
        }

        public override Task<List<Product>> GetAll()
        {
            _log.LogInformation("GetAll Methodu çalıştı");
            return base.GetAll();
        }
        public override Task<List<Product>> GetAll(string userId)
        {
            _log.LogInformation("GetAll(userId) Methodu çalıştı");
            return base.GetAll(userId);
        }
        public override Task<Product> Save(Product product)
        {
            _log.LogInformation("Save Methodu çalıştı");
            return base.Save(product);
        }
        public override Task Update(Product product)
        {
            _log.LogInformation("Update Methodu çalıştı");
            return base.Update(product);
        }
        public override Task Remove(Product product)
        {
            _log.LogInformation("Remove Methodu çalıştı");
            return base.Remove(product);
        }
    }
}
