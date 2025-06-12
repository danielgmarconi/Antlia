using Antlia.Application.DTOs;
using Antlia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Antlia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovimentoManualController : ControllerBase
{
    private readonly IMovimentoManualService _movimentoManualService;
    public MovimentoManualController(IMovimentoManualService movimentoManualService)
    {
        _movimentoManualService = movimentoManualService;
    }
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] MovimentoManualDTO model)
    {
        var result = await _movimentoManualService.Get(model);
        return StatusCode(result.StatusCode, result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _movimentoManualService.Get(id);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MovimentoManualDTO model)
    {
        var result = await _movimentoManualService.Create(model);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] MovimentoManualDTO model)
    {
        var result = await _movimentoManualService.Update(model);
        return StatusCode(result.StatusCode, result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _movimentoManualService.Remove(id);
        return StatusCode(result.StatusCode, result);
    }
}
