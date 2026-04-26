namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class ResultProductAdvertListWithCategoryByEmployeeDto
    {
        public int productID { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public int CityId { get; set; }
        public string cityName { get; set; }
        public string district { get; set; }
        public string categoryName { get; set; }
        public string coverImage { get; set; }
        public string type { get; set; }
        public string adress { get; set; }
        public bool dealOfTheDay { get; set; }
    }
}
