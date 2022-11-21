namespace CitySearchApp.MVC.Models.CitySearch
{
    public class CityLongSearchVM : CityShortSearchVM
    {
        public int? start { get; set; }
        public int? finish { get; set; }
    }
}
