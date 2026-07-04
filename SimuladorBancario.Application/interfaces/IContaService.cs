using SimuladorBancario.Domain.Entities;

namespace SimuladorBancario.Application.Interfaces;

public interface IContaService
{
    Task<Guid> CriarContaAsync(Guid clienteId);
    Task DepositarAsync(Guid contaId, decimal valor);
    Task SacarAsync(Guid contaId, decimal valor);
    Task TransferirAsync(Guid contaOrigemId, Guid contaDestinoId, decimal valor);
    Task<decimal> ConsultarSaldoAsync(Guid contaId);
    Task<IEnumerable<Movimentacao>> ObterExtratoAsync(Guid contaId);
}