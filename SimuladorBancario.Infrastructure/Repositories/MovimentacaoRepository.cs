using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Infrastructure.Data;

namespace SimuladorBancario.Infrastructure.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly BancoContext _context;

        public MovimentacaoRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task CriarMovimentacao(Movimentacao movimentacao)
        {
            // Apenas anexa ao DbSet; a persistência será feita pelo serviço em um único SaveChangesAsync.
            await _context.Movimentacoes.AddAsync(movimentacao);
        }

        public async Task<IEnumerable<Movimentacao>> ObterPorContaIdAsync(Guid contaId)
        {
            return await _context.Movimentacoes
                .Where(m => m.ContaId == contaId)
                .OrderByDescending(m => m.DataHora)
                .ToListAsync();
        }
    }
}