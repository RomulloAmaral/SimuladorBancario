using SimuladorBancario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorBancario.Application.Interfaces
{
    public interface IMovimentacaoRepository
    {
        Task CriarMovimentacao(Movimentacao movimentacao);
    }
}
