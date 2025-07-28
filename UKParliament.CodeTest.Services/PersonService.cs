using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repo;
        public PersonService(IPersonRepository repo) => _repo = repo;

        public Task<List<Person>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Person?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<Person> CreateAsync(Person person)
        {
            await _repo.AddAsync(person);
            return person;
        }
        public async Task<Person> UpdateAsync(Person person)
        {
            await _repo.UpdateAsync(person);
            return person;
        }
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}