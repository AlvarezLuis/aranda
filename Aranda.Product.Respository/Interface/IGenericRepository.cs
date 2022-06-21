using Aranda.Product.Infraestructure.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Product.Respository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        int Count();
        IEnumerable<T> Get(Expression<Func<T, bool>> whereCondition = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string IncludeProperties = "",
            PaginationFilter paginationFilter = null);
        Task InsertAsync(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
