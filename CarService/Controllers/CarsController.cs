using CarService.Data;
using CarService.Dtos.RequestFeatures;
using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarService.Extentions;
using CarService.Repositories.Interfaces;

namespace CarService.Controllers
{
    
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICarRepository _repository;
        public CarsController(ApplicationDbContext context, ICarRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [Route("api/car")]
        [HttpGet]
        public async Task<IActionResult> GetAllCars([FromQuery] CarParameters carParams)
        { 
            var cars = await _repository.GetAllAsync(carParams);
            var companies = await _repository.GetCompaniesAsync();
            var models = await _repository.GetModelsAsync();
            var colors = await _repository.GetColorsAsync();

            return Ok(new CarDto
            {
                Cars = cars,
                Company = companies,
                Model = models,
                Color = colors
            });
        }

        [HttpGet]
        public async Task<IActionResult> Index(CarParameters carParams)
        {
            var cars = await _context.Cars.ToListAsync();

            var carCompanies = (from c in cars select c.Company).Distinct().ToList();
            var carModels = (from c in cars select c.Model).Distinct().ToList();
            var carColors = (from c in cars select c.Color).Distinct().ToList();

            var sendedCars = from c in cars
                         where c.Price >= carParams.MinPrice && c.Price <= carParams.MaxPrice
                         where carParams.Company == null || carParams.Company == "" || carParams.Company.Contains(c.Company)
                         where carParams.Model == null || carParams.Model == "" || carParams.Model.Contains(c.Model)
                         where carParams.Color == null || carParams.Color == "" || carParams.Color.Contains(c.Color)
                         select c;

            return View(new CarDto { Cars = sendedCars.ToList(),
                Company = String.Join(",", carCompanies), Model = String.Join(",", carModels), 
                Color = String.Join(",", carColors)
            });
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
