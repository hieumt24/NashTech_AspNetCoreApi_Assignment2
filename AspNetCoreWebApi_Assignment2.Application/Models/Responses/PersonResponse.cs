using AspNetCoreWebApi_Assignment2.Application.Models.DTOs;
using AspNetCoreWebApi_Assignment2.Domain.Entities;

namespace AspNetCoreWebApi_Assignment2.Application.Models.Responses
{
    public class PersonResponse : PersonDTO
    {
        public PersonResponse(Person person)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;
            PhoneNumber = person.PhoneNumber;
            BirthPlace = person.BirthPlace;
            IsGraduated = person.IsGraduated;
        }
    }
}