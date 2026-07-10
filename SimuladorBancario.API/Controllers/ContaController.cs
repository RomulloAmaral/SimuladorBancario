using Microsoft.AspNetCore.Mvc;
using SimuladorBancario.Application.Interfaces;

namespace SimuladorBancario.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaController : ControllerBase
{
    private readonly IContaService _contaService;

    public ContaController(IContaService contaService)
    {
        _contaService = contaService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarContaRequest request)
    {
        var id = await _contaService.CriarContaAsync(request.ClienteId);
        return CreatedAtAction(nameof(ConsultarSaldo), new { id }, new { id });
    }

    [HttpPost("{id}/deposito")]
    public async Task<IActionResult> Depositar(Guid id, [FromBody] ValorRequest request)
    {
        await _contaService.DepositarAsync(id, request.Valor);
        return NoContent();
    }

    [HttpPost("{id}/saque")]
    public async Task<IActionResult> Sacar(Guid id, [FromBody] ValorRequest request)
    {
        await _contaService.SacarAsync(id, request.Valor);
        return NoContent();
    }

    [HttpPost("transferencia")]
    public async Task<IActionResult> Transferir([FromBody] TransferenciaRequest request)
    {
        await _contaService.TransferirAsync(
            request.ContaOrigemId,
            request.ContaDestinoId,
            request.Valor);
        return NoContent();
    }

    [HttpGet("{id}/saldo")]
    public async Task<IActionResult> ConsultarSaldo(Guid id)
    {
        var saldo = await _contaService.ConsultarSaldoAsync(id);
        return Ok(new { saldo });
    }

    [HttpGet("{id}/extrato")]
    public async Task<IActionResult> ObterExtrato(Guid id)
    {
        var extrato = await _contaService.ObterExtratoAsync(id);
        return Ok(extrato);
    }
}

public record CriarContaRequest(Guid ClienteId);
public record ValorRequest(decimal Valor);
public record TransferenciaRequest(Guid ContaOrigemId, Guid ContaDestinoId, decimal Valor);