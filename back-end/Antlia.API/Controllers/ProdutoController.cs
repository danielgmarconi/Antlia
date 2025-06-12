using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Antlia.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Antlia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProdutoDTO model)
        {
            var result = await _produtoService.Get(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _produtoService.Get(id);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProdutoDTO model)
        {
            var result = await _produtoService.Create(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProdutoDTO model)
        {
            var result = await _produtoService.Update(model);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _produtoService.Remove(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
