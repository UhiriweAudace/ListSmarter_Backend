using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Services
{
    public interface IPersonService
    {
        IList<PersonDto> GetPersons();
        PersonDto GetPerson(int id);
        PersonDto CreatePerson(PersonDto person);
        void UpdatePerson(int personId, PersonDto person);
        PersonDto DeletePerson(int personId);
    }
}
