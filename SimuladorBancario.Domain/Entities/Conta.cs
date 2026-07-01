

namespace SimuladorBancario.Domain.Entities;

public class Conta
{
    public Guid ID { get; private set; }
    public Guid ClientID { get; private set; }
    public decimal Saldo { get; private set; }
    public DateTime DataHora { get; private set; }

    public ICollection<Movimentacao> Movimentacoes { get; private set; }

    private Conta()
    {
        Movimentacoes = new List<Movimentacao>();
    }

    public Conta(Guid clientID)
    {
        ID = Guid.NewGuid();
        ClientID = clientID;
        Saldo = 0;
        DataHora = DateTime.Now;
        Movimentacoes = new List<Movimentacao>();
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do depósito deve ser maior que zero.");

        Saldo += valor;
        Movimentacoes.Add(new Movimentacao(ID, valor, TipoMovimentacao.Deposito));
    }

    public void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser maior que zero.");

        if (valor > Saldo)
        {
            throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");
        }

        Saldo -= valor;
        Movimentacoes.Add(new Movimentacao(ID, valor, TipoMovimentacao.Saque));
    }
}