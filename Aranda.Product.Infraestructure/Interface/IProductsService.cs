using Aranda.Product.Infraestructure.DTO;
using Aranda.Product.Infraestructure.Filter;
using Aranda.Product.Infraestructure.Pagination;
using System.Threading.Tasks;

namespace Aranda.Product.Infraestructure.Interface
{
    public interface IProductsService
    {
        Task Create(ProductDTO product);
        void Update(ProductDTO product);
        void Delete(int id);
        Task<PagedResult<ProductDTO>> GetAll(PaginationFilter paginationFilter, SearchFilter searchFilter, OrderBy orderBy);
    }
}
