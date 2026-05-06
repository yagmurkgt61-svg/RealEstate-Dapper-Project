namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string District { get; set; }
        public string CoverImage { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool DealOfTheDay { get; set; }
        public DateTime AdvertisementDate { get; set; }
        public bool ProductStatus { get; set; }
        public int ProductCategory { get; set; }
        public int AppUserId { get; set; }
    }
}
