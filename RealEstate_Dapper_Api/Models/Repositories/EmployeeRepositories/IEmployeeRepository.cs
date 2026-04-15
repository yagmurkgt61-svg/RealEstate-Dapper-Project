using RealEstate_Dapper_Api.Dtos.EmployeeDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<List<ResultEmployeeDto>> GetAllEmployeeAsync();
        void CreateEmployee(CreateEmployeeDto EmployeeDto);
        void DeleteEmployee(int id);
        void UpdateEmployee(UpdateEmployeeDto EmployeeDto);
        Task<GetByIdEmployeeDto> GetEmployee(int id);
    }
}
