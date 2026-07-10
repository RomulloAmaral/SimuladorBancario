using Microsoft.AspNetCore.Mvc;
using SimuladorBancario.Application.Interfaces;

namespace SimuladorBancario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteRequest request)
    {
        var id = await _clienteService.CriarClienteAsync(request.Nome, request.CpfCnpj);
        return CreatedAtAction(nameof(ObterPorId), new { id }, new { id });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }
}

public record CriarClienteRequest(string Nome, string CpfCnpj);