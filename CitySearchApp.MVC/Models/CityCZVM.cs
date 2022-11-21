namespace CitySearchApp.MVC.Models
{
    public class CityCZVM
    {
        public int Id { get; set; }
        public string Obec { get; set; }
        public string Okres { get; set; }
        public string Kraj { get; set; }
        public string PSC { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Distance { get; set; }
    }
}
