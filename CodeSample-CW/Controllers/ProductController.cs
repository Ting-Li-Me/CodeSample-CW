using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeSample_CW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProductsAsync")]
        public async Task<ActionResult<IEnumerable<ProductBO>>> GetAllProductsAsync()
        {
            var products = await productService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("GetProductByIdAsync/{id}")]
        public async Task<ActionResult<ProductBO>> GetProductByIdAsync([FromRoute]int id)
        {
            var productBO = await productService.GetProdByidAsync(id);

            return Ok(productBO);
        }

        [HttpPost("AddProductAsync")]
        public async Task<ActionResult> AddProductAsync([FromBody]ProductBO productBO)
        {
            await productService.AddProdAsync(productBO);

            return Ok(productBO);
        }


        [HttpPut("UpdateProductAsync/{id}")]
        public async Task<ActionResult<ProductBO>> UpdateProductAsync([FromRoute]int id, [FromBody]ProductBO productBO)
        {
            if (id != productBO.Id)
            {
                return NotFound();
            }

            await productService.UpdateProdAsync(productBO);

            return Ok(productBO);
        }

        [HttpDelete("DeleteProductAsync/{id}")]
        public async Task<ActionResult<IEnumerable<ProductBO>>> DeleteProductAsync([FromRoute]int id)
        {
            await productService.DeleteProdAsync(id);

            var products = await productService.GetAllProductsAsync();

            return Ok(products);
        }




    }
}
