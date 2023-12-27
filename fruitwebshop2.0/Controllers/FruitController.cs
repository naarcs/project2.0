using fruitwebshop2._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fruitwebshop2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            var context = new FruitwebshopContext();
            try
            {
                return Ok(context.Fruits.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
