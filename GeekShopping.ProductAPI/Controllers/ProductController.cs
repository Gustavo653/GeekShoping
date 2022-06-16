using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var product = await _repository.FindAll();
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Post([FromBody] ProductVO product)
        {
            if (product == null)
                return BadRequest();
            var productEntity = await _repository.Create(product);
            return Ok(productEntity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductVO>> Put([FromBody] ProductVO product)
        {
            if (product == null)
                return BadRequest();
            var productEntity = await _repository.Update(product);
            return Ok(productEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status)
                return BadRequest();
            return Ok(status);
        }
    }
}
