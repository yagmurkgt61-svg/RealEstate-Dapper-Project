using Dapper;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Models.DapperContext;
namespace RealEstate_Dapper_Api.Models.Repositories.BottomGridRepositories
{
    public class BottomGridRepository : IBottomGridRepository
    {
        private readonly Context _context;

        public BottomGridRepository(Context context)
        {
            _context = context;
        }
   
        public async void CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
        {
            // tabloda belirlediğim alanlar için yeni bir kayıt ekleyen sorgu
            string query = "Insert Into BottomGrid (Icon,Title,Description) values (@icon,@title,@description)";
            var parameters = new DynamicParameters();
            parameters.Add("@icon", createBottomGridDto.Icon);
            parameters.Add("@title", createBottomGridDto.Title);
            parameters.Add("@description", createBottomGridDto.Description);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteBottomGrid(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydı silen sorgu
            string query = "Delete From BottomGrid where BottomGridID=@bottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@bottomGridID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultBottomGridDto>> GetAllBottomGridAsync()
        {
            // tabloda bulunan tüm kayıtları listeleyen sorgu
            string query = "Select * From BottomGrid";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultBottomGridDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetBottomGridDto> GetBottomGrid(int id)
        {
            // tabloda belirlediğim id'ye sahip kaydı getiren sorgu
            string query = "Select * From BottomGrid where BottomGridID=@bottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@bottomGridID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetBottomGridDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
        {
            // tabloda belirlediğim id'ye sahip kaydı güncelleyen sorgu
            string query = "Update BottomGrid Set Icon=@icon,Title=@title, Description=@description where BottomGridID=@bottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@icon", updateBottomGridDto.Icon);
            parameters.Add("@title", updateBottomGridDto.Title);
            parameters.Add("@description", updateBottomGridDto.Description);
            parameters.Add("@bottomGridID", updateBottomGridDto.BottomGridID);
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
