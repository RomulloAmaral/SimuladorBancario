using SimuladorBancario.Domain.Entities;

namespace SimuladorBancario.Application.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<Cliente?> ObterPorCpfCnpjAsync(string cpfCnpj);
    Task AdicionarAsync(Cliente cliente);
    Task SalvarAsync();
}