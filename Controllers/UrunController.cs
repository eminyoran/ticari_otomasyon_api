using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OtomasyonApi.Services;
using OtomasyonApi.DTOs;

namespace OtomasyonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UrunController : ControllerBase
    {
        private readonly IUrunService _service;

        public UrunController(IUrunService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UrunCreateDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UrunCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

