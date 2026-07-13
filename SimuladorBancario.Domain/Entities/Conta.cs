using SimuladorBancario.Domain.Enums;

namespace SimuladorBancario.Domain.Entities;

public class Conta
{
    public Guid Id { get; private set; }
    public Guid ClienteId { get; private set; }
    public decimal Saldo { get; private set; }
    public DateTime DataHora { get; private set; }

    public ICollection<Movimentacao> Movimentacoes { get; private set; }

    private Conta()
    {
        Movimentacoes = new List<Movimentacao>();
    }

    public Conta(Guid clienteId)
    {
        Id = Guid.NewGuid();
        ClienteId = clienteId;
        Saldo = 0;
        DataHora = DateTime.Now;
        Movimentacoes = new List<Movimentacao>();
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do depósito deve ser maior que zero.");

        Saldo += valor;
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
        
    }

    public void AdicionarMovimentacao(Movimentacao movimentacao) =>
        Movimentacoes.Add(movimentacao);
}