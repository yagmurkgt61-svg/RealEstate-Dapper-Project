using RealEstate_Dapper_Api.Dtos.PropertyAmenityDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.PropertyAmenityRepositories
{
    public interface IPropertyAmenityRepository
    {
       Task<List<ResultPropertyAmenityByStatusTrueDto>> ResultPropertyAmenityByStatusTrue(int id);
    }
}
