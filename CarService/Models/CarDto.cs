using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CarService.Models
{
    public class CarDto
    {
        public List<Car> Cars { get; set; }
        [BindProperty]
        public int MinPrice { get; set; } = 0;
        [BindProperty]
        public int MaxPrice { get; set; } = int.MaxValue;
        [BindProperty]
        public string Company { get; set; } = "";
        [BindProperty]
        public string Model { get; set; } = "";
        [BindProperty]
        public string Color { get; set; } = "";
    }
}
