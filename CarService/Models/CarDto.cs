using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace CarService.Models
{
    public class CarDto
    {
        public List<Car> Cars { get; set; }
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public string Company { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
    }
}
