namespace RealEstate_Dapper_Api.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public bool ProductStatus { get; set; }
    }
}
