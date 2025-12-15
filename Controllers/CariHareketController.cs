using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OtomasyonApi.Services;
using OtomasyonApi.DTOs;

namespace OtomasyonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CariHareketController : ControllerBase
    {
        private readonly ICariHareketService _service;

        public CariHareketController(ICariHareketService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetList(
            [FromQuery] int? cariId,
            [FromQuery] DateTime? baslangic,
            [FromQuery] DateTime? bitis)
        {
            var result = await _service.GetListAsync(cariId, baslangic, bitis);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CariHareketCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return NotFound();
            return Ok();
        }
    }
}
