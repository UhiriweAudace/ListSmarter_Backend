using ListSmarter.Models;
using ListSmarter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ListSmarter.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPerson _personRepository;
        private readonly IValidator<PersonDto> _personValidator;
        public PersonService(IPerson person, IValidator<PersonDto> personValidator)
        {
            _personRepository = person;
            _personValidator = personValidator ?? throw new ArgumentException();
        }
        public PersonDto CreatePerson(PersonDto person)
        {
            return _personRepository.Create(person);
        }

        public PersonDto DeletePerson(int personId)
        {
            return _personRepository.Delete(personId);
        }

        public PersonDto GetPerson(int id)
        {
            return _personRepository.GetById(id);
        }

        public IList<PersonDto> GetPersons()
        {
            return _personRepository.GetAll();
        }

        public void UpdatePerson(int personId, PersonDto person)
        {
            var validatePerson = _personValidator.Validate(person);
            if (validatePerson.IsValid)
            {
                _personRepository.Update(personId, person);
            }
        }
    }
}
