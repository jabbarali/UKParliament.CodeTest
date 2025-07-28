using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Repositories;
using Xunit;

namespace UKParliament.CodeTest.Tests;

public class PersonRepositoryTests
{
    private PersonManagerContext GetContext()
    {
        var options = new DbContextOptionsBuilder<PersonManagerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new PersonManagerContext(options);
    }

    [Fact]
    public async Task AddAsync_AddsPerson()
    {
        using var context = GetContext();
        var repo = new PersonRepository(context);
        var person = new Person { FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1990, 1, 1), DepartmentId = 1, Email = "jane@parliament.uk" };

        await repo.AddAsync(person);

        Assert.Single(context.People);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPersons()
    {
        using var context = GetContext();
        
        context.Departments.Add(new Department { Id = 1, Name = "Dept1" });
        context.Departments.Add(new Department { Id = 2, Name = "Dept2" });
        await context.SaveChangesAsync();

        context.People.Add(new Person { FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1990, 1, 1), DepartmentId = 1, Email = "jane@parliament.uk" });
        context.People.Add(new Person { FirstName = "John", LastName = "Doe", DateOfBirth = new DateOnly(1985, 5, 5), DepartmentId = 2, Email = "john@parliament.uk" });
        await context.SaveChangesAsync();

        var repo = new PersonRepository(context);

        var result = await repo.GetAllAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectPerson()
    {
        using var context = GetContext();
        context.Departments.Add(new Department { Id = 1, Name = "Dept1" });
        await context.SaveChangesAsync();

        var person = new Person { FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1990, 1, 1), DepartmentId = 1, Email = "jane@parliament.uk" };
        context.People.Add(person);
        await context.SaveChangesAsync();

        var repo = new PersonRepository(context);

        var result = await repo.GetByIdAsync(person.Id);

        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesPerson()
    {
        using var context = GetContext();
        var person = new Person { FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1990, 1, 1), DepartmentId = 1, Email = "jane@parliament.uk" };
        context.People.Add(person);
        await context.SaveChangesAsync();

        var repo = new PersonRepository(context);
        person.LastName = "Doe";
        await repo.UpdateAsync(person);

        var updated = await context.People.FindAsync(person.Id);
        Assert.Equal("Doe", updated.LastName);
    }

    [Fact]
    public async Task DeleteAsync_RemovesPerson()
    {
        using var context = GetContext();
        var person = new Person { FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateOnly(1990, 1, 1), DepartmentId = 1, Email = "jane@parliament.uk" };
        context.People.Add(person);
        await context.SaveChangesAsync();

        var repo = new PersonRepository(context);
        await repo.DeleteAsync(person.Id);

        Assert.Empty(context.People);
    }
}
