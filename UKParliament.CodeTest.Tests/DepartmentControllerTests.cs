using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.Controllers;
using Xunit;
using System.Linq;

namespace UKParliament.CodeTest.Tests;

public class DepartmentControllerTests
{
    private PersonManagerContext GetContext()
    {
        var options = new DbContextOptionsBuilder<PersonManagerContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
        return new PersonManagerContext(options);
    }

    [Fact]
    public void Get_ReturnsAllDepartments()
    {
        using var context = GetContext();
        context.Departments.Add(new Department { Id = 1, Name = "Dept1" });
        context.Departments.Add(new Department { Id = 2, Name = "Dept2" });
        context.SaveChanges();

        var controller = new DepartmentController(context);

        var result = controller.Get() as OkObjectResult;
        Assert.NotNull(result);

        var departments = result.Value as System.Collections.Generic.List<Department>;
        Assert.NotNull(departments);
        Assert.Equal(2, departments.Count);
        Assert.Contains(departments, d => d.Name == "Dept1");
        Assert.Contains(departments, d => d.Name == "Dept2");
    }
}
