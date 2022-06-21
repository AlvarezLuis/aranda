using Aranda.Product.Infraestructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.Product.Infraestructure.Interface
{
    public interface ICategoryService
    {
        Task Create(Category category);
        void Update(Category category);
        void Delete(int id);
        IEnumerable<Category> GetAll();
    }
}
