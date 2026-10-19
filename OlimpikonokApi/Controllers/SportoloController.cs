using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OlimpikonokApi.DTOs;
using OlimpikonokApi.Models;
using OlimpikonokApi.Services;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportoloController : ControllerBase
    {
        [HttpGet("GetAllSportolok")]
        public IActionResult GetAllSportolook()
        {
            try
            {
                List<Sportolo> Sportolok = new SportoloService().GetAllSportolok();
                return Ok(Sportolok);
            }
            catch
            {
                return StatusCode(500, "hiba történt az sportolóok lekérése során");
            }
        }

        [HttpGet("GetSportoloById/{id}")]
        public IActionResult GetSportoloById(int id)
        {
            try
            {
                Sportolo? eredmeny = new SportoloService().GetSportoloById(id);
                if (eredmeny is not null)
                {
                    return Ok(eredmeny);
                }
                else
                {
                    return NotFound("Nincs ilyen sportoló az adatbázisban");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hiba történt az sportoló lekérése során" + ex.Message);
            }
        }
        [HttpGet("GetAllSportoloDTO")]
        public IActionResult GetAllSportoloDTO()
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    List<SportoloDTO> dtok = context.Sportolos.Include(s => s.Sportag).Include(s=>s.Orszag)
                        .Select(s => new SportoloDTO
                        {
                            Id = s.Id,
                            Nev = s.Nev,
                            SportagNev = s.Sportag.Megnevezes,
                            Ermek = s.Ermek,
                            Orszagnev = s.Orszag.Nev
                        }).ToList();
                    return Ok(dtok);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hiba a kérés teljesítése közben" + ex.Message);
            }
        }
        [HttpGet("GetSportoloDTObyid/{id}")]
        public IActionResult GetSportoloDTOById(int id)
        {
            try
            {
                using (var context = new OlimpikonokContext())
                {
                    SportoloDTO? dtok = context.Sportolos.Include(s => s.Sportag).Include(s => s.Orszag)
                        .Select(s => new SportoloDTO
                        {
                            Id = s.Id,
                            Nev = s.Nev,
                            SportagNev = s.Sportag.Megnevezes,
                            Ermek = s.Ermek,
                            Orszagnev = s.Orszag.Nev
                        }).FirstOrDefault(s => s.Id == id);
                    if (dtok is not null)
                    {
                        return Ok(dtok);
                    }
                    else
                    {
                        return NotFound("Nincs ilyen sportoló az adatbázisban");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hiba a kérés teljesítése közben" + ex.Message);
            }
        }
    }
}
