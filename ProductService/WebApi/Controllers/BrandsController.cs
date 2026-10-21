using Application.Feature.Brands.Commands;
using Application.Feature.Brands.Queries;
using AuthLibrary.Attributes;
using AuthLibrary.Constants.Authentication;
using Microsoft.AspNetCore.Mvc;
using ResponseLibrary.Models.Requests.Product;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : AppBaseController
    {
        [HttpPost("create")]
        [HasPermission(AppService.Product, AppFeature.Brands, AppAction.Create)]
        public async Task<IActionResult> Create([FromBody] CreateBrandRequest createBrand)
        {
            var response = await Sender.Send(new CreateBrandCommand { CreateBrand = createBrand });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("update")]
        [HasPermission(AppService.Product, AppFeature.Brands, AppAction.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateBrandRequest updateBrand)
        {
            var response = await Sender.Send(new UpdateBrandCommand { UpdateBrand = updateBrand });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        [HasPermission(AppService.Product, AppFeature.Brands, AppAction.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await Sender.Send(new DeleteBrandCommand { Id = id });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{id}")]
        [HasPermission(AppService.Product, AppFeature.Brands, AppAction.Read)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await Sender.Send(new GetBrandByIdQuery { Id = id });

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("all")]
        [HasPermission(AppService.Product, AppFeature.Brands, AppAction.Read)]
        public async Task<IActionResult> GetAll()
        {
            var response = await Sender.Send(new GetAllBrandsQuery());

            if (response.IsSuccessful) return Ok(response);

            return BadRequest(response);
        }
    }
}
