using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories
{
    public class PersonRepository : IPerson
    {
        public PersonDto Create(PersonDto person)
        {
            throw new NotImplementedException();
        }

        public PersonDto Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<PersonDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public PersonDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, PersonDto person)
        {
            throw new NotImplementedException();
        }
    }
}
