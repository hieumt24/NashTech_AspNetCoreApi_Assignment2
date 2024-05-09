using AspNetCoreWebApi_Assignment2.Domain.Enums;

namespace AspNetCoreWebApi_Assignment2.Application.Models.DTOs
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }

        public int Age => DateTime.Now.Year - DateOfBirth.Year;
    }
}