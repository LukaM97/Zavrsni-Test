using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZavrsniTest.Models;
using ZavrsniTest.Models.DTO;
using ZavrsniTest.Repository.Interfaces;

namespace ZavrsniTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OglasiController : ControllerBase
    {
        private readonly IOglasRepository _oglasRepository;

        public OglasiController(IOglasRepository oglasRepository)
        {
            _oglasRepository = oglasRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_oglasRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Oglas oglas = _oglasRepository.GetOne(id);
            if (oglas == null)
            {
                return NotFound();
            }

            return Ok(oglas);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Kreiraj(Oglas oglas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _oglasRepository.Create(oglas);

            return CreatedAtAction("GetOne", new { id = oglas.Id }, oglas);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Oglas oglas)
        {

            if (id != oglas.Id)
            {
                return BadRequest();
            }

            try
            {
                _oglasRepository.Update(oglas);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(oglas);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Oglas oglas = _oglasRepository.GetOne(id);
            if (oglas == null)
            {
                return NotFound();
            }

            _oglasRepository.Delete(oglas);
            return NoContent();
        }

        [HttpGet("trazi/{vrednost}")]
        public IActionResult GetByTip(string vrednost)
        {
            return Ok(_oglasRepository.GetByTip(vrednost));
        }

        [Authorize]
        [Route("/api/pretraga")]
        [HttpPost]
        public IActionResult FilterPretraga(OglasFilterDTO dto)
        {
            if(dto.Najmanje > dto.Najvise)
            {
                return BadRequest();
            }

            return Ok(_oglasRepository.FilterPretraga(dto));
        }


    }
}
