using fruitwebshop2._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fruitwebshop2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            var context = new FruitwebshopContext();
            try
            {
                return Ok(context.Users.Include(f => f.Order).Include(f => f.Order.Orderitems).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]

        public IActionResult Post(User user)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Add(user);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres adattárolás");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpPut]

        public IActionResult Put(User user)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Update(user);
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
                User user = new User();
                user.UserId = id;
                context.Remove(user);
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
