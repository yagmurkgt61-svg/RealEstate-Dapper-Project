namespace RealEstate_Dapper_Api.Dtos.CategoriesdbDtos
{
    public class CreateCategoriesdbDto
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}   
