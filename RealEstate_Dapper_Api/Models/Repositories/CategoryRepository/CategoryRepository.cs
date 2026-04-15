using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
using Dapper;
using System.Threading.Tasks;

namespace RealEstate_Dapper_Api.Models.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCategory(CreateCategoryDto CategoryDto)
        {
            // tabloda belirlediğim alanlar için yeni bir kayıt ekleyen sorgu
            string query = "Insert Into Category (CategoryName,CategoryStatus) values (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", CategoryDto.CategoryName);
            parameters.Add("@categoryStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteCategory(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydı silen sorgu
            string query = "Delete From Category where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }   
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            // tabloda bulunan tüm kayıtları listeleyen sorgu
            string query = "Select * From Category";
            using (var connection = _context.CreateConnection())
            {
                var values  = await connection.QueryAsync<ResultCategoryDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetByIDCategoryDto> GetCategory(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydı getiren sorgu
            string query = "Select * From Category where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID", id);
            using (var connection = _context.CreateConnection())
            {
                var values= await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDto>(query,parameters);
                return values;
            }
        }

        public async void UpdateCategory(UpdateCategoryDto categoryDto)
        {
            // tabloda belirlediğim id'ye sahip kaydı güncelleyen sorgu
            string query = "Update Category Set CategoryName=@categoryName,CategoryStatus=@categoryStatus where CategoryID=@categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDto.CategoryName);
            parameters.Add("@categoryStatus", categoryDto.CategoryStatus);
            parameters.Add("@categoryID", categoryDto.CategoryID);
            using (var connectiont = _context.CreateConnection()) { 
                await connectiont.ExecuteAsync(query, parameters);
            }
        }

  
    }
}
