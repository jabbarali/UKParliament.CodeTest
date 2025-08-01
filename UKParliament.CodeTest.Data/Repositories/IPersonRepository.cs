// Data/Repositories/IPersonRepository.cs
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int id);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
