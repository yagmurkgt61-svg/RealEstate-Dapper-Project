namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string District { get; set; }
        public string? CategoryName { get; set; }
        public string CoverImage { get; set; }
        public string Type { get; set; }
        public string Adress { get; set; }
        public bool ProductStatus { get; set; }
    }
}
