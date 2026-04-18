using Dapper;
using RealEstate_Dapper_Api.Dtos.CityDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.CityRespositories
{
    public class CityRepository : ICityRepository
    {
        private readonly Context _context;

        public CityRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultCityDto>> GetAllCityAsync()
        {
            string query = "Select * From City";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCityDto>(query);
                return values.ToList();

            }
        }
    }
}
