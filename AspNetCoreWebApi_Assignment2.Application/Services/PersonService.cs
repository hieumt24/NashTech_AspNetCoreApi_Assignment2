using AspNetCoreWebApi_Assignment2.Application.Interfaces;
using AspNetCoreWebApi_Assignment2.Domain.Entities;
using AspNetCoreWebApi_Assignment2.Domain.Enums;
using AspNetCoreWebApi_Assignment2.Domain.Interface;

namespace AspNetCoreWebApi_Assignment2.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person AddPerson(Person person)
        {
            Person personExisted = _personRepository.GetPersonById(person.Id);
            if (personExisted != null)
            {
                return null;
            }
            return _personRepository.AddPerson(person);
        }

        public void DeletePerson(Guid id)
        {
            Person personExisted = _personRepository.GetPersonById(id);
            if (personExisted == null)
            {
                throw new Exception("Person does not exist.");
            }
            _personRepository.DeletePerson(id);
        }

        public IEnumerable<Person> FilterPeople(string name, GenderType? gender, string birthPlace)
        {
            var filterPeople = _personRepository.FilterPeople(name, gender, birthPlace);
            if (filterPeople == null)
            {
                throw new Exception("No persons found with the given.");
            }
            return filterPeople;
        }

        public IEnumerable<Person> GetAllPerson()
        {
            return _personRepository.GetAllPerson();
        }

        public Person GetPersonById(Guid id)
        {
            return _personRepository.GetPersonById(id);
        }

        public Person UpdatePerson(Person person)
        {
            Person personExisted = _personRepository.GetPersonById(person.Id);
            if (personExisted == null)
            {
                return null;
            }
            return _personRepository.UpdatePerson(person);
        }
    }
}