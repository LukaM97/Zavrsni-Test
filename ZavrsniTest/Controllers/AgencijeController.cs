using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZavrsniTest.Models;
using ZavrsniTest.Repository.Interfaces;

namespace ZavrsniTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencijeController : ControllerBase
    {
        private readonly IAgencijaRepository _agencijaRepository;

        public AgencijeController(IAgencijaRepository agencijaRepository)
        {
            _agencijaRepository = agencijaRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_agencijaRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Agencija agencija = _agencijaRepository.GetOne(id);
            if (agencija == null)
            {
                return NotFound();
            }

            return Ok(agencija);
        }

        [HttpGet("/api/prodaja/granica/{vrednost}")]
        public IActionResult GetSumbyGranica(int vrednost)
        {
            return Ok(_agencijaRepository.GetSumbyGranica(vrednost));
        }

        [HttpGet("/api/brojnost")]
        public IActionResult GetBrojnost()
        {
            return Ok(_agencijaRepository.GetBrojnost());
        }

        [HttpGet("trazi/{vrednost}")]
        public IActionResult GetByNaziv(string vrednost)
        {
            return Ok(_agencijaRepository.GetByNaziv(vrednost));
        }
    }
}
