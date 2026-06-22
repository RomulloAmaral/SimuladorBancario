namespace SimuladorBancario.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string CpfCnpj { get; private set; }
    public DateTime DataCadastro { get; private set; }

    // EF Core precisa de construtor sem parâmetros (privado está ok)
    private Cliente() { }

    public Cliente(string nome, string cpfCnpj)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(cpfCnpj))
            throw new ArgumentException("CPF/CNPJ é obrigatório.");

        Id = Guid.NewGuid();
        Nome = nome;
        CpfCnpj = cpfCnpj;
        DataCadastro = DateTime.UtcNow;
    }
}