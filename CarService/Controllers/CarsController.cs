using CarService.Data;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CarService.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/cars")]
        public async Task<IActionResult> GetAllCars([FromQuery]CarDto carParams)
        {
            Console.WriteLine(carParams.MinPrice);
            var cars = await _context.Cars.ToListAsync();
            var sendedCars = from c in cars
                             where c.Price >= carParams.MinPrice && (c.Price <= carParams.MaxPrice || carParams.MaxPrice == 0)
                             where carParams.Company == null || carParams.Company == "" || carParams.Company.Contains(c.Company)
                             where carParams.Model == null || carParams.Model == "" || carParams.Model.Contains(c.Model)
                             where carParams.Color == null || carParams.Color == "" || carParams.Color.Contains(c.Color)
                             select c;
            return Ok(sendedCars);
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarDto carParams)
        {
            var cars = await _context.Cars.ToListAsync();

            ViewData["Company"] = (from c in cars select c.Company).Distinct().ToList();
            ViewData["Model"] = (from c in cars select c.Model).Distinct().ToList();
            ViewData["Color"] = (from c in cars select c.Color).Distinct().ToList();

            var sendedCars = from c in cars
                         where c.Price >= carParams.MinPrice && c.Price <= carParams.MaxPrice
                         where carParams.Company == null || carParams.Company == "" || carParams.Company.Contains(c.Company)
                         where carParams.Model == null || carParams.Model == "" || carParams.Model.Contains(c.Model)
                         where carParams.Color == null || carParams.Color == "" || carParams.Color.Contains(c.Color)
                         select c;

            return View(new CarDto { Cars = sendedCars.ToList(),
                Company = carParams.Company, Model = carParams.Model, Color = carParams.Color});
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Company,Model,Color,Price")] Car car)
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
                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
                return NotFound();

            return View(car);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
