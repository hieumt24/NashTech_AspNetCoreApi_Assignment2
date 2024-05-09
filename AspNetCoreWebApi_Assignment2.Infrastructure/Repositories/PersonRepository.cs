using AspNetCoreWebApi_Assignment2.Domain.Entities;
using AspNetCoreWebApi_Assignment2.Domain.Enums;
using AspNetCoreWebApi_Assignment2.Domain.Interface;

namespace AspNetCoreWebApi_Assignment2.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public Person AddPerson(Person person)
        {
            MyDatabase.listPeoples.Add(person);
            return person;
        }

        public void DeletePerson(Guid id)
        {
            var personDelete = MyDatabase.listPeoples.Find(x => x.Id == id);
            if (personDelete == null)
            {
                throw new KeyNotFoundException("No person found with the given id");
            }
            MyDatabase.listPeoples.Remove(personDelete);
        }

        public IEnumerable<Person> FilterPeople(string name, GenderType? gender, string birthPlace)
        {
            var filteredPeople = MyDatabase.listPeoples
                                    .Where(p => string.IsNullOrEmpty(name) || (p.FirstName.ToLower()).Contains(name.ToLower()) || p.LastName.ToLower().Contains(name.ToLower()))
                                    .Where(p => !gender.HasValue || p.Gender == gender.Value)
                                    .Where(p => string.IsNullOrEmpty(birthPlace) || p.BirthPlace.ToLower().Contains(birthPlace.ToLower(), StringComparison.Ordinal));
            if (filteredPeople == null || !filteredPeople.Any())
            {
                throw new KeyNotFoundException("No persons found with the given");
            }
            return filteredPeople;
        }

        public IEnumerable<Person> GetAllPerson()
        {
            return MyDatabase.listPeoples;
        }

        public Person GetPersonById(Guid id)
        {
            return MyDatabase.listPeoples.Find(x => x.Id == id);
        }

        public Person UpdatePerson(Person person)
        {
            var personToUpdate = MyDatabase.listPeoples.FirstOrDefault(x => x.Id == person.Id);
            if (personToUpdate == null)
            {
                throw new KeyNotFoundException("No person found with the given id");
            }

            personToUpdate.FirstName = person.FirstName;
            personToUpdate.LastName = person.LastName;
            personToUpdate.Gender = person.Gender;
            personToUpdate.DateOfBirth = person.DateOfBirth;
            personToUpdate.PhoneNumber = person.PhoneNumber;
            personToUpdate.BirthPlace = person.BirthPlace;
            personToUpdate.IsGraduated = person.IsGraduated;

            return personToUpdate;
        }
    }
}