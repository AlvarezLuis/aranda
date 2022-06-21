using Aranda.Product.Respository.DataAcces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Product.Respository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRespository ProductRepository { get; }
        ICategoryRespository CategoryRepository { get; }
        void Commit();
    }
}
