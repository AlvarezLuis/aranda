using Aranda.Product.Infraestructure.DTO;
using Aranda.Product.Infraestructure.Filter;
using Aranda.Product.Infraestructure.Interface;
using Aranda.Product.Infraestructure.Pagination;
using Aranda.Product.Respository.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aranda.Product.Domain.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsService(
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(ProductDTO productDTO)
        {
            var category = _unitOfWork.CategoryRepository.Get(x => x.Id == productDTO.IdCategory);
            if (category.Count() < 1) throw new ArgumentNullException($"category {productDTO.IdCategory} not found");

            var product = _mapper.Map<Infraestructure.Models.Product>(productDTO);
            product.Category = category.FirstOrDefault();

             await _unitOfWork.ProductRepository.InsertAsync(product);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(x => x.Id == id);
            if(product.Count() == 0)
            {
                throw new ArgumentException($"product {id} not found");
            }
            _unitOfWork.ProductRepository.Delete(product.FirstOrDefault());
            _unitOfWork.Commit();
        }

        public async Task<PagedResult<ProductDTO>> GetAll(PaginationFilter paginationFilter, SearchFilter searchFilter, OrderBy orderBy)
        {
            IEnumerable<Infraestructure.Models.Product> products;
            switch (orderBy.Field?.ToLower())
            {
                case "name":
                    products = _unitOfWork.ProductRepository.Get(searchFilter.Contains(), x=> orderBy.Ascending ? x.OrderBy(o => o.Name) : x.OrderByDescending(o => o.Name), "Category", paginationFilter);
                    break;
                case "category":
                    products = _unitOfWork.ProductRepository.Get(searchFilter.Contains(), x => orderBy.Ascending ? x.OrderBy(o => o.Category.Name) : x.OrderByDescending(o => o.Category.Name), "Category", paginationFilter);
                    break;
                default: 
                    products = _unitOfWork.ProductRepository.Get(searchFilter.Contains(), null, "Category", paginationFilter);
                    break;
            }

            PagedResult<ProductDTO> result = new PagedResult<ProductDTO>(products.Select(x => _mapper.Map<ProductDTO>(x)).ToList(), paginationFilter.PageNumber, paginationFilter.PageSize);

            return result;
        }

        public void Update(ProductDTO productDTO)
        {
            _unitOfWork.ProductRepository.Update(_mapper.Map<Infraestructure.Models.Product>(productDTO));
            _unitOfWork.Commit();
        }
    }
}
