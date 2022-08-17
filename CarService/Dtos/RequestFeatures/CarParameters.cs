namespace CarService.Dtos.RequestFeatures
{
    public class CarParameters
    {
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = int.MaxValue;
        public string Company { get; set; } = "";
        public string Model { get; set; } = "";
        public string Color { get; set; } = "";
        public string OrderBy { get; set; }
    }
}
