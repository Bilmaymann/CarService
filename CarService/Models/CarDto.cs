using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CarService.Models
{
    public class CarDto
    {
        public List<Car> Cars { get; set; }
        public string Company { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
    }
}
