using RealEstate_Dapper_Api.Dtos.AppUserDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.AppUserRepositories
{
    public interface IAppUserRepository
    {
        Task<GetAppUserByProductIdDto> GetAppUserByProductId(int id);
    }
}
