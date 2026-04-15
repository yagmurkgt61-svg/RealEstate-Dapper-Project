using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;

namespace RealEstate_Dapper_UI.Models
{
    public class CompositeViewModel
    {
        public List<ResultCategoryDto> CategoryList { get; set; }
        public List<ResultProductDto> ProductList { get; set; }
    }
}
