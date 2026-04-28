namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class ResultLast3ProductWithCategoryDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string District { get; set; }
        public string CoverImage { get; set; }
        public string Description { get; set; }
        public int ProductCatergory { get; set; }
        public string CategoryName { get; set; }
        public DateTime AdvertisementDate { get; set; }
    }
}
