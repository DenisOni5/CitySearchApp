namespace CitySearchApp.MVC.Models.CitySearch
{
    public class CityCoordsSearchVM
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; } = 20;
        public int Count { get; set; }
    }
}
