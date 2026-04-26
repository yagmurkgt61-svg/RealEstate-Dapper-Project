using RealEstate_Dapper_Api.Dtos.SubFeatureDto;

namespace RealEstate_Dapper_Api.Models.Repositories.SubFeatureRepositories
{
    public interface ISubFeatureRepository
    {
        Task<List<ResultSubFeatureDto>> GetAllSubFeatureAsync();
    }
}
