using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZavrsniTest.Models;
using ZavrsniTest.Models.DTO;
using ZavrsniTest.Repository.Interfaces;

namespace ZavrsniTest.Repository
{
    public class OglasRepository:IOglasRepository
    {
        private readonly AppDbContext _context;

        public OglasRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Oglas> GetAll()
        {
            return _context.Oglasi.Include(o => o.Agencija).OrderBy(o => o.Naslov);
        }

        public Oglas GetOne(int id)
        {
            return _context.Oglasi.Include(o => o.Agencija).FirstOrDefault(f => f.Id == id);
        }

        public void Create(Oglas oglas)
        {
            _context.Oglasi.Add(oglas);
            _context.SaveChanges();
        }
        public void Update(Oglas oglas)
        {
            _context.Entry(oglas).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public void Delete(Oglas oglas)
        {
            _context.Oglasi.Remove(oglas);
            _context.SaveChanges();
        }

        public IQueryable<Oglas> GetByTip(string tip)
        {
            return _context.Oglasi.Include(o => o.Agencija).Where(o => o.Tip.Contains(tip)).OrderBy(o => o.CenaNekretnine);
        }

        public IQueryable<Oglas> FilterPretraga(OglasFilterDTO dto)
        {
            return _context.Oglasi.Include(o => o.Agencija).Where(o => o.CenaNekretnine >= dto.Najmanje && o.CenaNekretnine <= dto.Najvise).OrderBy(o => o.CenaNekretnine);
        }
    }
}
