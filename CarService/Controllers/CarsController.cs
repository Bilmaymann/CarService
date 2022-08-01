using CarService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.Controllers
{
    public class CarsController : Controller
    {
        public static List<Car> cars = new List<Car>()
        {
            new Car()
            {
                Id = 1,
                Company = "Chevrolet",
                Model = "Cobalt",
                Color = "Oq",
                Price = 10000
            },
            new Car()
            {
                Id = 2,
                Company = "Chevrolet",
                Model = "Malibu",
                Color = "Qora",
                Price = 30000
            },
            new Car()
            {
                Id = 3,
                Company = "Chevrolet",
                Model = "Matiz",
                Color = "Qizil",
                Price = 5000
    },
            new Car()
            {
                Id = 4,
                Company = "Chevrolet",
                Model = "Neksiya",
                Color = "ko`k",
                Price = 7000
            },
            new Car()
            {
                Id = 5,
                Company = "Chevrolet",
                Model = "Damas",
                Color = "Oq",
                Price = 10000
            },
        };
                
        [HttpGet]
        public async Task<IActionResult> Index(CarDto carParams)
        {
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

        [HttpGet]
        public async Task<CarDto> GetCars([FromRoute]CarDto carParams)
        {
            var sendedCars = from c in cars
                        where c.Price >= carParams.MinPrice && c.Price <= carParams.MaxPrice
                         where c.Company == carParams.Company || carParams.Company == null || carParams.Company == ""
                         where c.Model == carParams.Model || carParams.Model == null || carParams.Model == ""
                         where c.Color == carParams.Color || carParams.Color == null || carParams.Color == ""
                         select c;

            return new CarDto { Cars = sendedCars.ToList() };
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Company,Model,Color,Price")] Car car)
        {
            if (ModelState.IsValid)
            {
                cars.Add(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var car = cars.Find(c => c.Id == id);
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
                var carr = cars.Find(c => c.Id == id);
                cars.Remove(carr);
                cars.Add(car);

                return RedirectToAction(nameof(Index));
            }

            return View(car);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var car = cars.FirstOrDefault(m => m.Id == id);
            if (car == null)
                return NotFound();

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = cars.Find(c => c.Id == id);
            cars.Remove(car);

            return RedirectToAction(nameof(Index));
        }

        private bool carExists(int id)
        {
            return cars.Any(e => e.Id == id);
        }
    }
}
