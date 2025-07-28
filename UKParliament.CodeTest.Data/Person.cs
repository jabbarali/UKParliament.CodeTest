namespace UKParliament.CodeTest.Data;

using System.ComponentModel.DataAnnotations;

public class Person
{
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; } = default!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = default!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = default!;

    [Required, EmailAddress]
    public string Email { get; set; } = default!;
}