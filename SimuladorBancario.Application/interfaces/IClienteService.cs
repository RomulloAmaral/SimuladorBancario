namespace SimuladorBancario.Application.Interfaces;

public interface IClienteRepository
{
    Task<Guid> ObterClienteIdPorCpfCnpjAsync(string cpfCnpj);
    Task<Cliente> ObterClientePorIdAsync(Guid id);
}