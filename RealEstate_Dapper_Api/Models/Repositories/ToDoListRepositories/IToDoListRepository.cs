
using RealEstate_Dapper_Api.Dtos.ToDoListDtos;

namespace RealEstate_Dapper_Api.Models.Repositories.ToDoListRepositories
{
    public interface IToDoListRepository
    {
        Task<List<ResultToDoListDto>> GetAllToDoListAsync();
        void CreateToDoList(CreateToDoListDto toDoListDto);
        void DeleteToDoList(int id);
        void UpdateToDoList(UpdateToDoListDto toDoListDto);
        Task<GetByIDToDoListDto> GetToDoList(int id);
    }
}
