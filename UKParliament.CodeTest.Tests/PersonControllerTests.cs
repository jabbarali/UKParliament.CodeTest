using Microsoft.AspNetCore.Mvc;
using Moq;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.Controllers;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;
using Xunit;

namespace UKParliament.CodeTest.Tests;

public class PersonControllerTests
{
    [Fact]
    public async Task Get_ReturnsPersonDto_WhenPersonExists()
    {
        var mockService = new Mock<IPersonService>();
        var person = new Person { Id = 1, FirstName = "John", LastName = "Doe", DepartmentId = 1, DateOfBirth = new DateOnly(1990, 1, 1), Email = "john@parliament.uk" };
        mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(person);

        var controller = new PersonController(mockService.Object, null);

        var result = await controller.Get(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var dto = Assert.IsType<PersonDto>(okResult.Value);
        Assert.Equal(person.Id, dto.Id);
    }
}
