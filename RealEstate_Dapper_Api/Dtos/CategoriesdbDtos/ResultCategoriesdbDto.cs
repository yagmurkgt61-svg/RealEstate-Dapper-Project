namespace RealEstate_Dapper_Api.Dtos.CategoriesdbDtos
{
    public class ResultCategoriesdbDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}
