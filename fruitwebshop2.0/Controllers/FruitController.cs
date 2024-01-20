using fruitwebshop2._0.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet("{id}")]

        public IActionResult GetId(int id)
        {
            var context = new FruitwebshopContext();
            try
            {
                var response = context.Fruits.FirstOrDefault(f => f.FruitId == id);
                if (response != null)
                {
                    return BadRequest("Nincs ilyen gyümölcs.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]

        public IActionResult Post(Fruit fruit)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Add(fruit);
                context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres adattárolás");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        [HttpPut]

        public IActionResult Put(Fruit fruit)
        {
            var context = new FruitwebshopContext();
            try
            {
                context.Update(fruit);
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
                Fruit fruit = new Fruit();
                fruit.FruitId = id;
                context.Remove(fruit);
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
