using fruitwebshop2._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruitwebshop2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var context = new FruitwebshopContext();
            try
            {
                return Ok(context.Orders.Include(f => f.Orderitems).ToList());
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
                var response = context.Orders.FirstOrDefault(f => f.OrderId == id);
                if (response == null)
                {
                    return BadRequest("Nincs ilyen rendelés.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Order order)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Add(order);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres adattárolás");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Order order)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Update(order);
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
                Order order = new Order();
                order.OrderId = id;
                context.Remove(order);
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
