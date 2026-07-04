using SimuladorBancario.Domain.Entities;

namespace SimuladorBancario.Application.Interfaces;

public interface IClienteService
{
    Task<Guid> CriarClienteAsync(string nome, string cpfCnpj);
    Task<Cliente?> ObterPorIdAsync(Guid id);
}