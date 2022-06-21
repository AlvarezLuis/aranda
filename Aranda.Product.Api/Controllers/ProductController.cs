using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aranda.Product.Infraestructure.DTO;
using Aranda.Product.Infraestructure.Filter;
using Aranda.Product.Infraestructure.Interface;
using Aranda.Product.Infraestructure.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aranda.Product.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductController(
            IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<PagedResult<ProductDTO>> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] SearchFilter searchFilter, [FromQuery] OrderBy orderBy)
        {
            var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var products = await _productsService.GetAll(validFilter, searchFilter, orderBy);
            return products;
        }


        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _productsService.Create(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductDTO product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _productsService.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productsService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
