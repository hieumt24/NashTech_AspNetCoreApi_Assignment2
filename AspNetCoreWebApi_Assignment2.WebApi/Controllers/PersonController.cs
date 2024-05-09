using AspNetCoreWebApi_Assignment2.Application.Interfaces;
using AspNetCoreWebApi_Assignment2.Application.Models.Requests;
using AspNetCoreWebApi_Assignment2.Application.Models.Responses;
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
        /// Get all list perons
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
        public IActionResult AddPerson([FromBody] PersonRequest personRequest)
        {
            Person newPerson = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = personRequest.FirstName,
                LastName = personRequest.LastName,
                Gender = personRequest.Gender,
                DateOfBirth = personRequest.DateOfBirth,
                PhoneNumber = personRequest.PhoneNumber,
                BirthPlace = personRequest.BirthPlace,
                IsGraduated = personRequest.IsGraduated
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

            return Ok(new PersonResponse(personAdded));
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("person/{id}")]
        public IActionResult UpdatePerson(Guid id, [FromBody] PersonRequest personRequest)
        {
            Person person = new Person
            {
                Id = id,
                FirstName = personRequest.FirstName,
                LastName = personRequest.LastName,
                Gender = personRequest.Gender,
                DateOfBirth = personRequest.DateOfBirth,
                PhoneNumber = personRequest.PhoneNumber,
                BirthPlace = personRequest.BirthPlace,
                IsGraduated = personRequest.IsGraduated
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

            return Ok(new PersonResponse(personUpdated));
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