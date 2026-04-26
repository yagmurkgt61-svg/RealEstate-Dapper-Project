using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.SubFeatureDto;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.SubFeatureRepositories
{
    public class SubFeatureRepository : ISubFeatureRepository
    {

        private readonly Context _context;

        public SubFeatureRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultSubFeatureDto>> GetAllSubFeatureAsync()
        {
            // tabloda bulunan tüm kayıtları listeleyen sorgu
            string query = "Select * From SubFeature";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultSubFeatureDto>(query);
                return values.ToList();

            }
        }
    }
}
