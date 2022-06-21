using Aranda.Product.Infraestructure.Filter;
using Aranda.Product.Respository.DataAcces;
using Aranda.Product.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aranda.Product.Respository.GenericRespository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private string _errorMessage = string.Empty;

        protected ApplicationContext _context;
        protected readonly ILogger _logger;

        public GenericRepository(ApplicationContext context, ILogger logger) {
            _context = context;
            _logger = logger;
        }
       
        public int Count()
        {
            return  _context.Set<T>().Count();
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> whereCondition = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string IncludeProperties = "", 
            PaginationFilter paginationFilter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            foreach (var includeproperty in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeproperty);
            }

            if(paginationFilter != null)
            {
                query = query.Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task InsertAsync(T obj)
        {
            try
            {
                if (obj == null)
                {
                    _logger.LogError("Null entity");
                    throw new ArgumentNullException("entity");
                }
                await _context.Set<T>().AddAsync(obj);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                _logger.LogError(_errorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Update(T obj)
        {
            try
            {
                if (obj == null)
                {
                    _logger.LogError("Null entity");
                    throw new ArgumentNullException("entity");
                }
                _context.Entry(obj).State = EntityState.Modified;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                
                _logger.LogError(_errorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void Delete(T obj)
        {
            try
            {
                if (obj == null)
                {
                    _logger.LogError("Null entity");
                    throw new ArgumentNullException("entity");
                }
                _context.Remove(obj);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                _logger.LogError(_errorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
    }
}
