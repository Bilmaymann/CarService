using CarService.Data;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Car car)
        {
            if (ModelState.IsValid)
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody]Car car)
        {
            if (id != car.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                        return NotFound();
                    else
                        throw;
                }
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
