using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public  interface IRepo<pL>
    {
        List<pL> GetAll();
        pL GetById(int id);
        void Create(pL playlist);
        void view(pL playlist, int id);
        void Delete(int id);
        void Save();
    }
}
