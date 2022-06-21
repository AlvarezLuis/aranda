using Aranda.Product.Respository.GenericRespository;
using Aranda.Product.Respository.Interface;
using Microsoft.Extensions.Logging;

namespace Aranda.Product.Respository.DataAcces
{
    public class CategoryRespository : GenericRepository<Infraestructure.Models.Category>, ICategoryRespository
    {
        public CategoryRespository(ApplicationContext context, ILogger logger) : base(context, logger)
        {

        }
    }
}
