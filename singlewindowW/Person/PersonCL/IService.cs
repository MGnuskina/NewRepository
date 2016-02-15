using PPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PService
{
    public interface IService
    {
        List<PersonViewModel> ReadAll();
        PersonViewModel GetById(int id);
        void Update(PersonViewModel person);
        void Add(PersonViewModel person);
        void Delete(PersonViewModel person);
    }
}
