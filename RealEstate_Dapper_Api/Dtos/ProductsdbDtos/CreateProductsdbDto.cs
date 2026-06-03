namespace RealEstate_Dapper_Api.Dtos.ProductsdbDtos
{
    public class CreateProductsdbDto
    {
        public int CategoryID { get; set; }

        public string ProductName { get; set; } 

        public int UnitInStock { get; set; }

        public decimal Price { get; set; }

        public decimal PriceVat { get; set; }
        public string Image { get; set; }
        public string Details { get; set; }
    }
}
