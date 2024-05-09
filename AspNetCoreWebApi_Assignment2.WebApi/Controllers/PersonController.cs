using AspNetCoreWebApi_Assignment2.Application.Interfaces;
using AspNetCoreWebApi_Assignment2.Application.Models.DTOs;
using AspNetCoreWebApi_Assignment2.Domain.Entities;
using AspNetCoreWebApi_Assignment2.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi_Assignment2.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Get all list per
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("persons")]
        public IActionResult GetAllPerson()
        {
            var listPerson = _personService.GetAllPerson();
            return Ok(listPerson);
        }

        /// <summary>
        /// Get person by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("person/{id}")]
        public IActionResult GetPersonById([FromRoute] Guid id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound("No person found with given id");
            }
            return Ok(person);
        }

        /// <summary>
        /// Add new person
        /// </summary>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("person")]
        public IActionResult AddPerson([FromBody] PersonDTO personDto)
        {
            Person newPerson = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Gender = personDto.Gender,
                DateOfBirth = personDto.DateOfBirth,
                PhoneNumber = personDto.PhoneNumber,
                BirthPlace = personDto.BirthPlace,
                IsGraduated = personDto.IsGraduated
            };
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state person");
            }
            var personAdded = _personService.AddPerson(newPerson);
            if (personAdded == null)
            {
                return BadRequest("Person already existed");
            }
            var personUpdateDto = new PersonDTO
            {
                FirstName = newPerson.FirstName,
                LastName = newPerson.LastName,
                Gender = newPerson.Gender,
                DateOfBirth = newPerson.DateOfBirth,
                PhoneNumber = newPerson.PhoneNumber,
                BirthPlace = newPerson.BirthPlace,
                IsGraduated = newPerson.IsGraduated
            };
            return Ok(personUpdateDto);
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("person/{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody] PersonDTO personDto)
        {
            Person person = new Person
            {
                Id = id,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Gender = personDto.Gender,
                DateOfBirth = personDto.DateOfBirth,
                PhoneNumber = personDto.PhoneNumber,
                BirthPlace = personDto.BirthPlace,
                IsGraduated = personDto.IsGraduated
            };
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state person");
            }
            var personUpdated = _personService.UpdatePerson(person);
            if (personUpdated == null)
            {
                return BadRequest("No person found with given id");
            }

            var personUpdateDto = new PersonDTO
            {
                FirstName = personUpdated.FirstName,
                LastName = personUpdated.LastName,
                Gender = personUpdated.Gender,
                DateOfBirth = personUpdated.DateOfBirth,
                PhoneNumber = personUpdated.PhoneNumber,
                BirthPlace = personUpdated.BirthPlace,
                IsGraduated = personUpdated.IsGraduated
            };

            return Ok(personUpdateDto);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("person/{id}")]
        public IActionResult DeletePerson(Guid id)
        {
            try
            {
                _personService.DeletePerson(id);
                return Ok("Person deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Filter by name, gender, birthPlace
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="birthPlace"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("persons/filter")]
        public IActionResult FilterPersons([FromQuery] string name, [FromQuery] GenderType? gender, [FromQuery] string birthPlace)
        {
            try
            {
                var filteredPersons = _personService.FilterPeople(name, gender, birthPlace);

                if (filteredPersons == null || !filteredPersons.Any())
                {
                    return NotFound("No persons found with the given criteria");
                }

                return Ok(filteredPersons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
    }
}