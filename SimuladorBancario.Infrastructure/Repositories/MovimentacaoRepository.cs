using Microsoft.EntityFrameworkCore;
using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           
            await _context.Movimentacoes.AddAsync(movimentacao);
          
        }
    }
}
