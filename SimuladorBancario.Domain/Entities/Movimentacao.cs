using SimuladorBancario.Domain.Enums;

namespace SimuladorBancario.Domain.Entities;

public class Movimentacao
{
    public Guid Id { get; private set; }
    public Guid ContaId { get; private set; }
    public decimal Valor { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }
    public DateTime DataHora { get; private set; }

    private Movimentacao() { }

    public Movimentacao(Guid contaId, decimal valor, TipoMovimentacao tipo)
    {
        Id = Guid.NewGuid();
        ContaId = contaId;
        Valor = valor;
        Tipo = tipo;
        DataHora = DateTime.Now;
    }
}