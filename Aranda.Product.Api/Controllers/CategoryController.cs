using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aranda.Product.Infraestructure.Interface;
using Aranda.Product.Infraestructure.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aranda.Product.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService caetgoryService)
        {
            _categoryService = caetgoryService;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _categoryService.GetAll();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] Category category)
        {
            _categoryService.Create(category);
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public void Put([FromBody] Category category)
        {
            _categoryService.Update(category);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
