using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                DepartmentId = person.DepartmentId,
                DepartmentName = person.Department?.Name ?? "",
                Email = person.Email
            };
        }

        public static Person ToEntity(PersonDto dto)
        {
            return new Person
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                DepartmentId = dto.DepartmentId,
                Email = dto.Email
            };
        }
    }
}