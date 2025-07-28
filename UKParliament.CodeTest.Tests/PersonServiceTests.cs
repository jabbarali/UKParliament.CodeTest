using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories;
using UKParliament.CodeTest.Services;
using Xunit;

namespace UKParliament.CodeTest.Tests;

public class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _repoMock;
    private readonly PersonService _service;

    public PersonServiceTests()
    {
        _repoMock = new Mock<IPersonRepository>();
        _service = new PersonService(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPersons()
    {
        var persons = new List<Person> { new Person { Id = 1, FirstName = "John", LastName = "Doe" } };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(persons);

        var result = await _service.GetAllAsync();

        Assert.Equal(persons, result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPerson()
    {
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(person);

        var result = await _service.GetByIdAsync(1);

        Assert.Equal(person, result);
    }

    [Fact]
    public async Task CreateAsync_CallsAddAsync()
    {
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };

        await _service.CreateAsync(person);

        _repoMock.Verify(r => r.AddAsync(person), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_CallsUpdateAsync()
    {
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe" };

        await _service.UpdateAsync(person);

        _repoMock.Verify(r => r.UpdateAsync(person), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_CallsDeleteAsync()
    {
        await _service.DeleteAsync(1);

        _repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}
