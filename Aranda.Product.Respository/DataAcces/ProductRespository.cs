using Aranda.Product.Respository.GenericRespository;
using Aranda.Product.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.Product.Respository.DataAcces
{
    public class ProductRespository : GenericRepository<Infraestructure.Models.Product>, IProductRespository
    {
       public ProductRespository(ApplicationContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
