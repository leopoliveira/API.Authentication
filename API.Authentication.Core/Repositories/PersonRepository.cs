using API.Authentication.Core.Entities;

namespace API.Authentication.Core.Repositories
{
    public class PersonRepository
    {
        private IList<Person> Persons = new List<Person>();

        public PersonRepository()
        {
            Persons.Add(new Person { Id = 1, Name = "John Doe", Age = 30, CPF = "12345678900", City = "New York" });
            Persons.Add(new Person { Id = 2, Name = "Jane Doe", Age = 25, CPF = "09876543211", City = "New York" });
            Persons.Add(new Person { Id = 3, Name = "Jack Doe", Age = 20, CPF = "12312312399", City = "New York" });
            Persons.Add(new Person { Id = 4, Name = "Jill Doe", Age = 15, CPF = "98798798711", City = "New York" });
        }

        public Person GetPersonById(int id)
        {
            return Persons.FirstOrDefault(p => p.Id == id);
        }

        public Person GetPersonByCPF(string cpf)
        {
            return Persons.FirstOrDefault(p => p.CPF == cpf);
        }

        public Person GetPersonByName(string name)
        {
            return Persons.FirstOrDefault(p => p.Name.Contains(name));
        }

        public IList<Person> GetPersons()
        {
            return Persons;
        }

        public bool PersonExists(int id)
        {
            return Persons.Any(p => p.Id == id);
        }

        public bool PersonExists(string cpf)
        {
            return Persons.Any(p => p.CPF == cpf);
        }

        public void AddPerson(Person person)
        {
            Persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            var personToUpdate = Persons.FirstOrDefault(p => p.Id == person.Id);

            if (personToUpdate != null)
            {
                personToUpdate.Name = person.Name;
                personToUpdate.Age = person.Age;
                personToUpdate.CPF = person.CPF;
                personToUpdate.City = person.City;
            }
        }

        public void DeletePerson(int id)
        {
            var personToDelete = Persons.FirstOrDefault(p => p.Id == id);

            if (personToDelete != null)
            {
                Persons.Remove(personToDelete);
            }
        }
    }
}
