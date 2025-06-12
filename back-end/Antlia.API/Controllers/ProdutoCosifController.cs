using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Antlia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoCosifCosifController : ControllerBase
    {
        private readonly IProdutoCosifService _ProdutoCosifService;
        public ProdutoCosifCosifController(IProdutoCosifService ProdutoCosifService)
        {
            _ProdutoCosifService = ProdutoCosifService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProdutoCosifDTO model)
        {
            var result = await _ProdutoCosifService.Get(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _ProdutoCosifService.Get(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoCosifDTO model)
        {
            var result = await _ProdutoCosifService.Create(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProdutoCosifDTO model)
        {
            var result = await _ProdutoCosifService.Update(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ProdutoCosifService.Remove(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
