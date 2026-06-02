namespace RealEstate_Dapper_UI.Dtos.ProductsdbDtos
{
    public class GetProductsdbByProductIdDto
    {
        public int ProductID { get; set; }

        public int CategoryID { get; set; }

        public string ProductName { get; set; }

        public int UnitInStock { get; set; }

        public decimal Price { get; set; }

        public decimal PriceVat { get; set; }
    }
}
