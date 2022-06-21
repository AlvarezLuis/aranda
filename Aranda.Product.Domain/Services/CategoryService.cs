using Aranda.Product.Infraestructure.DTO;
using Aranda.Product.Infraestructure.Interface;
using Aranda.Product.Infraestructure.Models;
using Aranda.Product.Respository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aranda.Product.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(Category category)
        {
            await _unitOfWork.CategoryRepository.InsertAsync(category);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            var categories = _unitOfWork.CategoryRepository.Get(x => x.Id == id);
            if (categories.Count() == 0)
            {
                throw new ArgumentException($"categoy {id} not found");
            }
            _unitOfWork.CategoryRepository.Delete(categories.FirstOrDefault());
            _unitOfWork.Commit();
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.CategoryRepository.Get();
        }

        public void Update(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
        }
    }
}
