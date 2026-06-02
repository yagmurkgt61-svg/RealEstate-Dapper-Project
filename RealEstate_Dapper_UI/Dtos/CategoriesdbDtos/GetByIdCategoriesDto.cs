namespace RealEstate_Dapper_UI.Dtos.CategoriesdbDtos
{
    public class GetByIdCategoriesDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
    }
}
