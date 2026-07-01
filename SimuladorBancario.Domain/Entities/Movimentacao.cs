using SimuladorBancario.Domain.Enums;

namespace SimuladorBancario.Domain.Entities;

public class Movimentacao
{
    public Guid ID { get; private set; }
    public Guid ContaID { get; private set; }
    public decimal Valor { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }
    public DateTime DataHora { get; private set; }

    private Movimentacao() { }

    public Movimentacao(Guid contaID, decimal valor, TipoMovimentacao tipo)
    {
        ID = Guid.NewGuid();
        ContaID = contaID;
        Valor = valor;
        Tipo = tipo;
        DataHora = DateTime.Now;
    }
}