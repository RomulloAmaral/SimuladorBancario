using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Infrastructure.Data;

namespace SimuladorBancario.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly BancoContext _context;

    public ClienteRepository(BancoContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente?> ObterPorCpfCnpjAsync(string cpfCnpj)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.CpfCnpj == cpfCnpj);
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}