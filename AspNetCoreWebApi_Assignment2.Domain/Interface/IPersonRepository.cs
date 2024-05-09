using AspNetCoreWebApi_Assignment2.Domain.Entities;
using AspNetCoreWebApi_Assignment2.Domain.Enums;

namespace AspNetCoreWebApi_Assignment2.Domain.Interface
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPerson();

        Person AddPerson(Person person);

        Person UpdatePerson(Person person);

        void DeletePerson(Guid id);

        Person GetPersonById(Guid id);

        IEnumerable<Person> FilterPeople(string name, GenderType? gender, string birthPlace);
    }
}