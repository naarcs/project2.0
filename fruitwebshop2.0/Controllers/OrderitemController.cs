using fruitwebshop2._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fruitwebshop2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderitemController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var context = new FruitwebshopContext();
            try
            {
                return Ok(context.Orderitems.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var context = new FruitwebshopContext();
            try
            {
                var response = context.Orderitems.FirstOrDefault(f => f.OrderItemId == id);
                if (response == null)
                {
                    return BadRequest("Nincs ilyen rendelés tétel.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Orderitem orderItem)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Add(orderItem);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres adattárolás");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Orderitem orderItem)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Update(orderItem);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres módosítás.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var context = new FruitwebshopContext();
            try
            {
                Orderitem orderItem = new Orderitem();
                orderItem.OrderItemId = id;
                context.Remove(orderItem);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
