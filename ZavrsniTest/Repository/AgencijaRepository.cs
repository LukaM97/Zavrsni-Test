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
    public class AgencijaRepository:IAgencijaRepository
    {
        private readonly AppDbContext _context;

        public AgencijaRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Agencija> GetAll()
        {
            return _context.Agencije.OrderBy(f => f.Naziv);
        }

        public Agencija GetOne(int id)
        {
            return _context.Agencije.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<AgencijaSumaDTO> GetSumbyGranica(int granica)
        {
            
            var lista = _context.Agencije.GroupBy(a => a.Id)
                .Select(r => new AgencijaSumaDTO
                {
                    Agencija = _context.Agencije.Where(k => k.Id == r.Key).Select(k => k.Naziv).Single(),
                    SumaCena = _context.Oglasi.Where(p => p.AgencijaId == r.Key).Select(p => p.CenaNekretnine).Sum()
                }).ToList().OrderBy(r => r.Agencija).Where(r => r.SumaCena > granica);

            return lista;
        }

        public IEnumerable<AgencijaBrojOglasaDTO> GetBrojnost()
        {
            var lista = _context.Agencije.GroupBy(a => a.Id)
                .Select(r => new AgencijaBrojOglasaDTO
                {
                    Agencija = _context.Agencije.Where(k => k.Id == r.Key).Select(k => k.Naziv).Single(),
                    BrojOglasa = _context.Oglasi.Where(p => p.AgencijaId == r.Key).Count()
                }).ToList().OrderByDescending(r => r.BrojOglasa);

            return lista;
        }

        public IQueryable<Agencija> GetByNaziv(string naziv)
        {
            return _context.Agencije.Where(a => a.Naziv == naziv).OrderBy(a => a.GodinaOsnivanja).ThenByDescending(a => a.Naziv);
        }
    }
}
