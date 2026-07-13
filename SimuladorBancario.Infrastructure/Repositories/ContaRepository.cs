using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Infrastructure.Data;

namespace SimuladorBancario.Infrastructure.Repositories;

public class ContaRepository : IContaRepository
{
    private readonly BancoContext _context;

    public ContaRepository(BancoContext context)
    {
        _context = context;
    }

    public async Task<Conta?> ObterPorIdAsync(Guid id)
    {
        
        return await _context.Contas
            .Include(c => c.Movimentacoes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Conta?> ObterPorClienteIdAsync(Guid clienteId)
    {
        return await _context.Contas
            .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
    }

    public async Task AdicionarAsync(Conta conta)
    {
        await _context.Contas.AddAsync(conta);
    }

    public async Task SalvarAsync()
    {
        await _context.SaveChangesAsync();
    }
}