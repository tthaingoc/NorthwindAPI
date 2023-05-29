using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindDBTest.API.Models.Data;
using NorthwindDBTest.API.Models.Domain;
using NorthwindDBTest.API.Models.DTO;
using NorthwindDBTest.API.Repository;

namespace NorthwindDBTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          var products = await productsRepository.GetAll();
         var productDto = mapper.Map<List<ProductDTO>>(products);
            return Ok(productDto);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
          var product = await productsRepository.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }
            var productDto = mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateProductRequestDTO updateProductRequestDTO)
        {
            var productDomain = new Product
            {
                ProductName = updateProductRequestDTO.ProductName,
                QuantityPerUnit = updateProductRequestDTO.QuantityPerUnit,
                UnitPrice = updateProductRequestDTO.UnitPrice,
                UnitsInStock = updateProductRequestDTO.UnitsInStock,
                UnitsOnOrder = updateProductRequestDTO.UnitsOnOrder,
                ReorderLevel = updateProductRequestDTO.ReorderLevel,
                Discontinued = updateProductRequestDTO.Discontinued,
            };
            // check product existed and update
            var productUpdated = await productsRepository.Update(id, productDomain);
            if(productUpdated == null )
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDTO>(productUpdated));
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDTO)
        {
            var product = mapper.Map<Product>(createProductDTO);
            await productsRepository.Create(product);
            var productDTO = mapper.Map<ProductDTO>(product);
            return Ok();
        }

        // DELETE: api/Products/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var product = await productsRepository.Delete(id);
            if(product == null ) { return NotFound(); } return Ok(mapper.Map<ProductDTO>(product));
        }

    }
}
