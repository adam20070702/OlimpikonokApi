using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlimpikonokApi.Models;
using OlimpikonokApi.Services;

namespace OlimpikonokApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrszagController : ControllerBase
    {
        [HttpGet("GetAllOrszagok")]
        public IActionResult GetAllOrszagok()
        {
            try
            {
                List<Orszag> orszagok = new OrszagService().GetAllOrszagok();
                return Ok(orszagok);
            }
            catch
            {
                return StatusCode(500, "hiba történt az országok lekérése során");
            }
        }

        [HttpGet("GetOrszagById/{id}")]
        public IActionResult GetOrszagById(int id)
        {
            try
            {
                Orszag? eredmeny = new OrszagService().GetOrszagById(id);
                if (eredmeny is not null)
                {
                    return Ok(eredmeny);
                }
                else
                {
                    return NotFound("Nincs ilyen ország az adatbázisban");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "hiba történt az ország lekérése során" + ex.Message);
            }
        }
    }
}
