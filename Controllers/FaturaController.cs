using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OtomasyonApi.Services;
using OtomasyonApi.DTOs;

namespace OtomasyonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaService _service;

        public FaturaController(IFaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? faturaTipi = null)
        {
            return Ok(await _service.GetAllAsync(faturaTipi));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FaturaCreateDto dto)
        {
            try
            {
                var id = await _service.CreateAsync(dto);
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" | Inner: {ex.InnerException.Message}";
                }
                return StatusCode(500, new { error = errorMessage, stackTrace = ex.StackTrace });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FaturaCreateDto dto)
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

