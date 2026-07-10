using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Domain.Enums;

namespace SimuladorBancario.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepo;
    private static TipoMovimentacao TipoMovimentacaoPadrao => TipoMovimentacao.Deposito;

    public ClienteService(IClienteRepository clienteRepo)
    {
        _clienteRepo = clienteRepo;
    }

    public async Task<Guid> CriarClienteAsync(string nome, string cpfCnpj)
    {
        var existente = await _clienteRepo.ObterPorCpfCnpjAsync(cpfCnpj);
        if (existente != null)
            throw new InvalidOperationException("CPF/CNPJ já cadastrado.");

        var cliente = new Cliente(nome, cpfCnpj);
        await _clienteRepo.AdicionarAsync(cliente);
        await _clienteRepo.SalvarAsync();
        return cliente.Id;
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        return await _clienteRepo.ObterPorIdAsync(id);
    }
}