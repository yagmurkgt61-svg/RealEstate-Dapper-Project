namespace RealEstate_Dapper_UI.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string title { get; set; }
        public decimal price { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public object categoryid { get; set; }
        public string CoverImage { get; set; }
        public string type { get; set; }
        public string adress { get; set; }
    }
}
