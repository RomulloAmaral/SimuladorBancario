using SimuladorBancario.Application.Interfaces;
using SimuladorBancario.Domain.Entities;
using SimuladorBancario.Domain.Enums;

namespace SimuladorBancario.Application.Services;

public class ContaService : IContaService
{
    private readonly IContaRepository _contaRepo;
    private readonly IMovimentacaoRepository _movimentacaoRepo;
    public ContaService(IContaRepository contaRepo, IMovimentacaoRepository movimentacaoRepo)
    {
        _contaRepo = contaRepo;
        _movimentacaoRepo = movimentacaoRepo;
    }

    public async Task<Guid> CriarContaAsync(Guid clienteId)
    {
        var existente = await _contaRepo.ObterPorClienteIdAsync(clienteId);
        if (existente != null)
            throw new InvalidOperationException("Cliente já possui uma conta.");

        var conta = new Conta(clienteId);
        await _contaRepo.AdicionarAsync(conta);
        await _contaRepo.SalvarAsync();
        return conta.Id;
    }

    public async Task DepositarAsync(Guid contaId, decimal valor)
    {
        var conta = await _contaRepo.ObterPorIdAsync(contaId)
            ?? throw new KeyNotFoundException("Conta não encontrada.");

        conta.Depositar(valor);
        var movimentacao = new Movimentacao(contaId, valor, TipoMovimentacao.Deposito);
        await _movimentacaoRepo.CriarMovimentacao(movimentacao);
        conta.AdicionarMovimentacao(movimentacao);
        await _contaRepo.SalvarAsync();

        
    }

    public async Task SacarAsync(Guid contaId, decimal valor)
    {
        var conta = await _contaRepo.ObterPorIdAsync(contaId)
            ?? throw new KeyNotFoundException("Conta não encontrada.");

        
        conta.Sacar(valor);

        
        var movimentacao = new Movimentacao(contaId, valor, TipoMovimentacao.Saque);
        await _movimentacaoRepo.CriarMovimentacao(movimentacao);

        
        await _contaRepo.SalvarAsync(); 
    }

    public async Task TransferirAsync(Guid contaOrigemId, Guid contaDestinoId, decimal valor)
    {
        if (contaOrigemId == contaDestinoId)
            throw new InvalidOperationException("Não é possível transferir para a própria conta.");

        var origem = await _contaRepo.ObterPorIdAsync(contaOrigemId)
            ?? throw new KeyNotFoundException("Conta de origem não encontrada.");

        var destino = await _contaRepo.ObterPorIdAsync(contaDestinoId)
            ?? throw new KeyNotFoundException("Conta de destino não encontrada.");

        // 1. Aplica as regras de negócio
        origem.Sacar(valor);
        destino.Depositar(valor);

        // 2. Cria e salva as movimentações
        var movimentacaoOrigem = new Movimentacao(contaOrigemId, valor, TipoMovimentacao.TransferenciaEnviada);
        await _movimentacaoRepo.CriarMovimentacao(movimentacaoOrigem);

        var movimentacaoDestino = new Movimentacao(contaDestinoId, valor, TipoMovimentacao.TransferenciaRecebida);
        await _movimentacaoRepo.CriarMovimentacao(movimentacaoDestino);

        
        await _contaRepo.SalvarAsync(); 
    }

    public async Task<decimal> ConsultarSaldoAsync(Guid contaId)
    {
        var conta = await _contaRepo.ObterPorIdAsync(contaId)
            ?? throw new KeyNotFoundException("Conta não encontrada.");

        return conta.Saldo;
    }

    public async Task<IEnumerable<Movimentacao>> ObterExtratoAsync(Guid contaId)
    {
        var conta = await _contaRepo.ObterPorIdAsync(contaId)
            ?? throw new KeyNotFoundException("Conta não encontrada.");

        return conta.Movimentacoes.OrderByDescending(m => m.DataHora);
    }
}