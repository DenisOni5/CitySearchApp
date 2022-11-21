namespace CitySearchApp.MVC.Models.CitySearch
{
    public class CityShortSearchVM
    {
        private string? obec;
        private string? kraj;

        public string? Kraj { get => kraj; set => kraj = System.Net.WebUtility.UrlDecode(value); }
        public string? Obec { get => obec; set => obec = System.Net.WebUtility.UrlDecode(value); }
        public int PerPage { get; set; } = 10;
        public int PageNum { get; set; } = 1;
    }
}
