using SimuladorBancario.Domain.Entities;

namespace SimuladorBancario.Application.Interfaces;

public interface IContaRepository
{
    Task<Conta?> ObterPorIdAsync(Guid id);
    Task<Conta?> ObterPorClienteIdAsync(Guid clienteId);
    Task AdicionarAsync(Conta conta);
    Task SalvarAsync();
}