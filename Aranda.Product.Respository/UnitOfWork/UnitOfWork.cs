using Aranda.Product.Respository.DataAcces;
using Aranda.Product.Respository.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Aranda.Product.Respository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly ILogger _logger;

        public IProductRespository ProductRepository { get; private set; }
        public ICategoryRespository CategoryRepository { get; private set; }

        public UnitOfWork(ApplicationContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            ProductRepository = new ProductRespository(context, _logger);
            CategoryRepository = new CategoryRespository(context, _logger);
        }

        public void Commit()
        {
             _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
