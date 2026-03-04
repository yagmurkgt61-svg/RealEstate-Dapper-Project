using RealEstate_Dapper_Api.Dtos.ContactDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.ContactRepositories
{
    public interface IContactRepository
    {   
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task<List<Last4ContactResultDto>> GetLast4ContactAsync();
        void CreateContact(CreateContactDto ContactDto);
        void DeleteContact(int id);
        Task<GetByIDContactDto> GetContact(int id);
    }
}
