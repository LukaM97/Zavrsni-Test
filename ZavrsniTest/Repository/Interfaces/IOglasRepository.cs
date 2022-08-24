using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZavrsniTest.Models;
using ZavrsniTest.Models.DTO;

namespace ZavrsniTest.Repository.Interfaces
{
    public interface IOglasRepository
    {
        IQueryable<Oglas> GetAll();
        Oglas GetOne(int id);
        void Create(Oglas oglas);
        void Update(Oglas oglas);
        void Delete(Oglas oglas);
        IQueryable<Oglas> GetByTip(string tip);
        IQueryable<Oglas> FilterPretraga(OglasFilterDTO dto);
    }
}
