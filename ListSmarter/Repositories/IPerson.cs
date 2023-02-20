using ListSmarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter.Repositories
{
    public interface IPerson
    {
        IList<PersonDto> GetAll();
        PersonDto GetById(int id);
        PersonDto Create(PersonDto person);
        void Update(int id, PersonDto person);
        PersonDto Delete(int id);
    }
}
