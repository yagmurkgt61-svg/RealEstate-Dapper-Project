using Dapper;
using RealEstate_Dapper_Api.Dtos.ToDoListDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Models.Repositories.ToDoListRepositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly Context _concext;
        public ToDoListRepository(Context concext)
        {
            _concext = concext;
        }
        public void CreateToDoList(CreateToDoListDto toDoListDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteToDoList(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultToDoListDto>> GetAllToDoListAsync()
        {
            string query = "Select * From ToDoList";
            using (var connection = _concext.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDto>(query);
                return values.ToList();

            }
        }

        public Task<GetByIDToDoListDto> GetToDoList(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateToDoList(UpdateToDoListDto toDoListDto)
        {
            throw new NotImplementedException();
        }
    }
}
