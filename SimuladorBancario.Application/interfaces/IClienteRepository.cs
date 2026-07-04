namespace SimuladorBancario.Application.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> ObterClientePorIdAsync(Guid id);
    Task<Cliente> ObterClientePorCpfCnpjAsync(string cpfCnpj);

    Task AdicionarClienteAsync(Cliente cliente);

    Task SalvarAsync();

    
}