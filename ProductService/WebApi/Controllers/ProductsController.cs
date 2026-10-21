using Application.Feature.Products.Commands;
using Application.Feature.Products.Queries;
using AuthLibrary.Attributes;
using AuthLibrary.Constants.Authentication;
using Microsoft.AspNetCore.Mvc;
using ResponseLibrary.Models.Requests.Product;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : AppBaseController
    {
        [HttpPost("create")]
        [HasPermission(AppService.Product, AppFeature.Products, AppAction.Create)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest createProduct)
        {
            var response = await Sender.Send(new CreateProductCommand { CreateProduct = createProduct });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update")]
        [HasPermission(AppService.Product, AppFeature.Products, AppAction.Update)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest updateProduct)
        {
            var response = await Sender.Send(new UpdateProductCommand { UpdateProduct = updateProduct });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        [HasPermission(AppService.Product, AppFeature.Products, AppAction.Delete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await Sender.Send(new DeleteProductCommand { ProductId = id });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        [HasPermission(AppService.Product, AppFeature.Products, AppAction.Read)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await Sender.Send(new GetProductByIdQuery { ProductId = id });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("all")]
        [HasPermission(AppService.Product, AppFeature.Products, AppAction.Read)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await Sender.Send(new GetAllProductsQuery());

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }
    }
}
